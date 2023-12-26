using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : Item
{
    public Rigidbody rigidbody;
    public Vector3 oriPos;
    public Quaternion oriRot;
    public bool isCompleted;
    [SerializeField] private float toySize = 1;
    void Start()
    {
       rigidbody=GetComponent<Rigidbody>();
    }
    public void ReadToyData(ToyData toyData)
    {
        isCompleted = toyData.isCompleted;
        if (isCompleted)
        {
            transform.position = toyData.toyPos;
            transform.rotation = toyData.toyRot;
        }
    }
    public ToyData WriteToyData()
    {
        ToyData toyData = new ToyData(itemName, isCompleted, transform.position, transform.rotation);
        return toyData;
    }

    // check if the toy can be caught by the bubble based on its size
    public bool CanCatch(float bubbleSize)
    {
        if (bubbleSize >= toySize)
        {
            return true;
        }
        return false;
    }
    public void ResetPos()
    {
        transform.parent = PosMgr.Instance.transform;
        isCompleted = false;
        transform.position = oriPos;
        transform.rotation = oriRot;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (isCompleted)
        {
            return;
        }

        if (other.CompareTag("finaldestination"))
        {
            isCompleted = true;
            transform.parent = PosMgr.Instance.transform;
            other.gameObject.SetActive(false);
            // notify GameMgr of a completed toy
            GameMgr.Instance.AddCompleteToy();
        }

        if (other.CompareTag("Player"))
        {
            if (CanCatch(other.GetComponent<BubbleController>().bubbleSize) && other.GetComponent<BubbleController>().toyCount == 0)
            {
                rigidbody.useGravity=false;
                rigidbody.isKinematic=true;

                transform.parent = other.transform;
                other.GetComponent<BubbleController>().MoveToy(this);
            }

        }
    }

}

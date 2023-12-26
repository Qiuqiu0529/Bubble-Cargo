using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class BubbleController : MonoBehaviour
{
    // movement, pos, size
    public float airPos = 6f;
    public float stopMoveUpPos = 8f;
    public float moveSpeed = 5f;
    public float maxSize = 2f;
    public float minSize = 0.5f;
    public float absorbRate = 0.1f;
    public float waterRiseSpeed = 1f;
    public float bubbleSize = 1f;

    public int toyCount = 0;
    public Toy catchToy;

    public bool useParticle;
    public Transform bubbleParticle;

    public Transform oriPos;

    float horizontalInput;
    float verticalInput;
    Vector3 movement;

    public bool canMove = true;
    public bool pause;

    [SerializeField] bool isTouchingWaterColumn = false;
    private Rigidbody rb;

    public CapsuleCollider capsuleCollider;
    public float colliderRadius;

    // feedback effects
    public MMF_Player moveToyToCenterFB;
    public MMF_Position mMF_Position;
    public MMF_Player returnOriPosFB, vortexFB, waterColumeFB, waterMoveSoundFB, airMoveSoundFB;

    void Start()
    {
        pause = false;
        rb = GetComponent<Rigidbody>();
        toyCount = 0;
        mMF_Position = moveToyToCenterFB.GetFeedbackOfType<MMF_Position>();
    }

    private void Update()
    {
        if (pause)
        {
            return;
        }

        if (!canMove)
        {
            return;
        }

        movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // when the bubble touches the water column, continue rising
        if (isTouchingWaterColumn)
        {
            movement += Vector3.up * waterRiseSpeed * Time.deltaTime;

        }

        // detect collisions
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 2f);
        isTouchingWaterColumn = false;
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("watercolumn")) //when touching the water column
            {
                waterColumeFB.PlayFeedbacks();// play sound
                isTouchingWaterColumn = true;
            }

        }
        if (!isTouchingWaterColumn)
        {
            if (waterColumeFB.IsPlaying)
                waterColumeFB.StopFeedbacks();
        }

        rb.MovePosition(rb.position + movement);
        if (movement.magnitude >= 0.0001)
        {
            if (transform.position.y > airPos)
            {
                waterMoveSoundFB.StopFeedbacks();
                if (!airMoveSoundFB.IsPlaying)
                    airMoveSoundFB.PlayFeedbacks();
            }
            else
            {
                airMoveSoundFB.StopFeedbacks();
                if (!waterMoveSoundFB.IsPlaying)
                    waterMoveSoundFB.PlayFeedbacks();
            }
        }
        else
        {
            waterMoveSoundFB.StopFeedbacks();
            airMoveSoundFB.StopFeedbacks();
        }
    }
    public void SetHorizontalInput(float inputValue)
    {
        horizontalInput = inputValue;
    }
    public void SetVerticalInput(float inputValue)
    {
        verticalInput = inputValue;
    }


    public void Up()
    {
        horizontalInput = 0;
        verticalInput = 1;
    }
    public void Down()
    {
        horizontalInput = 0;
        verticalInput = -1;
    }
    public void Left()
    {
        horizontalInput = -1;
        verticalInput = -0;
    }
    public void Right()
    {
        horizontalInput = 1;
        verticalInput = 0;
    }
    public void Stay()
    {
        horizontalInput = 0;
        verticalInput = 0;
    }

    public void EnlargeBubble()
    {
        Debug.Log("EnlargeBubble");
        Vector3 scale = bubbleParticle.localScale;
        float nScale = scale.x;
        nScale += 0.01f;
        nScale = Mathf.Min(nScale, maxSize);
        capsuleCollider.radius = colliderRadius * nScale;
        capsuleCollider.height = nScale;
        bubbleParticle.localScale = Vector3.one * nScale;
        bubbleSize = bubbleParticle.localScale.x;
    }

    // move toy to bubble center
    public void MoveToy(Toy toy)
    {
        catchToy = toy;
        toyCount++;
        mMF_Position.AnimatePositionTarget = toy.gameObject;
        moveToyToCenterFB.PlayFeedbacks();
    }

    // reset size
    public void VorTexFB()
    {
        if (catchToy)
        {
            catchToy.ResetPos();
            catchToy = null;
        }

        capsuleCollider.radius = colliderRadius * minSize;
        capsuleCollider.height = minSize;
        bubbleParticle.localScale = minSize * Vector3.one;
        bubbleSize = bubbleParticle.localScale.x;
        pause = true;

        toyCount = 0;
        vortexFB.PlayFeedbacks();
    }
    public void ReSetPos()
    {
        pause = true;
        toyCount = 0;

        returnOriPosFB.PlayFeedbacks();
    }
    public void ResetScale()
    {
        capsuleCollider.radius = colliderRadius * 1;
        capsuleCollider.height = 1;
        bubbleParticle.localScale = 1 * Vector3.one;

    }
    public void Pause()
    {
        pause = true;
    }
    public void Resume()
    {
        pause = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("vortex"))
        {
            VorTexFB();
        }
    }

}

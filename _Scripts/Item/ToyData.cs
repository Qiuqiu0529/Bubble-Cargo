using UnityEngine;

public class ToyData : ScriptableObject
{
    public ItemName toyName;
    public bool isCompleted;
    public Vector3 toyPos;
    public Quaternion toyRot;
     public ToyData(ItemName name, bool completed,Vector3 pos,Quaternion rot)
    {
        toyName = name;
        isCompleted = completed;
        toyPos=pos;
        toyRot=rot;
    }
}
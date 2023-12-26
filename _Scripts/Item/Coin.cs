using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
public class Coin : MonoBehaviour
{
    public MMF_Player hitFB;
    public bool canHit;
    public BoxCollider boxCollider;
    public CoinSpawn coinSpawn;
    private void OnEnable()
    {
        canHit = true;
        boxCollider.enabled = true;
    }

    public void SetSpawn(CoinSpawn mcoinSpawn)
    {
        coinSpawn= mcoinSpawn;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&canHit)
        {
            hitFB.PlayFeedbacks();
            GameMgr.Instance.AddCoin();
            canHit = false;
            coinSpawn.ReSpawn();
        }    
    }
}

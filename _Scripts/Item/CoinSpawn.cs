using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    public bool cointiNuesSpawn;
    public ObjectPool coinPool;
    bool haveCoin;

    private void Start()
    {
        StartCoroutine(ReSpawnIenum());
    }

    public void SpawnCoin()
    {
        int temp = Random.Range(0, 100);
        if (temp <= 50)
        {
            PooledObject pooledObject = coinPool.GetPoolObj();
            pooledObject.GetComponent<Coin>().SetSpawn(this);
            pooledObject.transform.position =
                transform.position;
            haveCoin = true;
        }

    }
    public void ReSpawn()
    {
        StartCoroutine(ReSpawnIenum());
    }

    IEnumerator ReSpawnIenum()
    {
        haveCoin = false;
        while (!haveCoin)
        {
            yield return new WaitForSeconds(7f);
            SpawnCoin();
        }
        
    }
}

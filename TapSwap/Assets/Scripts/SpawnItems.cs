using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToSpawn = new GameObject[3];
    [SerializeField] private GameObject[] SpawnXY = new GameObject[3];
    [SerializeField] private GameObject GameManage;

    public void Spawn()
    {
        float ItemId = Random.Range(0f,(float)itemsToSpawn.Length);
        float ItemXY = Random.Range(0f,(float)SpawnXY.Length);
        Vector3 v = new Vector3(SpawnXY[(int)ItemXY].transform.position.x,transform.position.y);
        Instantiate(itemsToSpawn[(int)ItemId],v,Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnEnemy;
    [SerializeField] private List<Transform> spawnPoint;

    private void Start()
    {
        int rand = Random.Range(0, spawnPoint.Count);
        for(int i = 0; i < 30; i++)
        {
            Instantiate(spawnEnemy, spawnPoint[i].position, Quaternion.identity);
        }
    }
}

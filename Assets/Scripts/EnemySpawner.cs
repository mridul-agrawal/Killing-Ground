using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    [Range(0,100)]
    public int enemyCount = 30;
    private List<GameObject> enemies;
    private float range = 70f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<enemyCount; i++)
        {
            GameObject spawnedEnemy = Instantiate(enemy, GetRandomSpawnPosition(range), Quaternion.identity);
        }
    }


    public Vector3 GetRandomSpawnPosition(float radius)
    {
        Vector3 randomPosition = Random.insideUnitSphere * radius;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if(NavMesh.SamplePosition(randomPosition,out hit,radius,1))
        {
            finalPosition = hit.position;
            return finalPosition;
        }
        return finalPosition;
    }

}

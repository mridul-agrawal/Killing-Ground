using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    [Range(0,100)]
    public int enemyCount = 30;
    private List<GameObject> enemies;
    private float range = 35f;
    public int currentEnemies;
    public TextMeshProUGUI ZombiesLeftText;
    public TextMeshProUGUI GameStatus;
    public GameObject winOverlay;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<enemyCount; i++)
        {
            GameObject spawnedEnemy = Instantiate(enemy, GetRandomSpawnPosition(range), Quaternion.identity);
        }
        currentEnemies = enemyCount;
    }


    public Vector3 GetRandomSpawnPosition(float radius)
    {
        Vector3 randomPosition = new Vector3(-30,0,0) +  Random.insideUnitSphere * radius;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if(NavMesh.SamplePosition(randomPosition,out hit,radius,1))
        {
            finalPosition = hit.position;
            return finalPosition;
        }
        return finalPosition;
    }

    private void Update()
    {
        ZombiesLeftText.text = currentEnemies.ToString();
        if(currentEnemies == 0)
        {
            // You Won!
            GameStatus.text = "YOU WON!!!";
            ZombiesLeftText.text = "";
            winOverlay.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

}

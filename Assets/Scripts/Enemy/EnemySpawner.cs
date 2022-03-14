using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using KillingGround.Services;

namespace KillingGround.Enemy
{
    /// <summary>
    /// This class is responsible for Spawning enemies and tracking their count.
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        // References: 
        [SerializeField] private GameObject enemy;
        private List<GameObject> enemies;

        // Variables:
        [Range(0, 100)] [SerializeField] private int enemyCount = 30;
        private float range = 35f;
        internal int currentEnemies;


        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject spawnedEnemy = Instantiate(enemy, GetRandomSpawnPosition(range), Quaternion.identity);
            }
            currentEnemies = enemyCount;
        }

        // Spawns an enemy at a Random position on the NavMesh.
        public Vector3 GetRandomSpawnPosition(float radius)
        {
            Vector3 randomPosition = new Vector3(-30, 0, 0) + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomPosition, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }

        private void Update()
        {
            if (currentEnemies == 0)
            {
                UIService.Instance.UpdateWinUI();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }
}
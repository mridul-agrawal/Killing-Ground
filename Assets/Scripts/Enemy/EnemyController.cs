using System.Collections;
using KillingGround.Player;
using KillingGround.Audio;
using KillingGround.VFX;
using KillingGround.Services;
using UnityEngine;
using UnityEngine.AI;

namespace KillingGround.Enemy
{
    /// <summary>
    /// This script is used for enemy AI.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        // References:
        private NavMeshAgent navMeshAgent;
        private Transform target;
        private Animator enemyAnimator;
        [SerializeField] private EnemySpawner enemySpawner;

        // Variables:
        private bool canAttack = true;


        // Start is called before the first frame update
        void Start()
        {
            SetReferences();
            StartCoroutine(PlayZombieSound());
        }

        // Used to Set References.
        private void SetReferences()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            target = PlayerController.Instance.transform;
            enemyAnimator = GetComponentInChildren<Animator>();
        }

        // Coroutine to play zombie sounds at random intervals. 
        IEnumerator PlayZombieSound()
        {
            while (true)
            {
                SoundManager.Instance.PlaySoundEffects2(SoundType.zombieSound);
                yield return new WaitForSeconds(Random.Range(4, 10));
            }
        }

        // Update is called once per frame
        void Update()
        {
            navMeshAgent.SetDestination(target.position);
        }

        // Checks if collision with a thrown object occurs and handles that logic.
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Box"))
            {
                if (other.attachedRigidbody.velocity.magnitude > 5f)
                {
                    ParticleEffects.Instance.PlaydeathSplashAt(transform.position);
                    GameObject.Destroy(gameObject);
                    UIService.Instance.UpdateEnemyUI(--enemySpawner.currentEnemies);
                }
            }
        }

        // Checks if the player is in Range to Attack.
        private void OnTriggerStay(Collider other)
        {
            if (canAttack == true && other.CompareTag("Player"))
            {
                StartCoroutine(AttackPlayer());
                canAttack = false;
            }
        }

        IEnumerator AttackPlayer()
        {
            ToggleAttackAnimation(true);
            yield return new WaitForSeconds(1f);
            ToggleAttackAnimation(false);
            canAttack = true;
        }

        // Toggles the enemy attack animation.
        public void ToggleAttackAnimation(bool isAttacking)
        {
            enemyAnimator.SetBool("attack", isAttacking);
        }
    }
}
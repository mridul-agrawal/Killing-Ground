using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent navMeshAgent;
    public GameObject deathSplash;
    private Animator enemyAnimator;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        enemyAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Box"))
        {
            if(other.attachedRigidbody.velocity.magnitude > 5f)
            {
                Instantiate(deathSplash, transform.position, Quaternion.identity);
                GameObject.Destroy(gameObject);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && canAttack == true)
        {
            StartCoroutine("AttackPlayer");
            canAttack = false;
        }
    }

    public void Attack()
    {
        enemyAnimator.SetBool("attack", true);
    }


    IEnumerator AttackPlayer()
    {
        Attack();
        yield return new WaitForSeconds(1f);
        enemyAnimator.SetBool("attack", false);
        canAttack = true;
    }


}

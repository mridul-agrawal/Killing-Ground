using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Mortality : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    public GameObject deathOverlay;

    private bool IfAlive()
    {
        return health > 0;
    }

    private void Awake()
    {
        deathOverlay.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if(health>damage)
        {
            Debug.Log("Taking Damage:\t" + damage);
            health = health - damage;
        } else
        {
            if(IfAlive())
            {
                health = 0;
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log("DIE");
        GetComponent<Animator>().SetBool("die",true);
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<CameraController>().enabled = false;
        deathOverlay.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Damager"))
        {
            TakeDamage(10);
        }
    }

}

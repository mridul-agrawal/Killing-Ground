using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class Mortality : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    public GameObject deathOverlay;
    public TextMeshProUGUI hpText;
    public AudioClip deathSound;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        deathOverlay.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if(health-damage > 0)
        {
            health -= damage;
        } else
        {
            health = 0;
            Die();
        }
        hpText.text = health.ToString();
    }

    public void Die()
    {
        GetComponent<Animator>().SetBool("die",true);
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<CameraController>().enabled = false;
        deathOverlay.SetActive(true);
        audioSource.clip = deathSound;
        audioSource.Play();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Damager"))
        {
            TakeDamage(5);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController CharacterController;
    public float speed = 6f;
    public float runningSpeed = 12f;
    public float currentSpeed;
    public Animator animator;

    // Gravity:
    private float gravity = 9.87f;
    private float verticalSpeed = 0f;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : speed;

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if(CharacterController.isGrounded) 
        {
            verticalSpeed = 0;
        } else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }

        Vector3 forceOfGravity = new Vector3(0,verticalSpeed,0);
        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        CharacterController.Move(motion: move * Time.deltaTime * currentSpeed + forceOfGravity * Time.deltaTime);

        animator.SetBool("isWalking", horizontalMove!=0 || verticalMove!=0);
        animator.SetBool("run", currentSpeed == runningSpeed);

    }


    public void Restart()
    {
        Debug.Log("Inside Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}

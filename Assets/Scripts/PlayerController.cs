using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // References:
    [SerializeField] private CharacterController CharacterController;
    [SerializeField] private Animator animator;

    // Variables:
    [SerializeField] private float speed = 6f;
    [SerializeField] private float runningSpeed = 12f;
    private float currentSpeed;
    private float gravity = 9.87f;
    private float verticalSpeed = 0f;


    private void Awake()
    {
        SetUpCursor();
    }

    // Locks Cursor and makes it invisible at the start of game.
    private static void SetUpCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
    }

    // This method uses Character Controller Script to move our player occording to Input.
    public void Move()
    {
        float horizontalMove, verticalMove;
        Vector3 move;
        GetInput(out horizontalMove, out verticalMove, out move);
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : speed;     // Check Current Speed (Walking OR Running)
        Vector3 forceOfGravity = GetGravity();

        CharacterController.Move(motion: move * Time.deltaTime * currentSpeed + forceOfGravity * Time.deltaTime);
        HandleAnimation(horizontalMove, verticalMove);
    }

    // Used to calculate force of gravity.
    private Vector3 GetGravity()
    {
        if (CharacterController.isGrounded)
        {
            verticalSpeed = 0;
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
        return new Vector3(0, verticalSpeed, 0);
    }

    // This Method is used to receive player Input.
    private void GetInput(out float horizontalMove, out float verticalMove, out Vector3 move)
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        move = transform.forward * verticalMove + transform.right * horizontalMove;
    }

    // Handles Player Animation according to inputs received.
    private void HandleAnimation(float horizontalMove, float verticalMove)
    {
        animator.SetBool("isWalking", horizontalMove != 0 || verticalMove != 0);
        animator.SetBool("run", currentSpeed == runningSpeed);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] public float playerSpeed = 1.9f;
    [SerializeField] public float playerSprint = 3f;

    //[Header("Player Health Things")]

    [Header("Player Script Cameras")]
    [SerializeField] public Transform playerCamera;

    [Header("Player Animator and Gravity")]
    [SerializeField] public CharacterController cC;
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] public Animator anim;

    [Header("Player Jumping and Velocity")]
    [SerializeField] public float jumpRange = 1f;
    [SerializeField] public Transform surafaceCheck;
    [SerializeField] public float surfaceDistance = 0.4f;
    [SerializeField] public LayerMask surafaceMask;
    Vector3 velocity;
    bool onSurafce;
    [SerializeField] public float turnCalmTime = 0.1f;
    float turnCalmVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        onSurafce = Physics.CheckSphere(surafaceCheck.position, surfaceDistance, surafaceMask);
        if (onSurafce && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);

        playerMove();
        Sprint();
        Jump();
    }
    void playerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Running", false);
            anim.SetBool("Idel", false);
            anim.SetTrigger("Jump");
            anim.SetBool("AimWalk", false);
            anim.SetBool("IdelAim", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Idel", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Running", false);
            anim.SetBool("AimWalk", false);
            anim.SetTrigger("Jump");
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurafce)
        {
            anim.SetBool("Walk", false);
            anim.SetTrigger("Jump");

            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            anim.ResetTrigger("Jump");
        }
    }
    void Sprint()
    {
        if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurafce)
        {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                anim.SetBool("Running", true);
                anim.SetBool("Idel", false);
                anim.SetBool("Walk", false);
                anim.SetBool("IdelAim", false);
                anim.SetTrigger("Jump");

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Idel", false);
                anim.SetBool("Walk", false);
            }
        }
    }
}

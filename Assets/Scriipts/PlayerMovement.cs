using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controler;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    private Vector3 velocity;
    private bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(x > 0f && z > 0 || x > 0 || z > 0 || x < 0f && z < 0 || x < 0 || z < 0)
        {
            GunSystem.isMoving = true;
        }else
        {
            GunSystem.isMoving = false;
        }
        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 20;
        }else if(Input.GetKey(KeyCode.LeftControl))
        {
            speed = 5;
        }
        else
        {
            speed = 10;
        }

        controler.Move(move * speed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controler.Move(velocity * Time.deltaTime);
    }
}

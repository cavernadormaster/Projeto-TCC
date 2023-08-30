using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody controler;
    public float speed;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    public static bool takeItem;

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
            speed = 800;
        }else if(Input.GetKey(KeyCode.LeftControl))
        {
            speed = 100;
        }
        else
        {
            speed = 200;
        }

       controler.velocity = ((transform.forward * z) + transform.right* x) * speed * Time.deltaTime ;
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(takeItem)
        {
            if (Input.GetKey(KeyCode.E))
            {
                TakeItem.PickItem();
                takeItem = false;
            }
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
    public CharacterController controller;

    private Vector3 velocity;
    private bool isGrounded;


    private void Start()
    {

    }

    private void Update()
    {
       
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

       
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
        
            if (x > 0f && z > 0 || x > 0 || z > 0 || x < 0f && z < 0 || x < 0 || z < 0)
            {
                GunSystem.isMoving = true;
            }
            else
            {
                GunSystem.isMoving = false;
            }

            if (UIController.isAlive)
            {
                Vector3 move = transform.right * x + transform.forward * z;
                controller.Move(move * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 20;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                speed = 5;
            }
            else
            {
                speed = 12;
            }

            

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Debug.Log("Jump");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

          
            controller.Move(velocity * Time.deltaTime);

            if (takeItem)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    TakeItem.PickItem();
                    takeItem = false;
                }
            }
        

        
    }

   
}

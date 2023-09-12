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

    private Vector3 velocity;
    private bool isGrounded;


    private void Start()
    {
        StartCoroutine(EnviarDados());
    }

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

    IEnumerator EnviarDados()
    {
        while (true)
        {
            float[] trsfrm = new float[] { transform.position.x, transform.position.y, transform.position.z, transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w };
            byte[] dados = FloatArrayToByteArray(trsfrm);
            IPEndPoint ipep = new IPEndPoint(MltJogador.remotoIPAdress, MltJogador.PORTA);
            MltJogador.udpClient.Send(dados, dados.Length, ipep);
            yield return new WaitForSeconds(0.01f);

        }
    }
    byte[] FloatArrayToByteArray(float[] f)
    {
        byte[] b = new byte[28];
        for (int i = 0; i < f.Length; i += 1)
        {
            byte[] parcial = BitConverter.GetBytes(f[i]);
            Array.Copy(parcial, 0, b, i * 4, 4);
        }
        return b;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class IniciaCenaJogo : MonoBehaviour
{

    [SerializeField]
    GameObject gameObjectLocal, gameObjectRemoto;
    private void Awake()
    {

        //isso aqui é só para deixar as mesmas cores nas duas telas
        byte[] b;
        int ipRemoto, ipLocal;

        b=MltJogador.remotoIPAdress.GetAddressBytes();
        Array.Reverse(b);
        ipRemoto=BitConverter.ToInt32(b, 0);

        b = MltJogador.meuIPAdress.GetAddressBytes();
        Array.Reverse(b);
        ipLocal = BitConverter.ToInt32(b, 0);


        if (ipRemoto>ipLocal)
        {
            Vector3 pos;
            pos= gameObjectLocal.transform.position;
            pos.x = 3;
            gameObjectLocal.transform.position = pos;
            gameObjectLocal.GetComponent<MeshRenderer>().material.color = Color.red;
            gameObjectRemoto.GetComponent<MeshRenderer>().material.color= Color.blue;
        }
        else
        {
            Vector3 pos;
            pos = gameObjectLocal.transform.position;
            pos.x = 3;
            gameObjectLocal.transform.position = pos;
            gameObjectLocal.GetComponent<MeshRenderer>().material.color = Color.blue;
            gameObjectRemoto.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

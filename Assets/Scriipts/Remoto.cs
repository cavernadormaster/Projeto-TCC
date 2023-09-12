using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.XR;

public class Remoto : MonoBehaviour
{
    Thread recepcao;
    byte[] dadosRecebidos;
    Vector3 posicao;
    Quaternion rotacao;
    // Start is called before the first frame update
    void Start()
    {
        
        recepcao = new Thread(RecebeDados);
        recepcao.Start();
        StartCoroutine(Atualizar());

    }

    IEnumerator Atualizar()
    {
        while (true)
        {
            try
            {
                TratarDadosRecebidos();
                transform.position = Vector3.MoveTowards(transform.position, posicao, 200 * Time.deltaTime);
                transform.rotation= Quaternion.Lerp(transform.rotation, rotacao, 100*Time.deltaTime);
            }
            catch(Exception e) {
            
                Debug.LogError(e);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void TratarDadosRecebidos()
    {
        float x, y, z,w;
        
        //aqui para posição
        x = ByteArrayToFloat(0);
        y = ByteArrayToFloat(1);
        z = ByteArrayToFloat(2);
        posicao = new Vector3(x, y, z);
        //aqui para rotação
        x = ByteArrayToFloat(3);
        y = ByteArrayToFloat(4);
        z = ByteArrayToFloat(5);
        w = ByteArrayToFloat(6);
        rotacao = new Quaternion(x, y, z,w);

    }
    float ByteArrayToFloat(int pos)
    {
        byte[] b = new byte[4];//o tipo float tem 4 bytes
        Array.Copy(dadosRecebidos, 4 * pos, b, 0, 4);
        return BitConverter.ToSingle(b);
    }
    private void RecebeDados()
    {
        while (true)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] dados = MltJogador.udpClient.Receive(ref RemoteIpEndPoint);
            dadosRecebidos = dados;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class MltJogador : MonoBehaviour
{
    public static UdpClient udpClient;
    public static IPAddress remotoIPAdress;
    public static IPAddress meuIPAdress;
    public const int PORTA= 11000;
    public static void SetUdpCliente(IPAddress address)
    {
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            meuIPAdress = endPoint.Address;
        }

        udpClient = new UdpClient(PORTA);
        remotoIPAdress = address;
    }
}

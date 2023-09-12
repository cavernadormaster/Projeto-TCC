using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrada : MonoBehaviour
{
    [SerializeField]
    TMP_InputField ipremoto;
    public string scene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnIniciarClick()
    {
        MltJogador.SetUdpCliente(IPAddress.Parse( ipremoto.text));
        SceneManager.LoadScene(scene);

    }
}

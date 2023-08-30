using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    public static GameObject Gun;
    public static GameObject mao;
    public static Transform mao1;
    public static GameObject thisGameobject1;
    public GameObject player;
    public string thisGameobject;
    public string maoName;
    public string GunName;
    void Start()
    {
        Gun = GameObject.Find(GunName);
        mao = GameObject.Find(maoName);
        mao1 = player.transform.Find(maoName);
        thisGameobject1 = GameObject.Find(thisGameobject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PickItem()
    {
        Gun.transform.parent = mao.transform;
        Gun.transform.position = mao.transform.position;
        GunSystem.ArmaPrincipalAtiva = true;
        Destroy(thisGameobject1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerMovement.takeItem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}

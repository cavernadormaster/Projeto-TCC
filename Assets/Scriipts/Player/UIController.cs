using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    public static bool isAlive;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    #region Canvas Death Itens
    //Itens que estão no Canvas de morte do jogador
    public GameObject respawnButton;
    #endregion

    #region Transform de Locais
    // transform de locais onde o jogador pode se transportar
    public GameObject[] Respawnpoint;
    #endregion

    #region Variaveis Internas
    // Variaveis que são lidas dentro do UIController Script
    int ordemDosRespawnPoints = 0; // define qual transform de respawnpoint está atualmente
    bool respawnn;
    #endregion

    #region Objetos na cena
    // objetos que estejam na cena e possam interagir dentro do script
    public GameObject player;
    #endregion

    void Start()
    {
        isAlive = true;
        CurrentHealth = MaxHealth;
        SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (CurrentHealth <= 0)
        {
            isAlive = false;
            respawnButton.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isAlive = true;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
        CurrentHealth -= damage;
        Debug.Log("CurrentHealth: " + CurrentHealth);
        setHealth(CurrentHealth);
    }

    public void setHealth (int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void Respawn()
    {
        CharacterController temp = GetComponent<CharacterController>();
        temp.enabled = false;
        player.transform.position = Respawnpoint[ordemDosRespawnPoints].transform.position;
        CurrentHealth = MaxHealth;
        SetMaxHealth(MaxHealth);
        respawnButton.SetActive(false);
        temp.enabled = true;
    }

    
}

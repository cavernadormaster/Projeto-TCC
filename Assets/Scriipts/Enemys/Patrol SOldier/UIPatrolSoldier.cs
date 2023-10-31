using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPatrolSoldier : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    public static bool isAlive;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    void Start()
    {
        isAlive = true;
        CurrentHealth = MaxHealth;
        SetMaxHealth(MaxHealth);
    }

    void Update()
    {
        if (CurrentHealth <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
        else
        {
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

    public void setHealth(int health)
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

}

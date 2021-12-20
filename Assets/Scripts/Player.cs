using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxHealth;
    float currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageValue)
    {
        Debug.Log("you took damage: " + damageValue);
        currentHealth -= damageValue;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);
        SetHealthbar();
        if(currentHealth <= 0.0f)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Debug.Log("you died");
    }

    private void SetHealthbar()
    {
        UIMenuManager.Instance.UpdateHealth(currentHealth/maxHealth);
    }

    //debug
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(-20);
        }
    }
}

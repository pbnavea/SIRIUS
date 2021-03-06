﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float currentHealth, maxHealth;
    public float currentStamina, maxStamina;
    public float currentHunger, maxHunger;
    public float temperature;
   

    public float armor;
    public float insulation;

    public float healthRecoveryRate;
    public float staminaRecoveryRate;
    public float hungerRecoveryRate;
    

    public GameManagerScript gameManagerScript;
    public UIScript uiScript;

    private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        uiScript = GameObject.Find("UI").GetComponent<UIScript>();
    }

    private void FixedUpdate()
    {
        if(currentHealth < maxHealth)
        {
            ChangeHealth(healthRecoveryRate);
        }
        if(currentStamina < maxStamina)
        {
            ChangeStamina(staminaRecoveryRate);
        }
        if(currentHunger > 0)
        {
            ChangeHunger(hungerRecoveryRate);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth >= maxHealth) currentHealth = maxHealth;
        else if (currentHealth <= 0) currentHealth = 0.0f;

        uiScript.UpdateHealth((currentHealth / maxHealth) * 100.0f);
        if (currentHealth <= 0.0) { Die(); }
    }

    public void ChangeHealth(float health)
    {
        currentHealth += health;

        if (currentHealth >= maxHealth) currentHealth = maxHealth;
        else if (currentHealth <= 0) currentHealth = 0.0f;

        uiScript.UpdateHealth((currentHealth / maxHealth) * 100.0f);
        if (currentHealth <= 0.0) { Die(); }
    }

    public void ChangeStamina(float stamina)
    {
        currentStamina += stamina;

        if (currentStamina >= maxStamina) currentStamina = maxStamina;
        else if (currentStamina <= 0) currentStamina = 0.0f;

        uiScript.UpdateStamina((currentStamina / maxStamina) * 100.0f);
    }

    public void ChangeHunger(float hunger)
    {
        currentHunger += hunger;

        if (currentHunger >= maxHunger) currentHunger = maxHunger;
        else if (currentHunger <= 0) currentHunger = 0.0f;

        uiScript.UpdateHunger((currentHunger / maxHunger) * 100.0f);
    }


    public void Die()
    {

    }


}

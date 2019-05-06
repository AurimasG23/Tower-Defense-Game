using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private static double hitpoints = 100;
    double health;
    public Image healthBar;

    public GameObject deathEffect;

    // Use this for initialization
    void Start ()
    {
        health = hitpoints;
        UpdateHealthBarValue();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void ReduceHealth(double damage)
    {
        health -= damage;
        UpdateHealthBarValue();
    }

    public void UpdateHealthBarValue()
    {
        float fillAmount = (float)(health / hitpoints);
        healthBar.fillAmount = fillAmount;
        if(fillAmount <= 1 && fillAmount > 0.65)
        {
            healthBar.color = new Color((float)70/255, (float)195/255, (float)12/255, 1);
        }
        else if (fillAmount <= 0.64 && fillAmount > 0.35)
        {
            healthBar.color = new Color((float)195 / 255, (float)166 / 255, (float)12 / 255, 1);
        }
        else if(fillAmount <= 0.34)
        {
            healthBar.color = new Color((float)195 / 255, (float)71 / 255, (float)12 / 255, 1);
        }
    }

    public void Die()
    {
        Vector3 deathEffectPosition = new Vector3(transform.position.x, 2.5f, transform.position.z);
        GameObject deathEff = (GameObject)Instantiate(deathEffect, deathEffectPosition, Quaternion.identity);
        Destroy(deathEff, 5f);
        Destroy(gameObject);
    }
}

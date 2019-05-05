using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    double health = 100;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("spawned");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("dead");
        }
    }

    public void reduceHealth(double damage)
    {
        Debug.Log("health reduced");
        health -= damage;
    }
}

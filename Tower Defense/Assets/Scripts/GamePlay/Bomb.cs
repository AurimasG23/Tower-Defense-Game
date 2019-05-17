using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    double damagePerShot = 100;
    double explosionRange = 10;

    private Vector3 targetPosition;

    private float speed = 85f;

    private string enemyTag = "Enemy";

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 dir = targetPosition - transform.position;
        float currentDistance = speed * Time.deltaTime;
        if (dir.magnitude <= currentDistance)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * currentDistance, Space.World);
    }

    public void Seek(Vector3 _targetPosition)
    {
        targetPosition = _targetPosition;
    }

    void HitTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < explosionRange)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.ReduceHealth(damagePerShot);
                }   
            }
        }
        Destroy(gameObject);
    }
}

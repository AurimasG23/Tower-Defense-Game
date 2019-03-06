using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonControl : MonoBehaviour
{
    private Transform target;
    private float range = 15f;

    private float turnSpeed = 10f;

    public Transform partToRotate;

    private string enemyTag = "Enemy";

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target == null)
        {
            return;
        }

        //nearest target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, range);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistace = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistace)
            {
                shortestDistace = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistace <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
}

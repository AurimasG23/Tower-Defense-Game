using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowTowerControl : MonoBehaviour
{
    private Transform target;
    private float range = 20f;
    private float turnSpeed = 10f;
    public Transform partToRotate;
    private string enemyTag = "Enemy";

    private float fireRate = 5f;
    private float fireCountDown = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;   //kulkos atsiradimo vieta

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

        //shooting
        if (fireCountDown <= 0f)
        {
            if(!GamePlayManager.instance.IsGamePaused())
            {
                Shoot();
            }           
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
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
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistace)
            {
                shortestDistace = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistace <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject crossbowBullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        CrossbowTowerBullet bullet = crossbowBullet.GetComponent<CrossbowTowerBullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}

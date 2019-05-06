using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 10f;

    private Transform target;
    private int wayPointIndex = 0;

	// Use this for initialization
	void Start ()
    {
        target = WayPoints.points[0];
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWayPoint();
        }       
	}

    void GetNextWayPoint()
    {
        if(wayPointIndex >= WayPoints.points.Length - 1)
        {
            GamePlayManager.instance.DecreaseLiveCount();
            gameObject.GetComponent<EnemyHealth>().Die();
            return;
        }
        wayPointIndex++;
        target = WayPoints.points[wayPointIndex];
    }   
}

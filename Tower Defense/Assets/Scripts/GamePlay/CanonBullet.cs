﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : MonoBehaviour
{
    private Transform target;

    private float speed = 70f;
	
	// Update is called once per frame
	void Update ()
    {
		if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float currentDistance = speed * Time.deltaTime;

        if(dir.magnitude <= currentDistance)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * currentDistance, Space.World);
	}

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void HitTarget()
    {
        Destroy(gameObject);
        //Destroy(target.gameObject);   //prieso sunaikinimas
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gynimas : MonoBehaviour
{
    public PhysicMaterial slidiMaterial;

	// Use this for initialization
	void Start ()
    {
        slidiMaterial.dynamicFriction = 0;
        slidiMaterial.staticFriction = 0;
        slidiMaterial.bounciness = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.P))
        {
            IsjungtiSliduma();
        }
        if (Input.GetKey(KeyCode.L))
        {
            IjungtiSliduma();
        }
    }

    private void IsjungtiSliduma()
    {
        slidiMaterial.dynamicFriction = 1000000000000000000;
        slidiMaterial.staticFriction = 1000000000000000000;
    }

    private void IjungtiSliduma()
    {
        slidiMaterial.dynamicFriction = 0;
        slidiMaterial.staticFriction = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
    public Vector3 rotateVector;

    [HideInInspector]
    public bool aroundAxis = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(aroundAxis)
        {

        }
        else
        {
            Vector3 finalRot = transform.rotation.eulerAngles;
            finalRot += rotateVector * Time.deltaTime;
            transform.rotation = Quaternion.Euler(finalRot);
        }
	}
}

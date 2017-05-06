using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera cam;

	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        cam.transform.Translate(h, 0, v);
	}
}

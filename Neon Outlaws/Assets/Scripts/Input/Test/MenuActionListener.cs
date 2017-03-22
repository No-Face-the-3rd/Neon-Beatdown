using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputNew;

public class MenuActionListener : MonoBehaviour {
    public PlayerInput pInput;

    public AxisAction moveX, moveY;

    public ButtonAction accept;
    

	// Use this for initialization
	void Start () {
        pInput = GetComponent<PlayerInput>();
        moveX.Bind(pInput.handle);
        moveY.Bind(pInput.handle);
        accept.Bind(pInput.handle);	
	}
	
	// Update is called once per frame
	void Update () {

    }
}

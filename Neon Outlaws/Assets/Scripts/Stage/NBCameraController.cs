using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBCameraController : MonoBehaviour {

    public float minX = 0.0f, maxX = 0.0f;
    public float viewportEdgeBuffer = 0.0f;

    public int num = 1;

    public float curMinX = 0.0f, curMaxX = 0.0f;

    private Camera mainCam;

	// Use this for initialization
	void Start () {
        mainCam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject character = CharacterLocator.locator.getCharacter(num).gameObject;
        float zDist = Mathf.Abs(character.transform.position.z - transform.position.z);
        Vector3 outPos = mainCam.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, zDist));
        curMinX = outPos.x;
        outPos = mainCam.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, zDist));
        curMaxX = outPos.x;
        mainCam.WorldToViewportPoint(character.transform.position);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInitData : MonoBehaviour {
    public Vector3 startingCamera;
    public Vector3[] startingPos;
    public float minX, maxX;

	void Awake () {
        NBCameraController cam = FindObjectOfType<NBCameraController>();
        cam.minX = minX;
        cam.maxX = maxX;
	}
	
	void Update () {
		
	}

    public void resetPositions()
    {
        GameObject tmp = FindObjectOfType<Camera>().gameObject;
        tmp.transform.position = startingCamera;
        for(int i = 0;i< startingPos.Length;i++)
        {
            NBCharacterController character = CharacterLocator.locator.getCharacter(i + 1);
            if(character != null)
            {
                tmp = character.gameObject;
                tmp.transform.position = startingPos[i];
            }
        }
    }

}

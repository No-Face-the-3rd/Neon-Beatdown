using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBCameraController : MonoBehaviour {

    public float minX = 0.0f, maxX = 0.0f;
    public float viewportEdgeBuffer = 0.1f;

    public int num = 1;

    public float curMinX = 0.0f, curMaxX = 0.0f;

    public Vector3 pos;

    private Camera mainCam;

	// Use this for initialization
	void Start () {
        mainCam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        NBCharacterController charControl = CharacterLocator.locator.getCharacter(num);
        if (charControl != null)
        {
            GameObject character = charControl.gameObject;
            float zDist = Mathf.Abs(character.transform.position.z - transform.position.z);
            Vector3 outPos = mainCam.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, zDist));
            curMinX = outPos.x;
            outPos = mainCam.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, zDist));
            curMaxX = outPos.x;
            pos = mainCam.WorldToViewportPoint(character.transform.position);
            if (pos.x < viewportEdgeBuffer || pos.x > 1.0f - viewportEdgeBuffer)
            {
                character.transform.position = mainCam.ViewportToWorldPoint(new Vector3(viewportEdgeBuffer, pos.y, zDist));
                charControl.setState(CharacterState.Idle);
                charControl.doIdle();
            }
        }
    }

    public void checkPan()
    {
        int numChars = CharacterLocator.locator.getNumCharacters();
        List<ControlEdge> edgeCharacters = new List<ControlEdge>();
        bool leftEdge = false, rightEdge = false;
        for (int i = 0; i < numChars; i++)
        {
            int playerNum = i + 1;
            NBCharacterController character = CharacterLocator.locator.getCharacter(playerNum);
            if(character != null)
            {

            }
        }
    }
}

struct ControlEdge
{
    public NBCharacterController character;
    public bool left;
}
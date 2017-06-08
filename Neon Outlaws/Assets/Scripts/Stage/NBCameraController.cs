using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBCameraController : MonoBehaviour {

    public float minX = 0.0f, maxX = 0.0f;
    public float viewportEdgeBuffer = 0.1f;

    [Range(0.0f,1.0f)]
    public float panSpeed = 0.5f;
    public float panOffset = 0.25f;

    private Camera mainCam;

    private Vector3 panTarget;
    private Vector3 prevTarget;
    public AnimationCurve panSpeedCurve;
    public AnimationCurve panOffsetCurve;

	// Use this for initialization
	void Start () {
        mainCam = GetComponent<Camera>();
        panTarget = transform.position;
        prevTarget = panTarget;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        checkPan();
        doPan();
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
            if (character != null)
            {
                GameObject charObj = character.gameObject;
                float zDist = Mathf.Abs(charObj.transform.position.z - transform.position.z);
                Vector3 charPos = mainCam.WorldToViewportPoint(character.transform.position);
                if (charPos.x < viewportEdgeBuffer)
                {
                    leftEdge = true;
                    ControlEdge toAdd = new ControlEdge();
                    toAdd.character = character;
                    toAdd.left = true;
                    toAdd.zDist = zDist;
                    edgeCharacters.Add(toAdd);
                    continue;
                }
                if (charPos.x > 1.0f - viewportEdgeBuffer)
                {
                    rightEdge = true;
                    ControlEdge toAdd = new ControlEdge();
                    toAdd.character = character;
                    toAdd.left = false;
                    toAdd.zDist = zDist;
                    edgeCharacters.Add(toAdd);
                    continue;
                }
            }
        }
        if (leftEdge != rightEdge)
        {
            Vector3 offset = Vector3.zero;

            float curMinX = mainCam.ViewportToWorldPoint(new Vector3(0.0f,
                edgeCharacters[0].character.transform.position.y,
                edgeCharacters[0].zDist)).x;
            float curMaxX = mainCam.ViewportToWorldPoint(new Vector3(1.0f,
                edgeCharacters[0].character.transform.position.y,
                edgeCharacters[0].zDist)).x;
            if ((leftEdge))
            {
                if (curMinX > minX)
                {
                    Debug.Log(curMinX - minX + " Min " + curMinX + " " + minX);
                    offset.x = panOffsetCurve.Evaluate(curMinX - minX);// panOffset;
                }
                else
                {
                    for (int i = 0; i < edgeCharacters.Count; i++)
                    {
                        edgeCharacters[i].character.doIdle();
                        Vector3 charPos = mainCam.WorldToViewportPoint(
                            edgeCharacters[i].character.transform.position);
                        edgeCharacters[i].character.transform.position = mainCam.ViewportToWorldPoint(
                            new Vector3(viewportEdgeBuffer, charPos.y, charPos.z));
                    }
                }
            }
            if (rightEdge)
            {
                if (curMaxX < maxX)
                {
                    Debug.Log(curMaxX - maxX + " max " + curMaxX + " " + maxX);
                    offset.x = panOffsetCurve.Evaluate(curMaxX - maxX); // 1.0f * panOffset;
                }
                else
                {
                    for (int i = 0; i < edgeCharacters.Count; i++)
                    {
                        edgeCharacters[i].character.doIdle();
                        Vector3 charPos = mainCam.WorldToViewportPoint(
                            edgeCharacters[i].character.transform.position);
                        edgeCharacters[i].character.transform.position = mainCam.ViewportToWorldPoint(
                            new Vector3(1.0f - viewportEdgeBuffer, charPos.y, charPos.z));
                    }
                }
            }
            setPanOffset(offset);
        }
        else if (leftEdge == true)
        {
            for (int i = 0; i < edgeCharacters.Count; i++)
            {
                edgeCharacters[i].character.doIdle();
                Vector3 charPos = mainCam.WorldToViewportPoint(
                    edgeCharacters[i].character.transform.position);
                edgeCharacters[i].character.transform.position = mainCam.ViewportToWorldPoint(
                    new Vector3((edgeCharacters[i].left ? viewportEdgeBuffer : 
                    1.0f - viewportEdgeBuffer),charPos.y, charPos.z));
            }
        }
    }


    public void doPan()
    {
        Vector3 location = Vector3.Lerp(mainCam.transform.position, panTarget,
            panSpeedCurve.Evaluate(Vector3.Distance(prevTarget,panTarget)) *
            Vector3.Distance(mainCam.transform.position, panTarget));
        mainCam.transform.position = location;
    }

    public void setPanOffset(Vector3 offset)
    {
        prevTarget = panTarget;
        panTarget = panTarget + offset;
    }
}

struct ControlEdge
{
    public NBCharacterController character;
    public bool left;
    public float zDist;
}
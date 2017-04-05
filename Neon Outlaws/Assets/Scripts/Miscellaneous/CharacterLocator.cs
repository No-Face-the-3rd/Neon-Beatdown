using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocator : MonoBehaviour {
    public static CharacterLocator locator;

    List<NBCharacterController> characters;

	// Use this for initialization
	void Start () {
		if(locator == null)
        {
            locator = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(locator != this)
        {
            Destroy(this.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public NBCharacterController getCharacter(int playerNum)
    {
        int ind = characters.FindIndex(character => character.playerNum == playerNum);
        if (ind >= 0)
        {
            return characters[ind];
        }
        else
        {
            return null;
        }            
    }
}

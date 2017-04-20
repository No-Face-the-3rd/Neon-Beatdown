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
        characters = new List<NBCharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        findCharacters();
	}

    void findCharacters()
    {
        NBCharacterController[] charactersExist = FindObjectsOfType<NBCharacterController>();
        for (int i = 0; i < charactersExist.Length; i++)
        {
            if (!(characters.FindIndex(character => character.playerNum == charactersExist[i].playerNum) >= 0))
            {
                characters.Add(charactersExist[i]);
            }
        }

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

    public int getNumCharacters()
    {
        return characters.Count;
    }

    public void clearLists()
    {
        characters.Clear();
    }
}

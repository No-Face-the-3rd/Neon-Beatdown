using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthBarElements
{
    public Sprite background;
    public Sprite foreground;
    public Texture2D counterFill;
}


public class ObjectDB : MonoBehaviour {
    public static ObjectDB data;

    [SerializeField]
    private List<GameObject> characters;
    [SerializeField]
    private List<GameObject> attacks;
    [SerializeField]
    private List<GameObject> levels;
    [SerializeField]
    private List<HealthBarElements> healthBars;

    [SerializeField]
    private List<GameObject> genericPrefabs;


	// Use this for initialization
	void Start () {
		if(data == null)
        {
            data = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(data != this)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject getCharacter(int index)
    {
        if(withinRange(index,0, characters.Count))
        {
            return characters[index];
        }
        else
        {
            return null;
        }
    }
    
    public GameObject getAttack(int index)
    {
        if(withinRange(index, 0, attacks.Count))
        {
            return attacks[index];
        }
        else
        {
            return null;
        }
    }

    public GameObject getLevel(int index)
    {
        if(withinRange(index,0,levels.Count))
        {
            return levels[index];
        }
        else
        {
            return null;
        }
    }

    public HealthBarElements getHealthbar(int index)
    {
        if(withinRange(index, 0, healthBars.Count))
        {
            return healthBars[index];
        }
        else
        {
            return null;
        }
    }

    public GameObject getPrefab(int index)
    {
        if(withinRange(index, 0,genericPrefabs.Count))
        {
            return genericPrefabs[index];
        }
        else
        {
            return null;
        }
    }

    public bool withinRange(int value, int min, int max)
    {
        if (value >= min && value <= max)
            return true;
        else
            return false;
    }

    public int getNumCharacters()
    {
        return characters.Count;
    }

    public int getNumAttacks()
    {
        return attacks.Count;
    }

    public int getNumLevels()
    {
        return levels.Count;
    }

    public int getNumHealthBars()
    {
        return healthBars.Count;
    }

    public int getNumPrefabs()
    {
        return genericPrefabs.Count;
    }
}

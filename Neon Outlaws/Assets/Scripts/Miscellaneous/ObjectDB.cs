using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthBarElements
{
    
}


public class ObjectDB : MonoBehaviour {
    public static ObjectDB data;

    public List<GameObject> characters;
    public List<GameObject> attacks;
    public List<GameObject> levels;
     


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
}

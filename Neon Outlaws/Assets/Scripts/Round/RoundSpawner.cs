using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSpawner : MonoBehaviour {
    GameObject level;

    void Awake()
    {

    }


	// Use this for initialization
	void Start () {
		
	}

    void spawnStage()
    {
        level = Instantiate(
            ObjectDB.data.getLevel(ObjectDB.data.selectedStage),
            Vector3.zero, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

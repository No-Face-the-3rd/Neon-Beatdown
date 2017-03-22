using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public float[] maxHealths;


    private float[] curHealths;

    public static HealthManager manager;


	// Use this for initialization
	void Start () {

        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(manager != this)
        {
            Destroy(this.gameObject);
        }


        curHealths = new float[maxHealths.Length];
        for(int i =0;i < curHealths.Length;i++)
        {
            curHealths[i] = maxHealths[i];
        }

	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0;i < curHealths.Length;i++)
        {
            curHealths[i] = Mathf.Clamp(curHealths[i], 0.0f, maxHealths[i]);
        }
	}


    public float getP1PercentHealth()
    {
        return curHealths[0] / maxHealths[0];
    }
    public float getP2PercentHealth()
    {
        return curHealths[1] / maxHealths[1];
    }

    public void editP1Health(float health)
    {
        curHealths[0] += health;
    }
    public void editP2Health(float health)
    {
        curHealths[1] += health;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBar
{
    public GameObject objectRef;
    public Slider sliderBar;
    public List<RawImage> counters;

    public HealthBar(GameObject inObject)
    {
        objectRef = inObject;
        sliderBar = inObject.GetComponent<Slider>();
        RawImage[] tmp = inObject.GetComponentsInChildren<RawImage>(true);
        for (int i = 0; i < tmp.Length; i++)
        {
            if(tmp[i].gameObject.name != "Background" && tmp[i].gameObject.name != "Fill")
            {
                counters.Add(tmp[i]);
            }
        }
    }
    public HealthBar()
    {
        objectRef = null;
        sliderBar = null;
        counters = new List<RawImage>();
    }

}


public class HealthbarManager : MonoBehaviour {

    public List<HealthBar> healthBars;

    private bool initialized = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!initialized)
        {
            initializeArt();
        }

	}

    void initializeArt()
    {
        for(int i = 0;i < healthBars.Count;i++)
        {
            healthBars[i] = new HealthBar(healthBars[i].objectRef);
            int character = PlayerLocator.locator.getMenuListener(i + 1).selectedCharacter;
            RawImage background = healthBars[i].objectRef.transform.
                FindChild("Background").GetComponent<RawImage>();
            if(background != null && character >= 0)
            {
                
            }
            RawImage foreground = healthBars[i].objectRef.transform.
                FindChild("Fill").GetComponent<RawImage>();
        }
    }

    void updateValues()
    {

    }
}

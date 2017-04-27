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
		if(initialized)
        {
            updateValues();
        }
        else
        {
            initializeArt();
        }
	}

    void initializeArt()
    {
        bool success = true;
        for(int i = 0;i < healthBars.Count;i++)
        {
            int playerNum = i + 1;
            healthBars[i] = new HealthBar(healthBars[i].objectRef);
            MenuInputListener menuListener = PlayerLocator.locator.getMenuListener(playerNum);
            if(menuListener == null)
            {
                success = false;
                break;
            }
            int character = menuListener.selectedCharacter;
            if (character >= 0)
            {
                Image background = healthBars[i].objectRef.transform.
                    FindChild("Background").GetComponent<Image>();
                HealthBarElements healthBar = ObjectDB.data.getHealthbar(character);
                if (background != null)
                {
                    background.sprite = healthBar.background;
                }
                Image foreground = healthBars[i].objectRef.transform.
                    FindChild("Fill Area").FindChild("Fill").GetComponent<Image>();
                if (foreground != null)
                {
                    foreground.sprite = healthBar.foreground;
                }
            }
        }
        if (success)
        {
            initialized = true;
        }

    }



    void updateValues()
    {
        for (int i = 0; i < healthBars.Count; i++)
        {
            int playerNum = i + 1;
            NBCharacterController character = CharacterLocator.locator.getCharacter(playerNum);
            if (character != null)
            {
                healthBars[i].sliderBar.value = character.getCurHealthPercent();
                PlayerLocator.locator.getMenuListener(character.playerNum).setActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBar
{
    public GameObject objectRef;
    public Slider sliderBar;
    public GameObject background, foreground;
    public List<Image> counters;

    public HealthBar(GameObject inObject)
    {
        objectRef = inObject;
        sliderBar = inObject.GetComponent<Slider>();
        background = sliderBar.transform.FindChild("Background").gameObject;
        foreground = sliderBar.transform.FindChild("Fill Area").FindChild("Fill").gameObject;
        Image[] tmp = inObject.GetComponentsInChildren<Image>(true);
        counters = new List<Image>();
        for (int i = 0; i < tmp.Length; i++)
        {
            if(tmp[i].gameObject.name != "Background" && tmp[i].gameObject.name != "Fill")
            {
                counters.Add(tmp[i]);
            }
        }
        counters.Sort((counter1, counter2) => counter1.name.CompareTo(counter2.name));
    }
    public HealthBar()
    {
        objectRef = null;
        sliderBar = null;
        counters = new List<Image>();
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
        for (int i = 0; i < healthBars.Count; i++)
        {
            int playerNum = i + 1;
            healthBars[i] = new HealthBar(healthBars[i].objectRef);
            MenuInputListener menuListener = PlayerLocator.locator.getMenuListener(playerNum);
            if (menuListener == null)
            {
                success = false;
                break;
            }
            int character = menuListener.selectedCharacter;
            if (character >= 0)
            {
                Image background = healthBars[i].background.GetComponent<Image>();
                HealthBarElements healthBar = ObjectDB.data.getHealthbar(character);
                if (background != null)
                {
                    background.sprite = healthBar.background;
                }
                Image foreground = healthBars[i].foreground.GetComponent<Image>();
                if (foreground != null)
                {
                    foreground.sprite = healthBar.foreground;
                }
                for (int j = 0; j < healthBars[i].counters.Count; j++)
                {
                    Image counter = healthBars[i].counters[j];
                    counter.sprite = healthBar.counterFill;
                    counter.enabled = false;
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

    public void activateMarkers(int playerNum, int numActive)
    {
        for(int i = 0;i < healthBars.Count;i++)
        {
            healthBars[playerNum - 1].counters[i].enabled = (i >= numActive ? false : true);
        }
    }

    public int hasVictor()
    {
        int ret = -1;

        for (int i = 0; i < healthBars.Count; i++)
        {

        }
        return ret;
    }
}

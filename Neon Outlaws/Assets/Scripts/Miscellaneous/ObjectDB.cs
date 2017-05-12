using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthBarElements
{
    public Sprite background;
    public Sprite foreground;
    public Sprite counterFill;
}

[System.Serializable]
public class ourCurve
{
    public string label;
    public AnimationCurve curve;
}


public class ObjectDB : MonoBehaviour
{
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
    private List<ourCurve> curves;

    [SerializeField]
    private List<GameObject> genericPrefabs;

    public int selectedStage = -1;


    // Use this for initialization
    void Start()
    {
        if (data == null)
        {
            data = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (data != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject getCharacter(int index)
    {
        if (withinRange(index, 0, getNumCharacters() - 1))
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
        if (withinRange(index, 0, getNumAttacks() - 1))
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
        if (withinRange(index, 0, getNumLevels() - 1))
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
        if (withinRange(index, 0, getNumHealthBars() - 1))
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
        if (withinRange(index, 0, getNumPrefabs() - 1))
        {
            return genericPrefabs[index];
        }
        else
        {
            return null;
        }
    }
    public AnimationCurve getCurve(int index)
    {
        if(withinRange(index, 0, getNumCurves() - 1))
        {
            return curves[index].curve;
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

    public bool withinRange(float value, float min, float max)
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

    public int getNumCurves()
    {
        return curves.Count;
    }

    public float computeCurve(int curveInd, float time)
    {
        if (withinRange(curveInd, 0, curves.Count - 1))
        {
            float ret = 0.0f;
            if (withinRange(time, curves[curveInd].curve.keys[0].time,
                curves[curveInd].curve.keys[curves[curveInd].curve.keys.Length - 1].time))
            {
                ret = curves[curveInd].curve.Evaluate(time);
            }
            else
            {
                ret = float.NaN;
            }
            return ret;
        }
        else
        {
            return float.NaN;
        }

    }
}

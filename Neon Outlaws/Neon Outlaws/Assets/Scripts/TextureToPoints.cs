using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vector2Serialization

{
    public float x, y;
    public Vector2Serialization(float xIn, float yIn)
    {

        this.x = xIn;

        this.y = yIn;

    }

    public Vector2Serialization(Vector2 vecIn)
    {

        this.x = vecIn.x;

        this.y = vecIn.y;

    }

    public Vector2Serialization(Vector2Serialization vecIn)
    {

        this.x = vecIn.x;

        this.y = vecIn.y;

    }

    public Vector2Serialization()
    {

        this.x = this.y = 0.0f;

    }

    public static implicit operator Vector2Serialization(Vector2 vecIn)
    {

        Vector2Serialization tmp = new Vector2Serialization(vecIn);

        return tmp;

    }

    public static implicit operator Vector2(Vector2Serialization vecIn)
    {

        Vector2 tmp = new Vector2(vecIn.x, vecIn.y);

        return tmp;

    }

}

[System.Serializable]
public class ListWrapperSerialization
{
    public List<Vector2Serialization> subList;
    public int Count { get { return subList.Count; } }

    public static implicit operator ListWrapper(ListWrapperSerialization listIn)
    {
        ListWrapper ret = new ListWrapper();
        ret.subList = new List<Vector2>(System.Array.ConvertAll(listIn.subList.ToArray(), delegate (Vector2Serialization v) { return (Vector2)v; }));
        return ret;
    }

    public static implicit operator ListWrapperSerialization(ListWrapper listIn)
    {
        ListWrapperSerialization ret = new ListWrapperSerialization();
        ret.subList = new List<Vector2Serialization>(System.Array.ConvertAll(listIn.subList.ToArray(), delegate (Vector2 v) { return (Vector2Serialization)v; }));
        return ret;
    }

    public static implicit operator Vector2[](ListWrapperSerialization listIn)
    {
        Vector2[] ret = System.Array.ConvertAll(listIn.subList.ToArray(), delegate (Vector2Serialization v) { return (Vector2)v; });

        return ret;
    }
    public static implicit operator ListWrapperSerialization(Vector2Serialization[] listIn)
    {
        ListWrapperSerialization ret = new ListWrapperSerialization();
        ret.subList = new List<Vector2Serialization>(listIn);
        return ret;
    }
}


public class ListWrapper
{
    public List<Vector2> subList;
}

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TextureToPoints : MonoBehaviour {

    public Texture2D spriteSheet;

    public Sprite[] sprites;

    public List<ListWrapperSerialization> colliderPoints = new List<ListWrapperSerialization>();

    [Range(0,1)]
    public float alphaThresh = 0.0f;
    public float distThresh = 0.0f;

    [Range(-1, 1)]
    public int centerX = 0;

    [Range(-1, 1)]
    public int centerY = -1;

    public float pixelScale = 1.0f;


    public int changeInd = 0;
    [SerializeField]
    private int curInd = 0;
    [SerializeField]
    private int prevInd = 0;
    
    private PolygonCollider2D poly;
    private LineRenderer lineRend;

    public bool showHitVisualizer = false;

    public SpriteRenderer sprRend;

	// Use this for initialization
	void Start () {
        getPoly();
        getLineRend();
        if (colliderPoints.Count >= 1 && poly != null)
            setPath(curInd);
        sprRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        showHitVisualization();
        string spriteInd = sprRend.sprite.name.Replace(sprRend.sprite.texture.name + "_", "");
        setPath(int.Parse(spriteInd));
	}

    public Texture2D getSpriteSheet()
    {
        return spriteSheet;
    }

    public void setSprites(Sprite[] spritesIn)
    {
        sprites = spritesIn;
    }

    public Sprite[] getSprites()
    {
        return sprites;
    }

    public Vector2[] getFrameColliderPoints(int index)
    {
        return colliderPoints[index];
    }

    public void setFrameColliderPoints(List<List<Vector2>> pointsIn)
    {
        List<ListWrapperSerialization> tmp = new List<ListWrapperSerialization>();
        for (int i = 0; i < pointsIn.Count; i++)
        {
            Vector2Serialization[] tmpArr = System.Array.ConvertAll(pointsIn[i].ToArray(), delegate (Vector2 v) { return (Vector2Serialization)v; });
            tmp.Add(tmpArr);
        }
        colliderPoints = tmp;
    }

    public void setPath(int index)
    {
        if (changeInd != index)
        {
            changeInd = index;
            if (colliderPoints.Count > 0)
            {
                poly.SetPath(0, System.Array.ConvertAll(colliderPoints[index].subList.ToArray(), delegate (Vector2Serialization v) { return (Vector2)v; }));
            }
        }
    }

    public void updatePath()
    {
        setPath(curInd);
    }

    public void getPoly()
    {
        poly = GetComponent<PolygonCollider2D>();
    }

    public void toggleHitVisualization()
    {
        showHitVisualizer = !showHitVisualizer;
    }

    void showHitVisualization()
    {
        if(showHitVisualizer)
        {
            if (curInd != changeInd)
                curInd = changeInd;
            setLineRend();
        }
        else
        {
            clearLineRend();
            if (curInd != -1)
            { 
                curInd = -1;
            }
        }
        prevInd = curInd;
    }
    public void getLineRend()
    {
        lineRend = GetComponent<LineRenderer>();
    }

    void setLineRend()
    {
        if (prevInd != curInd && curInd != -1)
        {
            List<Vector3> tmp = new List<Vector3>();
            for (int i = 0; i < colliderPoints[curInd].Count; i++)
            {
                tmp.Add(new Vector3(colliderPoints[curInd].subList[i].x, colliderPoints[curInd].subList[i].y, -0.01f));
            }
            tmp.Add(new Vector3(colliderPoints[curInd].subList[0].x, colliderPoints[curInd].subList[0].y, -0.01f));
            lineRend.numPositions = tmp.Count;
            lineRend.SetPositions(tmp.ToArray());

        }
    }
    void clearLineRend()
    {
        if (prevInd != -1)
        {
            lineRend.numPositions = 0;
            Vector3[] tmp = new Vector3[0];
            lineRend.SetPositions(tmp);

        }
    }
}

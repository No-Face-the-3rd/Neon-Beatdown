using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextureToPoints))]
[CanEditMultipleObjects]
public class TextureToPointsInspector : Editor {
    private TextureToPoints data;


    public override void OnInspectorGUI()
    {
        data = (TextureToPoints)target;
        base.OnInspectorGUI();

        if(GUILayout.Button("Make Colliders"))
        {
            data.getPoly();
            data.setFrameColliderPoints(getFrameColliderPointsSprites(data.getSprites()));
        }

        if(GUILayout.Button("UpdateSprites"))
        {
            data.setSprites(pullSprites(data.getSpriteSheet()));
        }

        if(GUILayout.Button("Test Change Collider"))
        {
            data.getPoly();
            data.updatePath();
        }
        if(GUILayout.Button("Toggle Hit Zone"))
        {
            data.toggleHitVisualization();
        }


        EditorUtility.SetDirty(target);
    }


    public Sprite[] pullSprites(Texture2D spriteSheet)
    {
        string path = AssetDatabase.GetAssetPath(spriteSheet);
        var texAndSprites = AssetDatabase.LoadAllAssetsAtPath(path);
        List<Sprite> ret = new List<Sprite>();
        for(int i = 1;i < texAndSprites.Length;i++)
        {
            ret.Add((Sprite)texAndSprites[i]);
        }
        return ret.ToArray();
    }

    public List<List<Vector2>> getFrameColliderPointsSprites(Sprite[] sprites)
    {
        List<List<Vector2>> ret = new List<List<Vector2>>();
        TextureToPointsEditor.data = data;
        for (int i = 0; i < sprites.Length; i++)
        {
            ret.Add(TextureToPointsEditor.getOutline(sprites[i]));
        }
        return ret;
    }
}

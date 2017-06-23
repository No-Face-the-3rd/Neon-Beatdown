using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLineRenderer : MonoBehaviour {

    public List<Vector3> positions = new List<Vector3>();
    public float lineWidth = 0.1f;
    public Color lineColor = Color.white;
    public bool drawLine = false;

    public Shader lineShader;

    public Material lineMaterial;


    private Mesh lineMesh;

    private bool shouldUpdate = false;
    public float changeThreshold = 0.01f;

    void Reset()
    {
        lineShader = Shader.Find("Unlit/Color");
    }

	// Use this for initialization
	void Start () {
        if (lineMaterial == null)
            lineMaterial = new Material(lineShader);
        lineMesh = new Mesh();
	}
    	
	// Update is called once per frame
	void Update () {

	}

    void OnRenderObject()
    {
        updateMesh();

        drawMesh();
    }

    private void updateMesh()
    {
        if(shouldUpdate)
        {
            updateMeshVerts();
            lineMesh.RecalculateBounds();
            lineMaterial.color = lineColor;
            shouldUpdate = false;
        }
    }

    private void updateMeshVerts()
    {

        lineMesh.Clear();
        Vector3[] verts = new Vector3[positions.Count * 2];
        int[] tris = new int[(positions.Count > 0 ?
            (positions.Count - 1) : 0) * 12];
        float halfWidth = lineWidth / 2.0f;
        Vector3 norm = Vector3.zero;

        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 finalTop = new Vector3();
            Vector3 finalBot = new Vector3();
            Vector3 topOne = new Vector3();
            Vector3 topTwo = new Vector3();
            Vector3 botOne = new Vector3();
            Vector3 botTwo = new Vector3();
            if (i < positions.Count - 1)
            {
                tris[i * 6 + 0] = i * 2 + 0;
                tris[i * 6 + 1] = i * 2 + 1;
                tris[i * 6 + 2] = i * 2 + 2;
                tris[i * 6 + 3] = i * 2 + 1;
                tris[i * 6 + 4] = i * 2 + 3;
                tris[i * 6 + 5] = i * 2 + 2;
                int backface = (positions.Count - 1) * 6;
                tris[backface + i * 6 + 0] = i * 2 + 0;
                tris[backface + i * 6 + 1] = i * 2 + 2;
                tris[backface + i * 6 + 2] = i * 2 + 1;
                tris[backface + i * 6 + 3] = i * 2 + 1;
                tris[backface + i * 6 + 4] = i * 2 + 2;
                tris[backface + i * 6 + 5] = i * 2 + 3;
            }
            if (i > 0 && i < positions.Count - 1)
            {
                topOne = positions[i] +
                    getSegmentDir(positions[i - 1], positions[i], ref norm) * halfWidth;
                topTwo = positions[i] +
                    getSegmentDir(positions[i], positions[i + 1], ref norm) * halfWidth;
                botOne = positions[i] -
                    getSegmentDir(positions[i - 1], positions[i], ref norm) * halfWidth;
                botTwo = positions[i] -
                    getSegmentDir(positions[i], positions[i + 1], ref norm) * halfWidth;
            }
            else if (i == 0)
            {
                topOne = topTwo = positions[i] +
                    getSegmentDir(positions[i], positions[i + 1], ref norm) * halfWidth;
                botOne = botTwo = positions[i] +
                    getSegmentDir(positions[i], positions[i + 1], ref norm) * -halfWidth;

            }
            else if (i == positions.Count - 1)
            {
                topOne = topTwo = positions[i] +
                    getSegmentDir(positions[i - 1], positions[i], ref norm) * halfWidth;
                botOne = botTwo = positions[i] +
                    getSegmentDir(positions[i - 1], positions[i], ref norm) * -halfWidth;
            }
            finalTop = (topOne + topTwo) / 2.0f;
            finalBot = (botOne + botTwo) / 2.0f;

            verts[i * 2 + 0] = finalTop;
            verts[i * 2 + 1] = finalBot;
        }
        lineMesh.vertices = verts;
        lineMesh.triangles = tris;
    }


    private Vector3 getSegmentDir(Vector3 pointOne, Vector3 pointTwo, ref Vector3 normal)
    {
        if (normal == Vector3.zero)
            normal = Vector3.Cross(pointOne, pointTwo).normalized;
        Vector3 ret = Vector3.Cross(normal, pointTwo - pointOne);
        return ret.normalized;
    }

    private void drawMesh()
    {
        if(drawLine)
        {
            if(lineMaterial.SetPass(0))
            {
                Graphics.DrawMeshNow(lineMesh, Matrix4x4.TRS(transform.position,
                    transform.rotation, transform.lossyScale));
            }
        }
    }

    public void setPoints(List<Vector3> points)
    {
        shouldUpdate |= positions.Count != points.Count;
        if(!shouldUpdate)
        {
            for(int i = 0;i < positions.Count;i++)
            {
                shouldUpdate |= Vector3.Distance(positions[i], points[i]) > Mathf.Abs(changeThreshold);
                if (shouldUpdate)
                    break;
            }
        }
        positions = new List<Vector3>(points);
    }
}

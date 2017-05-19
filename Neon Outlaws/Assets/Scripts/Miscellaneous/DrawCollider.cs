using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCollider : MonoBehaviour {

    public List<Vector3> positions = new List<Vector3>();
    public float lineWidth = 0.1f;
    public Color lineColor = Color.white;
    public bool drawLine = false;

    public Shader lineShader;

    public Material lineMaterial;

    private Mesh lineMesh;

    private bool shouldUpdate = false;
    public float changeThreshold = 0.1f;


	// Use this for initialization
	void Start () {
        if (lineShader == null)
            lineShader = Shader.Find("Standard");
        if (lineMaterial == null)
            lineMaterial = new Material(lineShader);
        lineMesh = new Mesh();
	}
    	
	// Update is called once per frame
	void Update () {
        updateMesh();

        drawMesh();
	}

    private void updateMesh()
    {
        if(shouldUpdate)
        {
            updateVerts();
            updateTris();
            lineMesh.RecalculateBounds();
            shouldUpdate = false;
        }
    }

    private void updateVerts()
    {
        if (positions.Count > 1)
        {
            Vector3[] verts = new Vector3[positions.Count * 2];
            float halfWidth = lineWidth / 2.0f;

            for (int i = 0; i < positions.Count; i++)
            {
                Vector3 finalTop = new Vector3();
                Vector3 finalBot = new Vector3();
                Vector3 topOne = new Vector3();
                Vector3 topTwo = new Vector3();
                Vector3 botOne = new Vector3();
                Vector3 botTwo = new Vector3();
                if (i > 0 && i < positions.Count - 1)
                {
                    topOne = transform.InverseTransformPoint(positions[i] +
                        getSegmentDir(positions[i - 1], positions[i]) * halfWidth);
                    topTwo = transform.InverseTransformPoint(positions[i] +
                        getSegmentDir(positions[i], positions[i + 1]) * halfWidth);
                    botOne = transform.InverseTransformPoint(positions[i] -
                        getSegmentDir(positions[i - 1], positions[i]) * halfWidth);
                    botTwo = transform.InverseTransformPoint(positions[i] -
                        getSegmentDir(positions[i], positions[i + 1]) * halfWidth);
                }
                else if (i == 0)
                {
                    topOne = topTwo = transform.InverseTransformPoint(positions[i] +
                        getSegmentDir(positions[i], positions[i + 1]) * halfWidth);
                    botOne = botTwo = transform.InverseTransformPoint(positions[i] +
                        getSegmentDir(positions[i], positions[i + 1]) * -halfWidth);

                }
                else if (i == positions.Count - 1)
                {
                    topOne = topTwo = transform.InverseTransformPoint(positions[i] +
                        getSegmentDir(positions[i - 1], positions[i]) * halfWidth);
                    botOne = botTwo = transform.InverseTransformPoint(positions[i] +
                        getSegmentDir(positions[i - 1], positions[i]) * -halfWidth);
                }
                finalTop = (topOne + topTwo) / 2.0f;
                finalBot = (botOne + botTwo) / 2.0f;

                verts[i * 2 + 0] = finalTop;
                verts[i * 2 + 1] = finalBot;
            }
            lineMesh.vertices = verts;
        }

    }

    private Vector3 getSegmentDir(Vector3 pointOne, Vector3 pointTwo)
    {
        Vector3 normal = Vector3.Cross(pointOne, pointTwo);
        Vector3 ret = Vector3.Cross(normal, pointTwo - pointOne);
        ret.Normalize();

        return ret;
    }

    private void updateTris()
    {
        if(positions.Count > 1)
        {
            int[] tris = new int[(positions.Count - 1) * 6];
            for (int i = 0; i < positions.Count; i++)
            {
                tris[i * 6 + 0] = i * 6 + 0;
                tris[i * 6 + 1] = i * 6 + 1;
                tris[i * 6 + 2] = i * 6 + 2;
                tris[i * 6 + 3] = i * 6 + 1;
                tris[i * 6 + 4] = i * 6 + 3;
                tris[i * 6 + 5] = i * 6 + 2;
            }
            lineMesh.triangles = tris;
        }
    }

    private void drawMesh()
    {
        if(drawLine)
        {
            Graphics.DrawMesh(lineMesh, transform.localToWorldMatrix, lineMaterial, 0);
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

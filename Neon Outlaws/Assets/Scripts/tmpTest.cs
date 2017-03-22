using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpTest : MonoBehaviour {

    public Collider2D coll;

	// Use this for initialization
	void Start () {
        coll = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        List<Vector2> points = new List<Vector2>();

        points.Add(Vector2.zero);
        points.Add(new Vector2(0, 1));
        points.Add(new Vector2(1, 1));
        points.Add(new Vector2(1.5f, 0.5f));
        points.Add(new Vector2(1, 0));
        points.Add(new Vector2(0.5f, 0.5f));


        ((PolygonCollider2D)coll).SetPath(0, points.ToArray());
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OutlineMaker
{
    public Texture2D basis;

    List<EdgeLoop> loops;
    public OutlineMaker(Texture2D basisIn)
    {
        loops = new List<EdgeLoop>();
        basis = basisIn;
    }
    public void setMaker(float alphThreshold, float distThreshold)
    {
        Color[] pixels = basis.GetPixels();
        int imgWidth = basis.width;
        int imgHeight = basis.height;

        float uvWidth = (float)imgWidth;
        float uvHeight = (float)imgHeight;

        List<Edge> edges = new List<Edge>();

        for (int i = 0; i < imgWidth; i++)
        {
            float uvX = i + 0.5f;
            for (int j = 0; j < imgHeight; j++)
            {
                float curAlph = pixels[imgWidth * j + i].a;

                if (curAlph < alphThreshold)
                    continue;
                float uvY = j + 0.5f;

                float[] surAlphs = getAlphas(ref pixels, i, j, imgWidth, imgHeight);

                bool[] meetThresh = new bool[surAlphs.Length];

                for (int k = 0; k < surAlphs.Length; k++)
                    meetThresh[k] = surAlphs[k] >= alphThreshold;

                bool cur = meetThresh[(int)Location.ABOVE];
                bool check = (!meetThresh[(int)Location.UPPRIGHT] && !meetThresh[(int)Location.RIGHT] && (meetThresh[(int)Location.LEFT] || meetThresh[(int)Location.UPPLEFT]))
                    || (!meetThresh[(int)Location.UPPLEFT] && !meetThresh[(int)Location.LEFT] && (meetThresh[(int)Location.UPPRIGHT] || meetThresh[(int)Location.RIGHT]));

                if (cur && check)
                {
                    Edge edge = new Edge(new Vertex(i, j, uvX / uvWidth, uvY / uvHeight), new Vertex(i, j + 1, uvX / uvWidth, (uvY + 1) / uvHeight));
                    edges.Add(edge);
                }
                cur = meetThresh[(int)Location.UPPRIGHT];
                check = (meetThresh[(int)Location.ABOVE] && !meetThresh[(int)Location.RIGHT]) || (meetThresh[(int)Location.RIGHT] && !meetThresh[(int)Location.ABOVE]);
                if (cur && check)
                {
                    Edge edge = new Edge(new Vertex(i, j, uvX / uvWidth, uvY / uvHeight), new Vertex(i + 1, j + 1, (uvX + 1) / uvWidth, (uvY + 1) / uvHeight));
                    edges.Add(edge);
                }
                cur = meetThresh[(int)Location.RIGHT];
                check = (!meetThresh[(int)Location.ABOVE] && !meetThresh[(int)Location.UPPRIGHT] && (meetThresh[(int)Location.BELOW] || meetThresh[(int)Location.BELRight]))
                    || (!meetThresh[(int)Location.BELOW] && !meetThresh[(int)Location.BELRight] && (meetThresh[(int)Location.ABOVE] || meetThresh[(int)Location.UPPRIGHT]));
                if (cur && check)
                {
                    Edge edge = new Edge(new Vertex(i, j, uvX / uvWidth, uvY / uvHeight), new Vertex(i + 1, j, (uvX + 1) / uvWidth, uvY / uvHeight));
                    edges.Add(edge);
                }
                cur = meetThresh[(int)Location.BELRight];
                check = (meetThresh[(int)Location.RIGHT] && !meetThresh[(int)Location.BELOW]) || (meetThresh[(int)Location.BELOW] && !meetThresh[(int)Location.RIGHT]);
                if (cur && check)
                {
                    Edge edge = new Edge(new Vertex(i, j, uvX / uvWidth, uvY / uvHeight), new Vertex(i + 1, j - 1, (uvX + 1) / uvWidth, (uvY - 1) / uvHeight));
                    edges.Add(edge);
                }
            }
        }
        makeLoops(edges);
        smoothEdges(distThreshold);
        reduceRedundantVertices();
        sort();
        shiftToMin(0);
    }

    public enum Location
    {
        ABOVE = 0, BELOW, RIGHT, LEFT, UPPRIGHT, UPPLEFT, BELRight
    }


    public float[] getAlphas(ref Color[] pixels, int x, int y, int imgWidth, int imgHeight)
    {
        float[] alphas = new float[7];
        if (y < imgHeight - 1)
            alphas[(int)Location.ABOVE] = pixels[imgWidth * (y + 1) + x].a;
        if (y > 0)
            alphas[(int)Location.BELOW] = pixels[imgWidth * (y - 1) + x].a;
        if (x < imgWidth - 1)
            alphas[(int)Location.RIGHT] = pixels[imgWidth * y + (x + 1)].a;
        if (x > 0)
            alphas[(int)Location.LEFT] = pixels[imgWidth * y + (x - 1)].a;
        if (x < imgWidth - 1 && y < imgHeight - 1)
            alphas[(int)Location.UPPRIGHT] = pixels[imgWidth * (y + 1) + (x + 1)].a;
        if (x > 0 && y < imgHeight - 1)
            alphas[(int)Location.UPPLEFT] = pixels[imgWidth * (y + 1) + (x - 1)].a;
        if (x < imgWidth - 1 && y > 0)
            alphas[(int)Location.BELRight] = pixels[imgWidth * (y - 1) + (x + 1)].a;
        return alphas;
    }

    public void makeLoops(List<Edge> edges)
    {
        EdgeLoop curLoop = startLoop(ref edges);
        loops.Add(curLoop);

        int prev = edges.Count;
        while (edges.Count > 0)
        {
            List<Edge> toDelete = new List<Edge>();
            for (int i = 0; i < edges.Count; i++)
            {
                if (curLoop.checkClosed())
                {
                    EdgeLoop nextLoop = startLoop(ref edges);
                    loops.Add(nextLoop);
                    curLoop = nextLoop;
                }
                int check = curLoop.addEdge(edges[i]);
                if (check > -1)
                {
                    toDelete.Add(edges[i]);
                }
            }
            if (toDelete.Count == 0 && edges.Count == prev && curLoop.getLength() < 3)
            {
                loops.Remove(curLoop);
                curLoop = loops[0];
                prev = -1;
                continue;
            }
            for (int i = 0; i < toDelete.Count; i++)
            {
                edges.Remove(toDelete[i]);
            }
            prev = edges.Count;
        }
    }
    public Vector2[] getVertices()
    {
        List<Vector2> vertices = new List<Vector2>();
        foreach (EdgeLoop loop in loops)
        {
            vertices.AddRange(loop.getVertices());
        }
        return vertices.ToArray();
    }

    public Vector2[] getLoopVertices(int index)
    {
        Vector2[] vertices = loops[index].getVertices();
        return vertices;
    }
    

    public void reduceRedundantVertices()
    {
        foreach (EdgeLoop loop in loops)
        {
            loop.reduceRedundantVertices();
        }
    }
    public void smoothEdges(float threshold)
    {
        foreach (EdgeLoop loop in loops)
        {
            loop.smoothEdge(threshold);
        }
    }
    public EdgeLoop startLoop(ref List<Edge> edges)
    {
        EdgeLoop loop = new EdgeLoop(edges[0]);
        edges.RemoveAt(0);
        return loop;
    }
    public bool multipleLoops()
    {
        if (loops.Count > 1)
            return true;
        return false;
    }
    public int getNumLoops()
    {
        return loops.Count;
    }

    public void sort()
    {
        loops.Sort((x, y) => -x.getLength().CompareTo(y.getLength()));
    }

    public Vector2 getUV(int loopInd, int vertInd)
    {
        return loops[loopInd].getUV(vertInd);
    }

    public Vector2 getMin(int loopInd)
    {
        return loops[loopInd].getMin();
    }
    public void shiftToMin(int loopInd)
    {
        loops[loopInd].shiftToMin();
    }
    public void shiftToMin()
    {
        foreach (EdgeLoop loop in loops)
        {
            loop.shiftToMin();
        }
    }
    public Vector2 getMax(int loopInd)
    {
        return loops[loopInd].getMax();
    }
}

public class Vertex
{
    public Vector2 offset;
    public Vector2 uv;

    public Vertex(float x, float y, float u, float v)
    {
        offset = new Vector2(x, y);
        uv = new Vector2(u, v);
    }
    public Vertex(Vector2 positionIn, Vector2 uvIn)
    {
        offset = positionIn;
        uv = uvIn;
    }

    public static bool operator ==(Vertex a, Vertex b)
    {
        return a.offset == b.offset && a.uv == b.uv;
    }
    public static bool operator !=(Vertex a, Vertex b)
    {
        return a.offset != b.offset || a.uv != b.uv;
    }
}

public class Edge
{
    public Vertex firstVert, secondVert;

    public Edge(Vertex firstVertIn, Vertex secondVertIn)
    {
        firstVert = firstVertIn;
        secondVert = secondVertIn;
    }
    public void swapVerts()
    {
        Vertex tmp = firstVert;
        firstVert = secondVert;
        secondVert = tmp;
    }

    public int compareConnect(Edge target)
    {
        if (this.firstVert == target.secondVert)
            return 0;
        else if (this.secondVert == target.secondVert)
            return 1;
        else if (this.secondVert == target.firstVert)
            return 2;
        else if (this.firstVert == target.firstVert)
            return 3;
        return -1;
    }
    public Vector2 getDirection()
    {
        return (secondVert.offset - firstVert.offset).normalized;
    }
}

public class EdgeLoop
{
    public List<Edge> edges;
    private bool isClosed = false;
    public Vector2 getMin()
    {
        Vector2 ret = new Vector2(Mathf.Infinity, Mathf.Infinity);
        foreach (Edge edge in edges)
        {
            ret.x = edge.firstVert.offset.x < ret.x ? edge.firstVert.offset.x : ret.x;
            ret.y = edge.firstVert.offset.y < ret.y ? edge.firstVert.offset.y : ret.y;
        }
        return ret;
    }

    public void shiftToMin()
    {
        Vector2 min = getMin();
        for (int i = 0; i < edges.Count - 1; i++)
        {
            edges[i].firstVert.offset -= min;
        }
    }
    public Vector2 getMax()
    {
        Vector2 ret = new Vector2(Mathf.NegativeInfinity, Mathf.NegativeInfinity);
        foreach (Edge edge in edges)
        {
            ret.x = edge.firstVert.offset.x > ret.x ? edge.firstVert.offset.x : ret.x;
            ret.y = edge.firstVert.offset.y > ret.y ? edge.firstVert.offset.y : ret.y;
        }
        return ret;
    }
    public EdgeLoop(Edge edge)
    {
        edges = new List<Edge>();
        edges.Add(edge);
    }
    public int getLength()
    {
        return edges.Count;
    }
    public int addEdge(Edge edge)
    {
        //Compares to the last edge
        int compare = edge.compareConnect(edges[edges.Count - 1]);
        if (compare > -1 && compare < 2)
        {
            if (compare == 1)
            {
                edge.swapVerts();
            }
            edges.Add(edge);
        }
        else
        {
            return -1;
        }
        //Compares to the first edge
        if (getLength() > 2)
        {
            compare = edge.compareConnect(edges[0]);
            if (compare > 1)
            {
                if (compare == 3)
                {
                    edge.swapVerts();
                }
                isClosed = true;
                edges.Insert(0, edge);
                return 1;
            }
        }
        return 0;
    }
    public bool checkClosed()
    {
        return isClosed;
    }
    public Vector2[] getVertices()
    {
        int size = edges.Count - (isClosed ? 1 : 0);
        Vector2[] vertices = new Vector2[size];
        for (int i = 0; i < size; i++)
        {
            Vertex vert = edges[i].firstVert;
            vertices[i] = new Vector2(vert.offset.x, vert.offset.y);
        }
        return vertices;
    }
    public void reduceRedundantVertices()
    {
        List<Edge> newEdges = new List<Edge>();
        Edge curEdge = edges[0];
        for (int i = 1; i < edges.Count; i++)
        {
            Edge test = edges[i];

            if (curEdge.getDirection() == test.getDirection())
            {
                curEdge.secondVert = test.secondVert;
            }
            else
            {
                newEdges.Add(curEdge);
                curEdge = test;
            }
        }
        newEdges.Add(curEdge);
        edges = newEdges;
    }
    public void smoothEdge(float threshold)
    {
        if (threshold > 0.0f)
        {
            List<Edge> newEdges = new List<Edge>();
            Edge curEdge = edges[0];
            for (int i = 1; i < edges.Count; i++)
            {
                Edge test = edges[i];

                if (Vector2.Distance(curEdge.firstVert.offset, test.secondVert.offset) < threshold)
                {
                    curEdge.secondVert = test.secondVert;
                }
                else
                {
                    newEdges.Add(curEdge);
                    curEdge = test;
                }
            }
            newEdges.Add(curEdge);
            edges = newEdges;
        }
    }

    public Vector2 getUV(int index)
    {
        return edges[index].firstVert.uv;
    }

}

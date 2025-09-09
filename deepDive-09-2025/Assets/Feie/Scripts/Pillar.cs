using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [Range(0f, 50f)]
    public float height;
    private bool isTransforming;
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] currentVertices;
    private float targetHeight;
    private float smoothSpeed = 2f; // Adjust for speed of height change

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        currentVertices = mesh.vertices;
    }

    void Update()
    {
        if (isTransforming)
        {
            bool reached = true;
            for (int i = 0; i < currentVertices.Length; i++)
            {

                float maxY = float.NegativeInfinity;
                foreach (var v in originalVertices)
                {
                    if (v.y > maxY) maxY = v.y;
                }

                if (Mathf.Abs(originalVertices[i].y - maxY) < 0.01f)
                {
                    Vector3 targetVertex = new Vector3(currentVertices[i].x, targetHeight, currentVertices[i].z);
                    Vector3 newVertex = Vector3.MoveTowards(currentVertices[i], targetVertex, smoothSpeed * Time.deltaTime);
                    if (Vector3.Distance(newVertex, targetVertex) > 0.001f)
                    {
                        reached = false;
                    }
                    currentVertices[i] = newVertex;
                }
            }

            mesh.vertices = currentVertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            if (reached)
            {
                isTransforming = false;
            }
        }
    }

    public void SetHeight(float targetHeight = 0)
    {
        if (targetHeight == 0)
        {
            targetHeight = height;
        }
        this.targetHeight = targetHeight;
        isTransforming = true;
    }
}
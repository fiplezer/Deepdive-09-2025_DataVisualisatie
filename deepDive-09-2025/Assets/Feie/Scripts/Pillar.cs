using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Pillar : MonoBehaviour
{
    public int index;
    public float weight;
    public float height;
    public bool isMale;
    public int age;

    private bool isTransforming;
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] currentVertices;
    private float targetobjHeight;
    [SerializeField]
    private float smoothSpeed = 2f;

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
            Debug.Log("ITS TRANSFORMINGG");
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
                    Vector3 targetVertex = new Vector3(currentVertices[i].x, targetobjHeight, currentVertices[i].z);
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

    public void updatePillar()
    {
        // SetIndex(veriables.GetIndex());
        // SetWeight(veriables.GetWeight());
        // SetHeight(veriables.GetHeight());
        // SetGender(veriables.GetGender()); 
        // SetAge(veriables.GetAge());

    }
    public void SetIndex(int newIndex)
    {
        index = newIndex;
    }
    public void SetWeight(float newWeight)
    {
        weight = newWeight;
        float nmbr = newWeight / 300;
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x + nmbr, scale.y, scale.z + nmbr);
    }


    public void SetHeight(float newHeight)
    {
        height = newHeight;
        if (targetobjHeight == 0)
        {
            targetobjHeight = newHeight / 2;
        }
        isTransforming = true;

    }
    public void SetAge(int newAge)
    {
        age = newAge;
        float r = GetComponent<MeshRenderer>().material.color.r;
        float g = GetComponent<MeshRenderer>().material.color.g;
        float b = GetComponent<MeshRenderer>().material.color.b;

        float nmbr = (float)newAge / 100;

        GetComponent<MeshRenderer>().material.color = new Color(r - nmbr, g - nmbr, b - nmbr);

    }
    public void SetGender(bool newIsMale)
    {
        if (newIsMale)
        {
            GetComponent<MeshRenderer>().material.color = new Color(0, 1, 1);
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = new Color(1f, 0, 1f);
        }
        isMale = newIsMale;

    }
}
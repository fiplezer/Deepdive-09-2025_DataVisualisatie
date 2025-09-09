using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PillarGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject pillar;

    public List<GameObject> placedPillars;

    // Start is called before the first frame update
    void Start()
    {
        placedPillars = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Vector3 position = child.transform.position;
            Vector3 newPosition = new Vector3(position.x, -0.11f, position.z);
            // placedPillars.Add(Instantiate(pillar, newPosition, Quaternion.identity, transform));
            if (child.tag == "PillarPlacer")
            {
                child.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

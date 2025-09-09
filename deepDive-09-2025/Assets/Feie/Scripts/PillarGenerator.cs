using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PillarGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject pillar;
    [SerializeField]
    private Button buttonScript;

    public List<GameObject> placedPillars;

    // Start is called before the first frame update
    void Start()
    {
        placedPillars = new List<GameObject>();
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "PillarPlacer")
            {
                Vector3 position = child.transform.position;
                Vector3 newPosition = new Vector3(position.x, -0.11f, position.z);
                GameObject newPillar = Instantiate(pillar, newPosition, Quaternion.identity, transform);
                Pillar newPillarScript = newPillar.GetComponent<Pillar>();
                newPillar.GetComponent<Pillar>().height = Random.Range(0f, 50f); // Give height value
                
                placedPillars.Add(newPillar);
                buttonScript.AddPillarScript(newPillarScript);
                child.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

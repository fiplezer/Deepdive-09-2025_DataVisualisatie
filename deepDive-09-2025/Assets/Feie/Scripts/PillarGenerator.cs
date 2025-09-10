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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlacePilars(VariableManager.Root root)
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


                for (int j = 0; j < childCount; j++)
                {

                    newPillarScript.SetIndex(i);
                    newPillarScript.SetWeight(root.data[i].bodyweight_kg_all_m_1_T1_M);
                    newPillarScript.SetHeight(root.data[i].bodylength_cm_all_m_1_T1_M - 130);
                    if (root.data[i].gender_T1_M == "MALE")
                    {
                        newPillarScript.SetGender(true);
                    }
                    else
                    {
                        newPillarScript.SetGender(false);
                    }
                    newPillarScript.SetAge(root.data[i].age_1a_q_1 / 12);
                    break;

                }

                placedPillars.Add(newPillar);
                buttonScript.AddPillarScript(newPillarScript);
                child.SetActive(false);
            }
        }
    }
}

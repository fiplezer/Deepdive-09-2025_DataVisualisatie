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

    public void PlacePilars(VariableManagerScript.Root root)
    {
        placedPillars = new List<GameObject>();
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "PillarPlacer")
            {
                VariableManagerScript.VeriableData data = root.data[i];
                CreatePilar(child, (int)data.zip_code_T1_M, (float)data.bodyweight_kg_all_m_1_T1_M, (float)data.bodylength_cm_all_m_1_T1_M, data.gender_T1_M, (int)data.age_1a_q_1, (float)data.kcal_intake_adu_c_1_T1_QF);
            }
        }
    }



    public void CreateAvaragePilar(Vector3 pos, int zip, float weight, float height, bool ismale, int age, float kcal)
    {
        Vector3 newPosition = new Vector3(pos.x, -0.11f, pos.z);
        GameObject newPillar = Instantiate(pillar, newPosition, Quaternion.identity, transform);
        Pillar newPillarScript = newPillar.GetComponent<Pillar>();


        newPillarScript.SetZip(zip);
        newPillarScript.SetWeight(weight);
        newPillarScript.SetHeight(height);
        newPillarScript.SetGender(ismale);
        newPillarScript.SetAge(age);
        newPillarScript.SetKcal(kcal);

        placedPillars.Add(newPillar);
    }
    public void CreatePilar(GameObject obj, int zip, float weight, float height, string gender, int age, float kcal)
    {
        Vector3 position = obj.transform.position;
        Vector3 newPosition = new Vector3(position.x, -0.11f, position.z);
        GameObject newPillar = Instantiate(pillar, newPosition, Quaternion.identity, transform);
        Pillar newPillarScript = newPillar.GetComponent<Pillar>();


        newPillarScript.SetZip(zip);
        newPillarScript.SetWeight(weight);
        newPillarScript.SetHeight(height - 130);
        if (gender == "MALE")
        {
            newPillarScript.SetGender(true);
        }
        else
        {
            newPillarScript.SetGender(false);
        }
        newPillarScript.SetAge(age / 12);
        newPillarScript.SetKcal(kcal);

        placedPillars.Add(newPillar);
        buttonScript.AddPillarScript(newPillarScript);
        obj.SetActive(false);
    }
}

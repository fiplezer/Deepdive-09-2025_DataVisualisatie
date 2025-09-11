using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{

    [SerializeField]
    private UnityEvent unityEvent;

    private List<Pillar> pillars = new();
    private Animator animator;
    private PillarGenerator pillarGenerator;
    private VariableManager variableManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pillarGenerator = GameObject.FindGameObjectWithTag("PillarManager").GetComponent<PillarGenerator>();
        variableManager = GameObject.FindGameObjectWithTag("VariableManager").GetComponent<VariableManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PressButton() //Plays the button click animation
    {
        animator.SetTrigger("ClickButton");
        unityEvent.Invoke();
    }

    public void AddPillarScript(Pillar pillar)
    {
        pillars.Add(pillar);
    }

    void OnTriggerEnter(Collider other)
    {
        PressButton();
    }


    public void ShowAvarageHeight()
{
    Vector3[] provPos = { new Vector3(-3, 0, 1), new Vector3(-3.5f, 0, -1.5f), new Vector3(-.5f, 0, -.5f) };
    List<Pillar> drenthe = new();
    List<Pillar> groningen = new();
    List<Pillar> friesland = new();
    List<Pillar>[] provinces = { drenthe, groningen, friesland };

    // Distribute pillars into provinces and deactivate them
    for (int i = 0; i < pillars.Count(); i++)
    {
        if (i < pillars.Count() / 3)
        {
            drenthe.Add(pillars[i]);
        }
        else if (i < pillars.Count() / 3 * 2)
        {
            groningen.Add(pillars[i]);
        }
        else
        {
            friesland.Add(pillars[i]);
        }
        pillars[i].gameObject.SetActive(false);
    }

    // For each province, process data and create average pillar once
    for (int i = 0; i < provinces.Length; i++)
    {
        List<float> weights = new();
        List<float> heights = new();
        List<int> genders = new();
        List<int> ages = new();
        List<float> kcals = new();

        // Collect data from each pillar
        foreach (Pillar pillar in provinces[i])
        {
            weights.Add(pillar.weight);
            heights.Add(pillar.height);
            genders.Add(pillar.isMale ? 1 : 0);
            ages.Add(pillar.age);
            kcals.Add(pillar.kcal);
        }

        // Compute averages
        float avgWeight = weights.Count > 0 ? weights.Average() : 0;
        float avgHeight = heights.Count > 0 ? heights.Average() : 0;
        float avgAge = (float)(ages.Count > 0 ? ages.Average() : 0);
        float avgKcal = kcals.Count > 0 ? kcals.Average() : 0;
        float avgGender = (float)(genders.Count > 0 ? genders.Average() : 0);

        // Determine gender boolean
        bool isMaleAvg = avgGender >= 0.5f; // or > 0.5 if you prefer

        // Create the average pillar once per province
        pillarGenerator.CreateAvaragePilar(
            provPos[i], 
            0, 
            avgWeight, 
            avgHeight, 
            isMaleAvg, 
            (int)avgAge, 
            avgKcal
        );
    }
}
}
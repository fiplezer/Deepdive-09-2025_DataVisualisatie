using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{

    [SerializeField]
    private UnityEvent unityEvent;

    private List<Pillar> pillars = new();
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PressButton() //Plays the button click animation
    {
        Debug.Log("Clicked");
        animator.SetTrigger("ClickButton");
        unityEvent.Invoke();
        // foreach (Pillar pillar in pillars) //If needed runs for every pillar
        // {

        // }
    }

    public void AddPillarScript(Pillar pillar)
    {
        pillars.Add(pillar);
    }

    void OnTriggerEnter(Collider other)
    {
        PressButton();
    }
}

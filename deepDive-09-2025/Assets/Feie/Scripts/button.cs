using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
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
        animator.SetTrigger("ClickButton");
        foreach (Pillar pillar in pillars) {
            pillar.SetHeight();
        }
        Debug.Log("ingedrukt");
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

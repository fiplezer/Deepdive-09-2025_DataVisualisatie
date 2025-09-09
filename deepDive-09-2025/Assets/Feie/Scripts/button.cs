using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent onPress;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        onPress.Invoke();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PressButton() //Plays the button click animation
    {
        animator.SetTrigger("ClickButton");
    }

    void OnTriggerEnter(Collider other)
    {
        PressButton();
    }
}

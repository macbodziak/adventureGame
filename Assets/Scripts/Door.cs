using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    // [SerializeField] float animationSpeedModifier;
    bool isOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
    }

    public void Open()
    {
        Debug.Log("Door opened " + gameObject.GetInstanceID());
        isOpen = true;
        animator.SetBool("Open", true);
    }

    public void Close()
    {
        Debug.Log("Door closed " + gameObject.GetInstanceID());
        isOpen = false;
        animator.SetBool("Open", false);
    }

    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
    }

}

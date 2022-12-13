using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    Animator animator;
    // [SerializeField] float animationSpeedModifier;
    bool isActivated;
    public UnityEvent activatedCallbacks;
    public UnityEvent deactivatedCallbacks;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        isActivated = false;
    }

    public void Activate()
    {
        Debug.Log("Switch Activated " + gameObject.GetInstanceID());
        animator.SetBool("Activated", true);
        activatedCallbacks.Invoke();
    }

    public void Deactivate()
    {
        Debug.Log("Switch Deactivated  " + gameObject.GetInstanceID());
         animator.SetBool("Activated", false);
         deactivatedCallbacks.Invoke();
    }

    public bool IsActivated
    {
        get
        {
            return isActivated;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            Activate();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            Deactivate();
        }
    }
}

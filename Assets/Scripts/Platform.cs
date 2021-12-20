using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] MovingPlatformController platformController;


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            platformController.PlayerMovement = other.gameObject.GetComponentInParent<CharacterMovement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            platformController.PlayerMovement = null;
        }
    }

}

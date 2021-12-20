using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Vector3 prevPosition;
    [SerializeField] MovingPlatform movingPlatform;

    private void Awake()
    {
        prevPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            movingPlatform.PlayerMovement = other.gameObject.GetComponentInParent<CharacterMovement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            movingPlatform.PlayerMovement = null;
        }
    }

}

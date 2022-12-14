using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] List<float> stopTimes;
    [SerializeField] GameObject platformObject;
    int index;
    bool isStoped;
    CharacterMovement playerMovement;
    Vector3 currentWaypoint;
    Rigidbody platformRigidBody;

    private void Start()
    {
        platformRigidBody = platformObject.GetComponent<Rigidbody>();
        currentWaypoint = waypoints[0].position;
        index = 0;
        isStoped = false;
    }

    private void FixedUpdate()
    {
        UpdatePlatformMovement();
    }

    private void UpdatePlatformMovement()
    {
        if (isStoped == false)
        {
            Vector3 oldPos = platformObject.transform.position;
            Vector3 newPos = Vector3.MoveTowards(platformObject.transform.position, currentWaypoint, Time.fixedDeltaTime * speed);
            platformRigidBody.MovePosition(newPos);

            //updateplayer
            HandlePlayerTranslation(newPos - oldPos);

            if (Vector3.Distance(platformObject.transform.position, currentWaypoint) < 0.01f)
            {
                if (stopTimes[index] > 0)
                {
                    isStoped = true;
                    StartCoroutine("Stop", stopTimes[index]);
                }

                if (index == waypoints.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }

                currentWaypoint = waypoints[index].position;
            }
        }
        else
        {
            HandlePlayerTranslation(Vector3.zero);
        }

        
    }

    IEnumerator Stop(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);
        isStoped = false;
    }

    private void HandlePlayerTranslation(Vector3 newPos)
    {
        if (playerMovement != null)
        {
            //a hack to remove stutter when pushing player upwards, otherwise the isGrounded flag does not always work, the collider will push the character anyway
            if (newPos.y > 0.0f)
            {
                newPos.y = 0.0f;
            }
            else if (newPos.y < 0.0f)
            {
                //characterController skin offset - to avoid a one frame false when stops
                newPos.y -= 0.01f;
            }
            playerMovement.PlatformVector = newPos;
        }
    }

    public CharacterMovement PlayerMovement
    {
        set
        {
            if (value == null && playerMovement != null)
            {
                playerMovement.PlatformVector = Vector3.zero;
            }
            playerMovement = value;
        }

        get
        {
            return playerMovement;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform trackedObject;
    [SerializeField] Vector3 offset;
    [SerializeField] float rotationSpeed;
    [SerializeField] float zoomSpeed;
    [SerializeField] CharacterMovement playerController;
    float currentAngle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 inputVector = GameManager.Instance.PlayerInput.Player.Look.ReadValue<Vector2>();
        offset += new Vector3(0f, inputVector.y * zoomSpeed, -inputVector.y * zoomSpeed);
        offset.y = Mathf.Clamp(offset.y, -10f, -5f);
        offset.z = Mathf.Clamp(offset.z, 5f, 10f);
        transform.position = trackedObject.transform.position - offset;
        currentAngle += inputVector.x * Time.deltaTime * rotationSpeed;
        transform.RotateAround(trackedObject.transform.position, Vector3.up, currentAngle);
        transform.LookAt(trackedObject);
        playerController.CameraAngle = Quaternion.Euler(0f, currentAngle, 0f);
    }
}

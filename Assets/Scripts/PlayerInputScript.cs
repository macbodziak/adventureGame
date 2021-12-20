using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    //
    //
    //this script is just a leftover from a tutorial
    //
    //

    Rigidbody rb;
    PlayerInputActions inputMaster;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        PlayerInputActions pia = new PlayerInputActions();
        pia.Player.Enable();
        pia.Player.Jump.performed += Jump;
        // inputMaster.Player.Movement.performed += MovementPerformed;
    }

    private void Jump(InputAction.CallbackContext context) {
        if(context.performed)
        {
        Debug.Log("jump " + context.phase);
            
        rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
        }
    }

    private void FixedUpdate() 
    {
        Vector2 inputVector = inputMaster.Player.Move.ReadValue<Vector2>();
        float speed = 5f;
        rb.AddForce(new Vector3(inputVector.x, 0f, inputVector.y) * speed,ForceMode.Force);
        // Debug.Log("movement  " + inputVector.x + "  .  " + inputVector.y);
    }
    private void MovementPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("movement performed " + context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        float speed = 5f;
        rb.AddForce(new Vector3(inputVector.x, 0f, inputVector.y) * speed,ForceMode.Force);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    CharacterController charCon;
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float gravity;
    [SerializeField] float fallToDamage;
    Player player;
    private Vector3 movementVelocity;
    private Animator animator;
    private float currentRotation;
    bool isGroundedPrevFrame;
    float fallingMaxHeight;
    Vector3 platformVector;
    Quaternion cameraAngle;

    private void Awake()
    {
        charCon = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Debug.Assert(charCon != null);
        Debug.Assert(animator != null);
        fallingMaxHeight = transform.position.y;
    }

    void Start()
    {
        GameManager.Instance.PlayerInput.Player.Jump.performed += Jump;
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (charCon.isGrounded && movementVelocity.y < 0.0f)
        {
            movementVelocity.y = 0.0f;
        }

        if (charCon.isGrounded)
        {
            //apply movement input only when touching ground
            Vector2 inputVector = GameManager.Instance.PlayerInput.Player.Move.ReadValue<Vector2>();
            movementVelocity.x = inputVector.x;
            movementVelocity.z = inputVector.y;
            movementVelocity = cameraAngle * movementVelocity;
        }
        //allways apply some gravity, also so it detects isGrounded collision every frame when touching ground
        movementVelocity.y += gravity * Time.fixedDeltaTime;

        charCon.Move(movementVelocity * Time.fixedDeltaTime * movementSpeed + platformVector);
        float animSpeed = new Vector3(movementVelocity.x, 0f, movementVelocity.z).magnitude;
        animator.SetFloat("Speed", animSpeed * movementSpeed);
        animator.SetBool("IsGrounded", charCon.isGrounded);

        RotateCharacter();
        HandleGroundBehaviour();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (charCon.isGrounded)
        {
            Vector2 inputVector = GameManager.Instance.PlayerInput.Player.Move.ReadValue<Vector2>();
            movementVelocity.x = inputVector.x;
            movementVelocity.z = inputVector.y;
            movementVelocity.y = jumpSpeed;
            animator.SetTrigger("Jump");
        }
    }

    private void RotateCharacter()
    {
        Vector2 inputVector = GameManager.Instance.PlayerInput.Player.Move.ReadValue<Vector2>();
        if (IsMovingHorizontally())
        {
            Vector3 positionToLookAt;

            positionToLookAt.x = movementVelocity.x;
            positionToLookAt.y = 0f;
            positionToLookAt.z = movementVelocity.z;

            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);

            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 12 * Time.fixedDeltaTime);
        }
    }

    private bool IsMovingHorizontally()
    {
        if (movementVelocity.x == 0.0f && movementVelocity.z == 0.0f)
        {
            return false;
        }
        return true;
    }

    private void HandleGroundBehaviour()
    {
        //on grounded touched
        if (charCon.isGrounded && !isGroundedPrevFrame)
        {
            float fallenHeight = transform.position.y - fallingMaxHeight;
            if (fallenHeight < -fallToDamage)
            {
                player.TakeDamage(25f);
                Debug.Log("you took damage from falling");
            }
            //reset the value to zero after landing
            animator.SetFloat("FallenHeight", 0.0f);
        }

        //on take off
        if (!charCon.isGrounded && isGroundedPrevFrame)
        {
            // fallingMaxHeight = transform.position.y;
            Debug.Log("taking off " + fallingMaxHeight);
        }

        if (!charCon.isGrounded)
        {
            animator.SetFloat("FallenHeight", transform.position.y - fallingMaxHeight);

            if(transform.position.y > fallingMaxHeight)
            {
                fallingMaxHeight = transform.position.y;
                Debug.Log("fallingMaxHeight " + fallingMaxHeight);
            }
        }

        isGroundedPrevFrame = charCon.isGrounded;
    }

    public Vector3 PlatformVector
    {
        get
        {
            return platformVector;
        }
        set
        {
            platformVector = value;
        }
    }

    public Quaternion CameraAngle
    {
        get
        {
            return cameraAngle;
        }
        set
        {
            cameraAngle = value;
        }
    }
}

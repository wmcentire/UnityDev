using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float hitForce = 2;
    [SerializeField] private float gravity;
    [SerializeField] private float turnRate = 10;
    [SerializeField] private float jumpHeight = 2;
    [SerializeField] private Animator animator;
    
    CharacterController controller;
    PlayerInputActions playerInput;
    Camera mainCamera;
    Vector3 velocity = Vector3.zero;
    float InAirtime = 0;

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;
        Vector2 axis = playerInput.Player.Move.ReadValue<Vector2>();
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        
        direction = mainCamera.transform.TransformDirection(direction);

        if (controller.isGrounded)
        {
            InAirtime = 0; 
            velocity.x = direction.x * speed;
            velocity.z = direction.z * speed;
            if (playerInput.Player.Jump.triggered)
            {
                animator.SetTrigger("Jump");
                velocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
            }
        }
        else
        {
            InAirtime += Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            velocity.x = direction.x * speed / 2;
            velocity.z = direction.z * speed / 2;
        }
        controller.Move(velocity * Time.deltaTime);
        Vector3 look = direction;
        look.y = 0;
        if(look.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), turnRate * Time.deltaTime);
        }

        //set animator parameters
        animator.SetFloat("Speed", controller.velocity.magnitude);
        animator.SetFloat("VelocityY", controller.velocity.y);
        animator.SetFloat("InAirTime", InAirtime);
        animator.SetBool("isGrounded", controller.isGrounded);

    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * hitForce;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump");
        }
    }
}

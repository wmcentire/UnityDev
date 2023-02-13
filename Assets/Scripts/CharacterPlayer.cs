using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterPlayer : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Animator animator;
    [SerializeField] private InputRouter router;
    
    CharacterController controller;
    Camera mainCamera;
    Vector2 inputAxis;

    Vector3 velocity = Vector3.zero;
    float InAirtime = 0;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        router.jumpEvent += OnJump;
        router.moveEvent += OnMove;
        router.fireEvent += OnFire;
        router.fireStopEvent += OnFireStop;
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = inputAxis.x;
        direction.z = inputAxis.y;
        
        direction = mainCamera.transform.TransformDirection(direction);

        if (controller.isGrounded)
        {
            InAirtime = 0; 
            velocity.x = direction.x * playerData.speed;
            velocity.z = direction.z * playerData.speed;
            
        }
        else
        {
            InAirtime += Time.deltaTime;
            velocity.y += playerData.gravity * Time.deltaTime;
            velocity.x = direction.x * playerData.speed / 2;
            velocity.z = direction.z * playerData.speed / 2;
        }
        controller.Move(velocity * Time.deltaTime);
        Vector3 look = direction;
        look.y = 0;
        if(look.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), playerData.turnRate * Time.deltaTime);
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
        body.velocity = pushDir * playerData.hitForce;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump");
        }
    }

    public void OnLeftFootSpawn(GameObject go)
    {
        Transform bone = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        Instantiate(go, bone.position, bone.rotation);
    }
    public void OnRightFootSpawn(GameObject go)
    {
        Transform bone = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        Instantiate(go, bone.position, bone.rotation);
    }

    public void OnJump()
    {
        if (controller.isGrounded)
        {
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(playerData.jumpHeight * -3 * playerData.gravity);
        }
    }

    public void OnFire()
    {

    }

    public void OnFireStop()
    {

    }


    public void OnMove(Vector2 axis)
    {
        inputAxis = axis;
    }
}

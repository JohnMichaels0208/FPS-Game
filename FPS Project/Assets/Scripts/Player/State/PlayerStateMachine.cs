using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [HideInInspector] public Rigidbody rigidbodyComponent;
    [HideInInspector] public Animator animatorComponent;
    private PlayerInputActions playerInputActions;
    [SerializeField] private Camera cam;
    [HideInInspector] public Vector2 moveVector = new Vector2(0, 0);
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public float jumpForce = 1500;
    [HideInInspector] public float walkSpeed = 1000;
    [HideInInspector] public float runSpeed = 1500;
    private float sensX = 70f;
    private float sensY = 70f;
    public readonly float animationDampSpeed = 0.1f;
    private float cameraZRot = 0;

    [HideInInspector] public bool moveKeysPressed = false;
    [HideInInspector] public bool jumpKeyPressed = false;
    [HideInInspector] public bool runKeyPressed = false;

    public PlayerBaseState playerIdleState = new PlayerIdleState();
    public PlayerBaseState playerWalkState = new PlayerWalkState();
    public PlayerBaseState playerJumpState = new PlayerJumpState();
    public PlayerBaseState playerRunState = new PlayerRunState();

    private PlayerBaseState startingState;
    public PlayerBaseState currentState;

    private bool cameraRaycast;
    private RaycastHit cameraRaycastHit;
    [SerializeField] private GameObject pickUpUI;
    [SerializeField] private LayerMask cameraRaycastLayerMask;

    private void Awake()
    {
        startingState = playerIdleState;
        isGrounded = false;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += JumpKeyPressed;
        playerInputActions.Player.Jump.canceled += JumpKeyCanceled;
        playerInputActions.Player.Move.performed += MoveKeysPressed;
        playerInputActions.Player.Move.canceled += MoveKeysCanceled;
        playerInputActions.Player.Run.performed += RunKeyPressed;
        playerInputActions.Player.Run.canceled += RunKeyCanceled;
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
        currentState = startingState;
    }
    void Update()
    {
        if (!currentState.executeInFixedUpdate)
        {
            currentState.Execute(this);
        }
        else if (currentState == playerWalkState)
        {
            animatorComponent.SetFloat("Input X", moveVector.x * 0.5f, animationDampSpeed, Time.deltaTime);
            animatorComponent.SetFloat("Input Y", moveVector.y * 0.5f, animationDampSpeed, Time.deltaTime);
        }
        else if (currentState == playerRunState)
        {
            animatorComponent.SetFloat("Input X", moveVector.x, animationDampSpeed, Time.deltaTime);
            animatorComponent.SetFloat("Input Y", moveVector.y, animationDampSpeed, Time.deltaTime);
        }

        transform.Rotate(new Vector3(0, playerInputActions.Player.RotateX.ReadValue<float>() * sensX * Time.deltaTime, 0));
        cam.transform.Rotate(new Vector3(-playerInputActions.Player.RotateY.ReadValue<float>() * sensY * Time.deltaTime, 0, 0));
        cam.transform.localEulerAngles = new Vector3(ClampAngle(cam.transform.localEulerAngles.x, -60, 60), 0, cameraZRot);
        cameraRaycast = Physics.Raycast(cam.transform.position, cam.transform.forward, out cameraRaycastHit, Mathf.Infinity, cameraRaycastLayerMask);
        if (cameraRaycast)
        {
            if (cameraRaycastHit.transform.tag == "Weapon")
            {
                pickUpUI.SetActive(true);
                pickUpUI.transform.position = cam.WorldToScreenPoint(cameraRaycastHit.transform.position);
            }
            else
            {
                pickUpUI.SetActive(false);
            }
        }

    }

    private void FixedUpdate()
    {
        if (currentState.executeInFixedUpdate)
        {
            currentState.Execute(this);
        }
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState=state;
    }

    public void JumpKeyPressed(InputAction.CallbackContext context)
    {
        jumpKeyPressed = true;
    }

    public void JumpKeyCanceled(InputAction.CallbackContext context)
    {
        jumpKeyPressed = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            isGrounded = CheckGrounded(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private bool CheckGrounded(Collision collision)
    {
        for (int i = 0; i < collision.GetContacts(collision.contacts); i++)
        {
            ContactPoint contactPoint = collision.GetContact(i);
            if (contactPoint.normal.y > 0)
            {
                return true;
            }
        }
        return false;
    }

    private void MoveKeysPressed(InputAction.CallbackContext context)
    {
        moveVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        moveKeysPressed = true;
    }

    private void MoveKeysCanceled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
        moveKeysPressed=false;
    }

    private void RunKeyPressed(InputAction.CallbackContext context)
    {
        runKeyPressed = true;
    }

    private void RunKeyCanceled(InputAction.CallbackContext context)
    {
        runKeyPressed = false;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        float angleInInpsectorEuler;
        angleInInpsectorEuler = (angle > 180) ? angle - 360 : angle;
        if (angleInInpsectorEuler > max)
        {
            return max;
        }
        if (angleInInpsectorEuler < min)
        {
            return min;
        }
        return angle;
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController controller;
    [SerializeField] private GameObject[] maleSkins;
    [SerializeField] private GameObject[] femaleSkins;
    [SerializeField] private Joystick joystick;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

    [HideInInspector] public enum PlayerStates { idle, isRunning, isJumping };
    [HideInInspector] public PlayerStates playerState = PlayerStates.idle;

    private Animator animator;
    private Vector3 velocity;
    [HideInInspector] public bool canMove;
    private bool isGrounded;
    private float speed = 12f;
    private float gravity = -19.62f;
    private float jumpHeight = 3f;
    private float groundDistance = 0.4f;

    private bool isMale;
    private int lastSkinUsed;

    void Start()
    {

        FindObjectOfType<PlayerManager>().LoadPlayer();

        isMale = FindObjectOfType<PlayerManager>().isMale;
        lastSkinUsed = FindObjectOfType<PlayerManager>().lastSkinUsed;

        animator = isMale 
            ? maleSkins[lastSkinUsed].GetComponent<Animator>() 
            : femaleSkins[lastSkinUsed].GetComponent<Animator>();

        if (isMale)
        {

            maleSkins[lastSkinUsed].SetActive(true);

        }
        else
        {

            femaleSkins[lastSkinUsed].SetActive(true);

        }

        canMove = false;

    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {

            velocity.y = -2f;

        }

        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;
        
        if (canMove && x != 0f && z != 0f)
        {

            playerState = PlayerStates.isRunning;
            controller.Move(move * speed * Time.deltaTime);

        }
        else
        {
            playerState = PlayerStates.idle;
        }

        if (SimpleInput.GetButtonDown("OnJump") && isGrounded && canMove)
        {

            playerState = PlayerStates.isJumping;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        }

        if (SimpleInput.GetButtonDown("OnCrouch") && isGrounded && canMove)
        {

            

        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        animator.SetInteger("playerState", (int) playerState);

    }

}

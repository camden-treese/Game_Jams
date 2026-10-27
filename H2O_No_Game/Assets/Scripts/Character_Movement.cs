using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Character_Movement : MonoBehaviour
{
    // Speeds for left/right movement as well as jumping
    [Header("MovementSpeeds")]
    [SerializeField] float moveSpeed = 12.0f;
    [SerializeField] float jumpPower = 8.0f;


    [Header("GravityMultipliers")]
    [Tooltip("How fast the player falls after hitting the apex")]
    [SerializeField] float fallMultiplier = 2.5f;

    [Tooltip("How fast the player falls if they let go of jump early (short jumps)")]
    [SerializeField] float shortJumpMultiplier = 2.0f;


    // Things needed to do a ground check (making sure the object is on the ground)
    [Header("GroundCheck")]
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float checkRadius = 0.02f;

    // Variables
    private Rigidbody2D rb;
    private float horizontalInput;
    private bool onGround;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // gets the RigidBody component from the object
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // set the horizontal input to zero so it doesn't infinitely move
        horizontalInput = 0f;

        // call to the horizontal input function to get horizontal direction
        getHorizontalInput();

        // checks if the player is on ground by checking the position of bottom, the radius for how close it is, and if it is the ground layer it is touching
        onGround = Physics2D.OverlapCircle(groundChecker.position, checkRadius, groundLayer);

        if (rb.linearVelocityY < 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.linearVelocityY > 0 && !(Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)) {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (shortJumpMultiplier - 1) * Time.deltaTime;
        }


        // checks for w or up and if so jump
        if ((Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) && onGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);
        }




    }

    private void FixedUpdate()
    {
        // fiuxed horizontal movement
        rb.linearVelocity = new Vector2(moveSpeed * horizontalInput, rb.linearVelocityY);
    }


    private void getHorizontalInput ()
    {
        // if a or left set movement to negative, if d or right set to positive
        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalInput = 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalInput = -1f;
        }
    }


}

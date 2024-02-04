using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public Rigidbody2D playerRigidbody;
    public KeyCode jumpKey = KeyCode.Space;

    public AudioSource jumpAudioSource;

    private bool isGrounded;
    private bool canJump = true;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        jumpAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        HandleMovementInput();

        if (Input.GetKeyDown(jumpKey) && isGrounded && canJump)
        {
            Jump();
        }
    }

    void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 moveDirection = new Vector2(horizontalInput, 0f);
        playerRigidbody.velocity = new Vector2(moveDirection.x * moveSpeed, playerRigidbody.velocity.y);

        // Check if the "S" key is pressed to make the player move down rapidly
        if (Input.GetKey(KeyCode.S))
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, -moveSpeed * 2f);
        }
    }

    void Jump()
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);

        if (jumpAudioSource != null)
        {
            jumpAudioSource.Play();
        }

        canJump = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}

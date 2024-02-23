using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement settings
    public float speed = 5f;
    public float runMultiplier = 1.5f;
    public float jumpForce = 5f;

    // Player state
    private bool isJumping = false;

    // Components
    private Rigidbody rb;
    public Transform cam;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get movement input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        Vector3 relativeDirection = cam.TransformDirection(direction);
        relativeDirection.y = 0;

        // Move player
        if (Input.GetKey(KeyCode.W) || relativeDirection.magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Run
                rb.linearVelocity = relativeDirection * speed * runMultiplier;
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsWalking", false);
            }
            else
            {
                // Walk
                rb.linearVelocity = relativeDirection * speed;
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsWalking", true);
            }
        }
        else
        {
            // Stop
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsWalking", false);
        }
        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

        // Jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("IsJumpPreparation", true);
            isJumping = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("IsJumpPreparation", false);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsLanding", true); // Reset jump state on landing
        }
    }
}

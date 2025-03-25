using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    public Transform groundCheck; // zemin kontrol noktasý
    public LayerMask groundLayer; // zemin layer'ý
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        // Hareket girdisi
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Run animasyonu kontrolü
        bool isRunning = Mathf.Abs(moveInput.x) > 0.1f;
        animator.SetBool("Run", isRunning);

        // Yerde mi kontrolü
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Zýplama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }


    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }
}

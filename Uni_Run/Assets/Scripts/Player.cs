using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Animation Settings\n")]
    public float jumpForce;
    public int jumpCountMax;

    public AudioClip dieAudioClip;

    private int jumpCount;
    private Animator animator;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private bool isGrounded = true;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isDead) return;

        if (Input.GetMouseButtonDown(0) && jumpCount < jumpCountMax)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            ++jumpCount;

            audioSource.Play();
        }

        if (Input.GetMouseButtonUp(0) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity *= 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.CompareTag("DeadZone"))
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform") && collision.contacts[0].normal.y > 0.7f) 
        {
            jumpCount = 0;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;

        GameManager.Instance.OnPlayerDead(); //made this to load the instance of the game manager when the player calls out the Die method.

        audioSource.PlayOneShot(dieAudioClip);
    }
}

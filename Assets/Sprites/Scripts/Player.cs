using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidbody;
    public Animator PlayerAnimator;
    public BoxCollider2D PlayerCollider;

    private bool isGrounded = true;
    public bool isInvincible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Jump when space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayerRigidbody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }
    }

    public void KillPlayer()
    {
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerRigidbody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }


    void Hit()
    {
        GameManager.instance.Lives -= 1;
    }

    void Heal()
    {
        GameManager.instance.Lives = Mathf.Min(3, GameManager.instance.Lives + 1);
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f); // 5 seconds of invincibility
    }

    void StopInvincible()
    {
        isInvincible = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded
        if (collision.gameObject.name == "Platform")
        {
            if (!isGrounded)
            {
                PlayerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemy")
        {
            if (!isInvincible)
            {
                Destroy(collider.gameObject);
                Hit();
            }
        }
        else if (collider.gameObject.tag == "food")
        {
            Destroy(collider.gameObject);
            Heal();
        }
        else if (collider.gameObject.tag == "golden") //cabbage
        {
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Editor References")]

    public Rigidbody playerRb;
    public AudioSource playerAudio;


    [Header("Sound Configuration")]

    public AudioClip[] soundCollection;


    [Header("Movement Parameters")]

    public float speed = 10;
    public Vector2 movementInput; 


    [Header("Jump Parameters")]

    public float jumpForce = 6;
    public bool isGrounded = true; 


    [Header("Respawn System")]
    public float fallLimit = -5f; 
    public Transform respawnPoint;


    void Start()
    {
        
    }

   
    void Update()
    {
        if (transform.position.y <= fallLimit)
        {
            Respawn();
        }
    }

    void FixedUpdate()
    {
        PhysicalMovement();
    }


    void PhysicalMovement()
    {
        playerRb.AddForce(Vector3.right * speed * movementInput.x);
        playerRb.AddForce(Vector3.forward * speed * movementInput.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Respawn();
        }
    }

    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        PlaySFX(0);
    }

    void Respawn()
    {
        transform.position = respawnPoint.position;
        playerRb.linearVelocity = Vector3.zero;

        PlaySFX(1);
    }

    public void PlaySFX(int soundToPlay)
    {
        playerAudio.PlayOneShot(soundCollection[soundToPlay]);
    }

    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump (InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded == true)
        {
            isGrounded = false;
            Jump();
        }
    }

    #endregion
}

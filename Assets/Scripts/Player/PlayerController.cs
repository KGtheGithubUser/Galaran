using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public GameObject torso;
    private int maxHealthPoint = 100;
    private int _CurrentHealth;
    public Slider healthSlider;
    private int staminaPoint = 100;
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float LaunchForce = 5f;
    

    private Rigidbody rb;
    private Camera playerCamera;
    private float rotationX = 0f;
    private bool isGrounded;

    void Start()
    {
        healthSlider.maxValue = maxHealthPoint;
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        _CurrentHealth = 100;
    }

    void Update()
    {
        Controlls();
        healthController();
    }

    private void Controlls()
    {
        // Looking
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Rotation of the torso of the player
        float playerRotationX = playerCamera.transform.eulerAngles.x * -1;
        torso.transform.localRotation = Quaternion.Euler(playerRotationX, 0, 0);

        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        move = move.normalized * speed;
        // Apply movement it's
        Vector3 velocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        rb.linearVelocity = velocity;
        if(horizontal == 0 && vertical == 0)
        {
            anim.SetFloat("Moving", 0);
        }
        else
        {
            anim.SetFloat("Moving", 1);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("Jumping", true);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    private void healthController()
    {
    }

    public void TakeDamage(int damage)
    {
        _CurrentHealth -= damage;
        _CurrentHealth = Mathf.Clamp(_CurrentHealth, 0, maxHealthPoint);
        if (healthSlider != null)
        {
            healthSlider.value = _CurrentHealth;
        }
        LaunchBack();
    }

    void LaunchBack()
    {
    // Apply force to launch the object backward and upward
    Vector3 launchDirection = -transform.forward * 10 + Vector3.up; // Negative forward and upward direction
    rb.AddForce(launchDirection.normalized * LaunchForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hurt")
        {
            TakeDamage(5);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}


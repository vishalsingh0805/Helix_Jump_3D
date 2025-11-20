


// power up is added and debug logs are included for better tracing as it is not working properly

// using UnityEngine;

// public class Player : MonoBehaviour
// {
//     public float speed = 5f;
//     public float bounceHeight = 2f;
//     private Rigidbody rb;
//     public Vector3 initialPosition;
//     public bool isGameOver = false;
//     private bool isMovingDown = true;
    
//     // Power-up variables
//     private int consecutiveEmptyPasses = 0;
//     private bool isPowerUpActive = false;
//     public Material powerUpMaterial;
//     private Material normalMaterial;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         initialPosition = transform.position;
//         rb.useGravity = false;
//         normalMaterial = GetComponent<Renderer>().material;
        
//         Debug.Log("Player initialized - Power-up system ready");
//     }
    
//     void Update()
//     {
//         if (isGameOver) return;

//         if (isMovingDown)
//         {
//             transform.Translate(Vector3.down * speed * Time.deltaTime);
//         }
//         else if (transform.position.y < initialPosition.y + bounceHeight)
//         {
//             transform.Translate(Vector3.up * speed * Time.deltaTime);
//         }
//         else
//         {
//             isMovingDown = true;
//             initialPosition = transform.position;
//         }
//     }
    
//     void OnTriggerEnter(Collider other)
//     {
//         if (isGameOver) return;
        
//         Debug.Log($"Trigger entered: {other.gameObject.name} with tag: {other.tag}");
        
//         if (other.CompareTag("Empty"))
//         {
//             Debug.Log("✅ EMPTY TRIGGER DETECTED!");
//             PassedThroughEmpty();
//         }
//     }
    
//     void OnCollisionEnter(Collision other)
//     {
//         if (isGameOver) return;

//         Debug.Log($"Collision with: {other.gameObject.name} with tag: {other.gameObject.tag}");

//         if (other.gameObject.CompareTag("Safe"))
//         {
//             Debug.Log("🛡️ Hit SAFE zone");
//             isMovingDown = false;
//             initialPosition = transform.position;
            
//             // Reset consecutive empty passes when hitting safe zone
//             consecutiveEmptyPasses = 0;
//             Debug.Log($"Reset consecutive empty passes to: {consecutiveEmptyPasses}");
            
//             // Deactivate power-up if active
//             if (isPowerUpActive)
//             {
//                 Debug.Log("🚫 Power-up DEACTIVATED due to safe zone hit");
//                 DeactivatePowerUp();
//             }
//         }
//         else if (other.gameObject.CompareTag("Danger"))
//         {
//             Debug.Log($"💀 Hit DANGER zone - Power-up active: {isPowerUpActive}");
            
//             if (isPowerUpActive)
//             {
//                 Debug.Log("💥 BREAKING danger zone with power-up!");
//                 BreakDangerZone(other.gameObject);
//                 // Continue moving down without game over
//             }
//             else
//             {
//                 Debug.Log("🎮 GAME OVER - No power-up active");
//                 isGameOver = true;
//                 GameManager.Instance?.OnGameOver();
//                 rb.linearVelocity = Vector3.zero;
//             }
            
//             // Reset consecutive empty passes when hitting danger zone
//             consecutiveEmptyPasses = 0;
//             Debug.Log($"Reset consecutive empty passes to: {consecutiveEmptyPasses}");
//         }
//     }
    
//     public void PassedThroughEmpty()
//     {
//         if (isGameOver) return;
        
//         consecutiveEmptyPasses++;
//         Debug.Log($"🎯 Empty pass #{consecutiveEmptyPasses}");
        
//         // Activate power-up after 2 consecutive empty passes
//         if (consecutiveEmptyPasses >= 2 && !isPowerUpActive)
//         {
//             Debug.Log("✨ ACTIVATING POWER-UP! (2 consecutive empty passes)");
//             ActivatePowerUp();
//         }
//     }
    
//     void ActivatePowerUp()
//     {
//         isPowerUpActive = true;
        
//         // Visual feedback
//         if (powerUpMaterial != null)
//         {
//             GetComponent<Renderer>().material = powerUpMaterial;
//             Debug.Log("🎨 Power-up visual activated");
//         }
//         else
//         {
//             Debug.LogWarning("❌ Power-up material not assigned!");
//         }
        
//         Debug.Log("⚡ POWER-UP ACTIVE - Can break danger zones!");
//     }
    
//     void DeactivatePowerUp()
//     {
//         isPowerUpActive = false;
//         consecutiveEmptyPasses = 0;
        
//         // Reset visual
//         if (normalMaterial != null)
//         {
//             GetComponent<Renderer>().material = normalMaterial;
//             Debug.Log("🎨 Power-up visual deactivated");
//         }
        
//         Debug.Log("🚫 POWER-UP DEACTIVATED");
//     }
    
//     void BreakDangerZone(GameObject dangerZone)
//     {
//         // Disable the danger zone
//         dangerZone.SetActive(false);
//         Debug.Log("💥 Danger zone broken and disabled!");
        
//         // Add bonus points for breaking danger zone
//         GameManager.Instance?.AddAndUpdateScore(5);
        
//         // Deactivate power-up after breaking one danger zone
//         Debug.Log("🔄 Deactivating power-up after breaking danger zone");
//         DeactivatePowerUp();
//     }
// }






using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Physics Settings")]
    public float moveForce = 2f;
    public float bounceForce = 6f;
    public float maxDownwardSpeed = 3f;
    
    private Rigidbody rb;
    public Vector3 initialPosition;
    public bool isGameOver = false;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Starting position
        transform.position = new Vector3(0f, 0.3f, -0.86f);
        initialPosition = transform.position;
        
        // NEW PHYSICS SYSTEM setup
        rb.useGravity = true;
        // These are automatically set in inspector, but we can set them in code too
        // rb.linearDamping = 1.0f;  // This is "Linear Damping" in inspector
        // rb.angularDamping = 0.8f; // This is "Angular Damping" in inspector
        
        // Constraints are already set in inspector, but let's enforce them in code too
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        
        Debug.Log("Player initialized with new physics system");
    }
    
    void Update()
    {
        if (isGameOver) 
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        // Limit downward speed
        if (rb.linearVelocity.y < -maxDownwardSpeed)
        {
            Vector3 currentVelocity = rb.linearVelocity;
            currentVelocity.y = -maxDownwardSpeed;
            rb.linearVelocity = currentVelocity;
        }
        
        // Forcefully maintain X and Z position (safety measure)
        Vector3 currentPos = transform.position;
        if (Mathf.Abs(currentPos.x) > 0.01f || Mathf.Abs(currentPos.z + 0.86f) > 0.01f)
        {
            transform.position = new Vector3(0f, currentPos.y, -0.86f);
        }
        
        // Also ensure velocity doesn't accumulate in X/Z
        if (Mathf.Abs(rb.linearVelocity.x) > 0.01f || Mathf.Abs(rb.linearVelocity.z) > 0.01f)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }
    
    void FixedUpdate()
    {
        if (isGameOver) return;

        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * moveForce, ForceMode.Acceleration);
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (isGameOver) return;

        if (other.gameObject.CompareTag("Safe"))
        {
            isGrounded = true;
            
            // Reset all velocity to prevent drift
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            
            Debug.Log("Bounced on safe platform");
        }
        else if (other.gameObject.CompareTag("Danger"))
        {
            isGameOver = true;
            GameManager.Instance?.OnGameOver();
            rb.linearVelocity = Vector3.zero;
        }
    }
    
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Safe"))
        {
            isGrounded = false;
        }
    }
}
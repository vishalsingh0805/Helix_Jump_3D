// first version where only simple game is working

// using UnityEngine;

// public class Player : MonoBehaviour
// {
//     public float speed = 5f;
//     public float bounceHeight = 2f;
//     private Rigidbody rb;
//     public Vector3 initialPosition;
//     public bool isGameOver = false;
//     private bool isMovingDown = true;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         initialPosition = transform.position;
//         rb.useGravity = false;
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
//     void OnCollisionEnter(Collision other)
//     {
//         if (isGameOver) return;

//         if (other.gameObject.CompareTag("Safe"))
//         {
//             isMovingDown = false;
//             initialPosition = transform.position;
//         }
//         else if (other.gameObject.CompareTag("Danger"))
//         {
//             isGameOver = true;
//             GameManager.Instance?.OnGameOver();
//             rb.linearVelocity = Vector3.zero;
//         }
//     }
// }






// power up feature added but there are issues
// using UnityEngine;

// public class Player : MonoBehaviour
// {
//     public float speed = 5f;
//     public float bounceHeight = 2f;
//     private Rigidbody rb;
//     public Vector3 initialPosition;
//     public bool isGameOver = false;
//     private bool isMovingDown = true;
    
//     // POWER-UP VARIABLES - ADD THESE
//     private int consecutiveEmptyPasses = 0;
//     private bool isPowerUpActive = false;
//     public Material powerUpMaterial;
//     private Material normalMaterial;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         initialPosition = transform.position;
//         rb.useGravity = false;
//         normalMaterial = GetComponent<Renderer>().material; // Store original material
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
    
//     // ADD THIS METHOD: Detect when ball passes through empty sections
//     void OnTriggerEnter(Collider other)
//     {
//         if (isGameOver) return;
        
//         if (other.CompareTag("Empty"))
//         {
//             PassedThroughEmpty();
//         }
//     }
    
//     void OnCollisionEnter(Collision other)
//     {
//         if (isGameOver) return;

//         if (other.gameObject.CompareTag("Safe"))
//         {
//             isMovingDown = false;
//             initialPosition = transform.position;
            
//             // POWER-UP: Reset consecutive empty passes when hitting safe zone
//             consecutiveEmptyPasses = 0;
            
//             // POWER-UP: Deactivate power-up if active
//             if (isPowerUpActive)
//             {
//                 DeactivatePowerUp();
//             }
//         }
//         else if (other.gameObject.CompareTag("Danger"))
//         {
//             // POWER-UP: Check if power-up is active
//             if (isPowerUpActive)
//             {
//                 // Break through the danger zone
//                 BreakDangerZone(other.gameObject);
//                 // Continue moving down (don't set isMovingDown = false)
//                 // Don't set game over
//             }
//             else
//             {
//                 // Normal game over behavior
//                 isGameOver = true;
//                 GameManager.Instance?.OnGameOver();
//                 rb.linearVelocity = Vector3.zero;
//             }
            
//             // POWER-UP: Reset consecutive empty passes
//             consecutiveEmptyPasses = 0;
//         }
//     }
    
//     // POWER-UP METHODS - ADD THESE
    
//     public void PassedThroughEmpty()
//     {
//         if (isGameOver) return;
        
//         consecutiveEmptyPasses++;
        
//         // Activate power-up after 2 consecutive empty passes
//         if (consecutiveEmptyPasses >= 2 && !isPowerUpActive)
//         {
//             ActivatePowerUp();
//         }
//     }
    
//     void ActivatePowerUp()
//     {
//         isPowerUpActive = true;
        
//         // Visual feedback - change ball color
//         if (powerUpMaterial != null)
//         {
//             GetComponent<Renderer>().material = powerUpMaterial;
//         }
//     }
    
//     void DeactivatePowerUp()
//     {
//         isPowerUpActive = false;
//         consecutiveEmptyPasses = 0;
        
//         // Reset visual - back to normal color
//         if (normalMaterial != null)
//         {
//             GetComponent<Renderer>().material = normalMaterial;
//         }
//     }
    
//     void BreakDangerZone(GameObject dangerZone)
//     {
//         // Disable the danger zone
//         dangerZone.SetActive(false);
        
//         // Add bonus points for breaking danger zone
//         GameManager.Instance?.AddAndUpdateScore(5);
        
//         // Deactivate power-up after breaking one danger zone
//         DeactivatePowerUp();
//     }
// }




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
    [Header("Physics Movement Settings")]
    public float moveForce = 4f;        // Replaces 'speed' - controls downward force
    public float bounceForce = 7.5f;      // Replaces 'bounceHeight' - controls bounce strength
    public float maxSpeed = 5f;          // Prevents ball from moving too fast
    
    private Rigidbody rb;
    public Vector3 initialPosition;
    public bool isGameOver = false;
    private bool isGrounded = false;     // Track if ball is touching a platform

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        
        // CRITICAL: Set up physics properties
        rb.useGravity = true;            // Enable Unity's gravity
        rb.linearDamping = 0.3f;                  // Air resistance
        rb.angularDamping = 0.3f;           // Rotation resistance
        
        Debug.Log("Physics-based player initialized");
    }
    
    void Update()
    {
        if (isGameOver) 
        {
            rb.linearVelocity = Vector3.zero;  // Stop all movement when game over
            return;
        }

        // Safety: Limit maximum speed to prevent unrealistic movement
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
    
    void FixedUpdate()
    {
        if (isGameOver) return;

        // Apply constant downward force when ball is in air (not grounded)
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
            isGrounded = true;  // Ball is now touching a platform
            
            // Calculate bounce direction (mostly upward with slight randomness)
            Vector3 bounceDirection = Vector3.up;
            
            // Add some randomness based on where the ball hit the platform
            if (other.contacts.Length > 0)
            {
                ContactPoint contact = other.contacts[0];
                bounceDirection += contact.normal * 0.3f;
                bounceDirection = bounceDirection.normalized;
            }
            
            // Apply bounce force
            rb.linearVelocity = Vector3.zero;  // Reset velocity for consistent bounce
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            
            Debug.Log("Bounced on safe platform");
        }
        else if (other.gameObject.CompareTag("Danger"))
        {
            // Game over when hitting danger zone
            isGameOver = true;
            GameManager.Instance?.OnGameOver();
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;  // Freeze physics
        }
    }
    
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Safe"))
        {
            isGrounded = false;  // Ball left the platform
        }
    }
}
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // PlayerBall
    public float smoothSpeed = 5f;
    public float minYDistance = 2f;
    public float maxFollowSpeed = 15f;

    [Header("Zoom Settings")]
    public Camera cam;
    public float normalFOV = 60f;
    public float zoomOutFOV = 75f;
    public float zoomLerpSpeed = 3f;
    public float fallSpeedThreshold = 10f;
    public float tiltAngle = 20f; // how much to tilt downward
    public float tiltLerpSpeed = 2f;

    private Rigidbody targetRb;
    private float targetY;

    void Start()
    {
        if (target != null)
        {
            targetRb = target.GetComponent<Rigidbody>();
            targetY = transform.position.y;
        }

        if (cam == null)
            cam = GetComponentInChildren<Camera>();
    }

    void LateUpdate()
    {
        if (target == null || cam == null) return;
        
        // --- CAMERA TILT OVERVIEW ---
        // Rotate the whole camera rig to look downward slightly
        float currentYRotation = transform.rotation.eulerAngles.y;
        Quaternion targetRot = Quaternion.Euler(tiltAngle, currentYRotation, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * tiltLerpSpeed);


        // --- FOLLOW LOGIC ---
        float desiredY = target.position.y;

        // Only follow when the ball drops beyond a threshold
        if (desiredY < transform.position.y - minYDistance)
        {
            targetY = desiredY;
        }

        // Smoothly move the entire camera rig (and environment) down
        float newY = Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * smoothSpeed);
        newY = Mathf.MoveTowards(transform.position.y, targetY, maxFollowSpeed * Time.deltaTime);


        // --- ZOOM LOGIC ---
        float fallSpeed = Mathf.Abs(targetRb.linearVelocity.y);
        float targetFOV = fallSpeed > fallSpeedThreshold ? zoomOutFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomLerpSpeed);
        

    }   
}

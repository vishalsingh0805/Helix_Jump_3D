using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class HelixController : MonoBehaviour
{
    public float rotationSpeed = 0.1f; // Adjusted to a sensitivity factor, tweak as needed
    public Vector2 startPos;
    public bool isSwiping;

    // For continuous game let's take some references
    public GameObject stackPrefab;
    public float gapBetweenStacks = 2f;
    private GameObject ball;
    private float lowestStackY;
    private List<GameObject> stacks = new List<GameObject>();

    // Camera related references
    private Camera mainCamera;
    private Vector3 cameraTargetpos;
    private float cameraMoveStartTime;
    public float cameraMoveDuration = 0.5f;
    private bool isCameraMoving;
    public Player player;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        SpawnInitialStacks();
        mainCamera = Camera.main;
    }

    void SpawnInitialStacks()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 spawnPos = new Vector3(0, -i * gapBetweenStacks, 0);
            GameObject stack = Instantiate(stackPrefab, spawnPos, Quaternion.identity);
            if (i == 0)
            {
                stack.GetComponent<Stack>().GenerateRandomStack(true);
            }
            else
            {
                stack.GetComponent<Stack>().GenerateRandomStack();
            }
            stacks.Add(stack);
            stack.transform.SetParent(transform);
        }
        lowestStackY = -9 * gapBetweenStacks;
    }

    void Update()
    {
        if (player.isGameOver) return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                RotateHelix(touch.position.x - startPos.x);
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isSwiping = false;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButton(0) && isSwiping) // Changed to GetMouseButton for dragging
        {
            RotateHelix(Input.mousePosition.x - startPos.x);
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
        }

        CheckStackClear();

        if (isCameraMoving && mainCamera)
        {
            float t = (Time.time - cameraMoveStartTime) / cameraMoveDuration;
            if (t < 1f)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraTargetpos, t);
            }
            else
            {
                mainCamera.transform.position = cameraTargetpos;
                isCameraMoving = false;
                Debug.Log("Camera reached its position");
            }
        }
    }

    void CheckStackClear()
    {
        if (!ball) return;

        for (int i = stacks.Count - 1; i >= 0; i--)
        {
            if (stacks[i] && ball.transform.position.y < stacks[i].transform.position.y)
            {
                StackCleared(i);
                GameManager.Instance.AddAndUpdateScore(10);
                break;
            }
        }
    }

    void StackCleared(int index)
    {
        GameObject clearedStack = stacks[index];
        stacks.RemoveAt(index);
        if (clearedStack)
        {
            Destroy(clearedStack);
            Debug.Log("Stack cleared");
        }

        lowestStackY -= gapBetweenStacks;
        GameObject newStack = Instantiate(stackPrefab, new Vector3(0f, lowestStackY, 0f), Quaternion.identity, transform);
        newStack.GetComponent<Stack>().GenerateRandomStack();
        stacks.Add(newStack);
        Debug.Log("New Stack Added at pos Y: " + lowestStackY);

        if (mainCamera)
        {
            cameraTargetpos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - gapBetweenStacks, mainCamera.transform.position.z);
            cameraMoveStartTime = Time.time;
            isCameraMoving = true;
        }
    }

    void RotateHelix(float swipeDistance)
    {
        float rotation = -swipeDistance * rotationSpeed; // Rotation proportional to swipe distance
        transform.Rotate(0f, rotation, 0f);
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Stack : MonoBehaviour
// {
//     #region Unity Methods
//     void Start()
//     {
//         // GenerateRandomStack();
//     }
//     #endregion
//     public List<GameObject> stackElements = new List<GameObject>();
//     public Material SafeMaterial;
//     public Material DangerMaterial;
    
//     // ADD THIS: For empty trigger
//     private GameObject emptyTrigger;

//     public void GenerateRandomStack(bool isFirstStack = false)
//     {
//         // Clean up any existing empty trigger
//         if (emptyTrigger != null)
//         {
//             Destroy(emptyTrigger);
//         }

//         if (isFirstStack)
//         {
//             foreach (var g in stackElements)
//             {
//                 g.GetComponent<Renderer>().material = SafeMaterial;
//                 g.tag = "Safe";
//             }
//             stackElements[2].SetActive(false);
            
//             // CREATE EMPTY TRIGGER for first stack
//             CreateEmptyTrigger(stackElements[2].transform.position);
//         }
//         else
//         {
//             int totalStacksTillNow = (int)GameManager.Instance.PlayerScore / 10;
//             int difficultyLevel = totalStacksTillNow / 3;
            
//             int totalDangerElementsCount;
//             if (difficultyLevel == 0 || difficultyLevel == 1)
//             {
//                 totalDangerElementsCount = Random.Range(1, 3);
//             }
//             else if (difficultyLevel == 2)
//             {
//                 totalDangerElementsCount = Random.Range(1, 3);
//             }
//             else if (difficultyLevel == 3)
//             {
//                 totalDangerElementsCount = Random.Range(2, 4);
//             }
//             else
//             {
//                 totalDangerElementsCount = Random.Range(2, 5);
//             }

//             int emptyElementIndex = Random.Range(0, stackElements.Count);
            
//             List<int> dangerElementIndicesList = new List<int>();
//             for (int i = 0; i < totalDangerElementsCount; i++)
//             {
//                 int foundIndex;
//                 do
//                 {
//                     foundIndex = Random.Range(0, stackElements.Count);
//                 }
//                 while (foundIndex == emptyElementIndex || dangerElementIndicesList.Contains(foundIndex));
//                 dangerElementIndicesList.Add(foundIndex);
//             }

//             foreach (var g in stackElements)
//             {
//                 g.GetComponent<Renderer>().material = SafeMaterial;
//                 g.tag = "Safe";
//                 g.SetActive(true); // Ensure all are active initially
//             }

//             stackElements[emptyElementIndex].SetActive(false);
            
//             // CREATE EMPTY TRIGGER for this stack
//             CreateEmptyTrigger(stackElements[emptyElementIndex].transform.position);

//             foreach (int index in dangerElementIndicesList)
//             {
//                 stackElements[index].GetComponent<Renderer>().material = DangerMaterial;
//                 stackElements[index].tag = "Danger";
//             }
//         }
//     }
    
//     // ADD THIS METHOD: Create empty trigger
//     private void CreateEmptyTrigger(Vector3 position)
//     {
//         emptyTrigger = new GameObject("EmptyTrigger");
//         emptyTrigger.transform.position = position;
//         emptyTrigger.transform.SetParent(this.transform);
        
//         // Use tag-based detection
//         emptyTrigger.tag = "Empty";
        
//         BoxCollider triggerCollider = emptyTrigger.AddComponent<BoxCollider>();
//         triggerCollider.isTrigger = true;
//         triggerCollider.size = new Vector3(1.2f, 0.2f, 1.2f);
//     }
    
//     // ADD THIS: Clean up when stack is destroyed
//     void OnDestroy()
//     {
//         if (emptyTrigger != null)
//         {
//             Destroy(emptyTrigger);
//         }
//     }
// }


// this is the improved version with debug logs

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    #region Unity Methods
    void Start()
    {
        // GenerateRandomStack();
    }
    #endregion
    public List<GameObject> stackElements = new List<GameObject>();
    public Material SafeMaterial;
    public Material DangerMaterial;
    
    private GameObject emptyTrigger;

    public void GenerateRandomStack(bool isFirstStack = false)
    {
        Debug.Log($"🔄 Generating stack - First stack: {isFirstStack}");
        
        // Clean up any existing empty trigger
        if (emptyTrigger != null)
        {
            Destroy(emptyTrigger);
            Debug.Log("🧹 Cleaned up previous empty trigger");
        }

        if (isFirstStack)
        {
            Debug.Log("🏁 Setting up first stack");
            foreach (var g in stackElements)
            {
                g.GetComponent<Renderer>().material = SafeMaterial;
                g.tag = "Safe";
            }
            stackElements[2].SetActive(false);
            
            // CREATE EMPTY TRIGGER for first stack
            CreateEmptyTrigger(stackElements[2].transform.position);
        }
        else
        {
            Debug.Log("🎲 Generating random stack");
            int totalStacksTillNow = (int)GameManager.Instance.PlayerScore / 10;
            int difficultyLevel = totalStacksTillNow / 3;
            
            int totalDangerElementsCount;
            if (difficultyLevel == 0 || difficultyLevel == 1)
            {
                totalDangerElementsCount = Random.Range(1, 3);
            }
            else if (difficultyLevel == 2)
            {
                totalDangerElementsCount = Random.Range(1, 3);
            }
            else if (difficultyLevel == 3)
            {
                totalDangerElementsCount = Random.Range(2, 4);
            }
            else
            {
                totalDangerElementsCount = Random.Range(2, 5);
            }

            int emptyElementIndex = Random.Range(0, stackElements.Count);
            Debug.Log($"🎯 Empty element index: {emptyElementIndex}");
            
            List<int> dangerElementIndicesList = new List<int>();
            for (int i = 0; i < totalDangerElementsCount; i++)
            {
                int foundIndex;
                do
                {
                    foundIndex = Random.Range(0, stackElements.Count);
                }
                while (foundIndex == emptyElementIndex || dangerElementIndicesList.Contains(foundIndex));
                dangerElementIndicesList.Add(foundIndex);
            }

            Debug.Log($"⚠️ Danger elements: {string.Join(", ", dangerElementIndicesList)}");

            foreach (var g in stackElements)
            {
                g.GetComponent<Renderer>().material = SafeMaterial;
                g.tag = "Safe";
                g.SetActive(true);
            }

            stackElements[emptyElementIndex].SetActive(false);
            
            // CREATE EMPTY TRIGGER for this stack
            CreateEmptyTrigger(stackElements[emptyElementIndex].transform.position);

            foreach (int index in dangerElementIndicesList)
            {
                stackElements[index].GetComponent<Renderer>().material = DangerMaterial;
                stackElements[index].tag = "Danger";
            }
            
            Debug.Log("✅ Stack generation complete");
        }
    }
    
    private void CreateEmptyTrigger(Vector3 position)
    {
        emptyTrigger = new GameObject("EmptyTrigger");
        emptyTrigger.transform.position = position;
        emptyTrigger.transform.SetParent(this.transform);
        
        // Use tag-based detection
        emptyTrigger.tag = "Empty";
        Debug.Log($"🏷️ Created EmptyTrigger at position: {position} with tag: {emptyTrigger.tag}");
        
        BoxCollider triggerCollider = emptyTrigger.AddComponent<BoxCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector3(1.5f, 0.3f, 1.5f); // Slightly larger to ensure detection
        
        Debug.Log($"📦 EmptyTrigger collider size: {triggerCollider.size}");
    }
    
    void OnDestroy()
    {
        if (emptyTrigger != null)
        {
            Destroy(emptyTrigger);
        }
    }
}
*/
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DG.Tweening;

// public class Stack : MonoBehaviour
// {
//     #region Unity Methods
//     void Start()
//     {
//         // GenerateRandomStack();
//     }
//     #endregion
    


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stack : MonoBehaviour
{
    #region Unity Methods
    void Start()
    {
        // GenerateRandomStack();
    }
    #endregion
    
    public List<GameObject> stackElements = new List<GameObject>();
    public Material SafeMaterial;
    public Material DangerMaterial;
    
    private GameObject emptyTrigger;

    // Animation Properties
    [Header("Breaking Animation")]
    public float breakForce = 5f;
    public float fadeDuration = 1f;
    private bool isBreaking = false;

    public void GenerateRandomStack(bool isFirstStack = false)
    {
        Debug.Log($"🔄 Generating stack - First stack: {isFirstStack}");
        
        // Reset breaking state
        isBreaking = false;
        
        // Clean up any existing empty trigger
        if (emptyTrigger != null)
        {
            Destroy(emptyTrigger);
            Debug.Log("🧹 Cleaned up previous empty trigger");
        }

        if (isFirstStack)
        {
            Debug.Log("🏁 Setting up first stack - ALL SAFE WITH ONE EMPTY");
            
            // FIRST STACK: All safe zones with one empty space
            int emptyElementIndex = 2; // You can randomize this: Random.Range(0, stackElements.Count);
            
            foreach (var g in stackElements)
            {
                if (g != null)
                {
                    g.GetComponent<Renderer>().material = SafeMaterial;
                    g.tag = "Safe";
                    g.SetActive(true);
                }
            }

            // Set one element as empty (inactive)
            if (stackElements[emptyElementIndex] != null)
            {
                stackElements[emptyElementIndex].SetActive(false);
                Debug.Log($"First stack empty piece at index: {emptyElementIndex}");
            }
            
            // CREATE EMPTY TRIGGER for first stack
            CreateEmptyTrigger(stackElements[emptyElementIndex].transform.position);
        }
        else
        {
            Debug.Log("🎲 Generating random stack");
            int totalStacksTillNow = (int)GameManager.Instance.PlayerScore / 10;
            int difficultyLevel = totalStacksTillNow / 3;
            
            int totalDangerElementsCount;
            if (difficultyLevel == 0 || difficultyLevel == 1)
            {
                totalDangerElementsCount = Random.Range(1, 3);
            }
            else if (difficultyLevel == 2)
            {
                totalDangerElementsCount = Random.Range(1, 3);
            }
            else if (difficultyLevel == 3)
            {
                totalDangerElementsCount = Random.Range(2, 4);
            }
            else
            {
                totalDangerElementsCount = Random.Range(2, 5);
            }

            int emptyElementIndex = Random.Range(0, stackElements.Count);
            Debug.Log($"🎯 Empty element index: {emptyElementIndex}");
            
            List<int> dangerElementIndicesList = new List<int>();
            for (int i = 0; i < totalDangerElementsCount; i++)
            {
                int foundIndex;
                do
                {
                    foundIndex = Random.Range(0, stackElements.Count);
                }
                while (foundIndex == emptyElementIndex || dangerElementIndicesList.Contains(foundIndex));
                dangerElementIndicesList.Add(foundIndex);
            }

            Debug.Log($"⚠️ Danger elements: {string.Join(", ", dangerElementIndicesList)}");

            foreach (var g in stackElements)
            {
                if (g != null)
                {
                    g.GetComponent<Renderer>().material = SafeMaterial;
                    g.tag = "Safe";
                    g.SetActive(true);
                }
            }

            stackElements[emptyElementIndex].SetActive(false);
            
            // CREATE EMPTY TRIGGER for this stack
            CreateEmptyTrigger(stackElements[emptyElementIndex].transform.position);

            foreach (int index in dangerElementIndicesList)
            {
                if (stackElements[index] != null)
                {
                    stackElements[index].GetComponent<Renderer>().material = DangerMaterial;
                    stackElements[index].tag = "Danger";
                }
            }
            
            Debug.Log("✅ Stack generation complete");
        }
    }
    
    private void CreateEmptyTrigger(Vector3 position)
    {
        emptyTrigger = new GameObject("EmptyTrigger");
        emptyTrigger.transform.position = position;
        emptyTrigger.transform.SetParent(this.transform);
        
        // Use tag-based detection
        emptyTrigger.tag = "Empty";
        Debug.Log($"🏷️ Created EmptyTrigger at position: {position} with tag: {emptyTrigger.tag}");
        
        BoxCollider triggerCollider = emptyTrigger.AddComponent<BoxCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector3(1.5f, 0.3f, 1.5f);
        
        Debug.Log($"📦 EmptyTrigger collider size: {triggerCollider.size}");
    }

    // Breaking Animation Method
    public void BreakStack()
    {
        if (isBreaking) return;
        
        isBreaking = true;
        Debug.Log("Starting stack break animation");
        
        // Break each active piece
        foreach (var piece in stackElements)
        {
            if (piece != null && piece.activeInHierarchy)
            {
                StartCoroutine(BreakPiece(piece));
            }
        }
        
        // Destroy the empty trigger immediately
        if (emptyTrigger != null)
        {
            Destroy(emptyTrigger);
        }
        
        // Destroy the parent stack object after animation completes
        Destroy(gameObject, fadeDuration + 1f);
    }
    
    // Individual piece breaking coroutine
    private IEnumerator BreakPiece(GameObject piece)
    {
        // Add Rigidbody for physics
        Rigidbody rb = piece.AddComponent<Rigidbody>();
        
        // Apply random force to make pieces fly away
        Vector3 randomForce = new Vector3(
            Random.Range(-breakForce, breakForce),
            Random.Range(0, breakForce),
            Random.Range(-breakForce, breakForce)
        );
        rb.AddForce(randomForce, ForceMode.Impulse);
        
        // Apply random rotation
        rb.AddTorque(new Vector3(
            Random.Range(-breakForce, breakForce),
            Random.Range(-breakForce, breakForce),
            Random.Range(-breakForce, breakForce)
        ));
        
        // Wait a moment before starting scale animation
        yield return new WaitForSeconds(0.3f);
        
        // Scale down animation using DOTween
        if (piece != null)
        {
            piece.transform.DOScale(0f, fadeDuration).OnComplete(() => {
                if (piece != null)
                {
                    Destroy(piece);
                }
            });
        }
    }
    
    void OnDestroy()
    {
        if (emptyTrigger != null)
        {
            Destroy(emptyTrigger);
        }
    }
}
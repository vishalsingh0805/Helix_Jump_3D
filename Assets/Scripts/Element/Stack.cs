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

//     //Element 1: empty krne ke liye jisme se ball neeche jayegi
//     //Element 2,3: Danger zone keliye jisme se ball neeche nahi jayegi and game over ho jayegi
//     public void GenerateRandomStack(bool isFirstStack = false)
//     {
//         //Element 1: Empty element
//         if (isFirstStack)
//         {
//             foreach (var g in stackElements)
//             {
//                 g.GetComponent<Renderer>().material = SafeMaterial; // Default to safe material
//                 g.tag = "Safe";
//             }
//             stackElements[2].SetActive(false);
//         }
//         else
//         {
//             //total stacks ki value leni h
//             int totalStacksTillNow = (int)GameManager.Instance.PlayerScore / 10;
//             int difficultyLevel = totalStacksTillNow / 3;
//             //difficulty level is 0 or 1 = 1 danger element
//             //difficulty is 2 = 1, 2 danger element
//             //difficulty is 3 = 2,3 danger elements
//             //difficulty is >=4 => 2,3,4 danger elements
//             int totalDangerElementsCount;
//             if (difficultyLevel == 0 || difficultyLevel == 1)
//             {
//                 totalDangerElementsCount = Random.Range(1, 3);//1,2
//             }
//             else if (difficultyLevel == 2)
//             {
//                 totalDangerElementsCount = Random.Range(1, 3);//1,2
//             }
//             else if (difficultyLevel == 3)
//             {
//                 totalDangerElementsCount = Random.Range(2, 4);//2,3
//             }
//             else
//             {
//                 totalDangerElementsCount = Random.Range(2, 5);//2,3,4
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
//                 g.GetComponent<Renderer>().material = SafeMaterial; // Default to safe material
//                 g.tag = "Safe";
//             }

//             stackElements[emptyElementIndex].SetActive(false);

//             foreach (int index in dangerElementIndicesList)
//             {
//                 stackElements[index].GetComponent<Renderer>().material = DangerMaterial;
//                 stackElements[index].tag = "Danger";
//                 stackElements[index].GetComponent<Renderer>().material = DangerMaterial;
//                 stackElements[index].tag = "Danger";
//             }
//         }

//     }
// }


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




using System.Collections;
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
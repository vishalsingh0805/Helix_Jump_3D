// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance;
//     public Player player;

//     // 🧠 Local leaderboard manager reference (optional)
//     public LocalLeaderboardManager localLeaderboardManager;

//     void Awake()
//     {
//         Instance = this;
//     }

//     void Start()
//     {
//         playerHighScore = PlayerPrefs.GetFloat("PlayerHighscore", 0);
//     }

//     [Header("UI Elements")]
//     public TMP_Text ScoreText;
//     public GameObject GameoverPanel;
//     public TMP_Text ScoreOnGameOverText;
//     public TMP_Text HighScoreOnGameOverText;

//     [Header("Player Score Info")]
//     public float PlayerScore = 0;
//     private float playerHighScore = 0;

//     // 🧩 Add points and update score
//     public void AddAndUpdateScore(float scoreToAdd)
//     {
//         //ManageDifficulty();
//         PlayerScore += scoreToAdd;
//         ScoreText.text = $"SCORE: {PlayerScore}";

//         if (PlayerScore > playerHighScore)
//         {
//             playerHighScore = PlayerScore;
//             PlayerPrefs.SetFloat("PlayerHighscore", PlayerScore);
//         }
//     }

//     // 🧠 Gradually increase difficulty
//     void ManageDifficulty()
//     {
//         if (player != null && player.speed < 4)
//         {
//             player.speed += 0.15f;
//         }
//     }

//     // 💀 When player dies
//     public void OnGameOver()
//     {
//         GameoverPanel.SetActive(true);
//         ScoreOnGameOverText.text = $"SCORE: {PlayerScore}";
//         HighScoreOnGameOverText.text = $"HIGHSCORE: {playerHighScore}";

//         // 🏆 Save score locally
//         if (localLeaderboardManager != null)
//         {
//             string playerName = PlayerPrefs.GetString("Username", "Player");
//             localLeaderboardManager.AddScore(playerName, Mathf.RoundToInt(PlayerScore));
//         }
//     }

//     // 🔁 Restart scene
//     public void RestartTheGame()
//     {
//         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//     }

//     // 🧹 Reset all local data (username, highscore, leaderboard)
//     public void ResetLocalData()
//     {
//         PlayerPrefs.DeleteAll();
//         PlayerPrefs.Save();

//         if (localLeaderboardManager != null)
//         {
//             localLeaderboardManager.ResetLeaderboard();
//         }

//         Debug.Log("✅ Local PlayerPrefs and leaderboard cleared!");
//     }
// }




using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;

    // 🧠 Local leaderboard manager reference (optional)
    public LocalLeaderboardManager localLeaderboardManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerHighScore = PlayerPrefs.GetFloat("PlayerHighscore", 0);
    }

    [Header("UI Elements")]
    public TMP_Text ScoreText;
    public GameObject GameoverPanel;
    public TMP_Text ScoreOnGameOverText;
    public TMP_Text HighScoreOnGameOverText;

    [Header("Player Score Info")]
    public float PlayerScore = 0;
    private float playerHighScore = 0;

    // 🧩 Add points and update score
    public void AddAndUpdateScore(float scoreToAdd)
    {
        //ManageDifficulty();
        PlayerScore += scoreToAdd;
        ScoreText.text = $"SCORE: {PlayerScore}";

        if (PlayerScore > playerHighScore)
        {
            playerHighScore = PlayerScore;
            PlayerPrefs.SetFloat("PlayerHighscore", PlayerScore);
        }
    }

    // 🧠 Gradually increase difficulty
    void ManageDifficulty()
    {
        if (player != null && player.moveForce < 20f)
        {
            player.moveForce += 0.15f;
        }
    }

    // 💀 When player dies
    public void OnGameOver()
    {
        GameoverPanel.SetActive(true);
        ScoreOnGameOverText.text = $"SCORE: {PlayerScore}";
        HighScoreOnGameOverText.text = $"HIGHSCORE: {playerHighScore}";

        // 🏆 Save score locally
        if (localLeaderboardManager != null)
        {
            string playerName = PlayerPrefs.GetString("Username", "Player");
            localLeaderboardManager.AddScore(playerName, Mathf.RoundToInt(PlayerScore));
        }
    }

    // 🔁 Restart scene
    public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 🧹 Reset all local data (username, highscore, leaderboard)
    public void ResetLocalData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        if (localLeaderboardManager != null)
        {
            localLeaderboardManager.ResetLeaderboard();
        }

        Debug.Log("✅ Local PlayerPrefs and leaderboard cleared!");
    }
}
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerScoreEntry
{
    public string playerName;
    public int score;
}

public class LocalLeaderboardManager : MonoBehaviour
{
    [Header("Leaderboard UI References")]
    public Transform leaderboardContent;     // GameOverPanel → Scroll View → Viewport → Content
    public GameObject playerItemPrefab;      // Prefab with 3 TMP_Texts (Rank, Name, Score)

    [Header("Username Input")]
    public GameObject usernamePanel;         // Canvas → UsernamePanel
    public TMP_InputField usernameInput;     // UsernamePanel → InputField (TMP)

    private const string leaderboardKey = "LocalLeaderboard";
    private const string usernameKey = "Username";

    private List<PlayerScoreEntry> leaderboard = new List<PlayerScoreEntry>();

    void Awake()
    {
        LoadLeaderboard();

        // Only show the username panel once
        if (!PlayerPrefs.HasKey(usernameKey) && usernamePanel)
        {
            usernamePanel.SetActive(true);
        }
        else if (usernamePanel)
        {
            usernamePanel.SetActive(false);
        }
    }

    public void OnSubmitUsername()
    {
        if (usernameInput == null) return;

        string enteredName = usernameInput.text.Trim();
        if (string.IsNullOrEmpty(enteredName))
            enteredName = "Player";

        PlayerPrefs.SetString(usernameKey, enteredName);
        PlayerPrefs.Save();

        if (usernamePanel) usernamePanel.SetActive(false);
    }

    public string GetUsername()
    {
        return PlayerPrefs.GetString(usernameKey, "Player");
    }

    public void AddScore(string name, int score)
    {
        leaderboard.Add(new PlayerScoreEntry { playerName = name, score = score });
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));
        if (leaderboard.Count > 10) leaderboard.RemoveAt(10);
        SaveLeaderboard();
    }

    public void DisplayLeaderboard()
    {
        // Defensive checks
        if (leaderboardContent == null)
        {
            Debug.LogWarning("⚠️ Leaderboard Content (ScrollView/Content) not assigned!");
            return;
        }
        if (playerItemPrefab == null)
        {
            Debug.LogWarning("⚠️ Player Item Prefab not assigned!");
            return;
        }

        // Clear previous entries
        foreach (Transform child in leaderboardContent)
            Destroy(child.gameObject);

        // Add current entries
        for (int i = 0; i < leaderboard.Count; i++)
        {
            GameObject entry = Instantiate(playerItemPrefab, leaderboardContent);
            TMP_Text[] texts = entry.GetComponentsInChildren<TMP_Text>();

            if (texts.Length >= 3)
            {
                texts[0].text = (i + 1).ToString();
                texts[1].text = leaderboard[i].playerName;
                texts[2].text = leaderboard[i].score.ToString();
            }
        }
    }

    public void ResetLeaderboard()
    {
        leaderboard.Clear();
        PlayerPrefs.DeleteKey(leaderboardKey);
        PlayerPrefs.Save();
    }

    private void SaveLeaderboard()
    {
        string json = JsonUtility.ToJson(new Wrapper { entries = leaderboard });
        PlayerPrefs.SetString(leaderboardKey, json);
        PlayerPrefs.Save();
    }

    private void LoadLeaderboard()
    {
        leaderboard.Clear();
        if (PlayerPrefs.HasKey(leaderboardKey))
        {
            string json = PlayerPrefs.GetString(leaderboardKey);
            Wrapper data = JsonUtility.FromJson<Wrapper>(json);
            if (data != null && data.entries != null)
                leaderboard = data.entries;
        }
    }

    [System.Serializable]
    private class Wrapper { public List<PlayerScoreEntry> entries; }
}

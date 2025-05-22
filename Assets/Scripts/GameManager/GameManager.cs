using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string currentPlayerName;
    public int currentScore;

    [System.Serializable]
    public class HighScoreEntry
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    public class HighScoreData
    {
        public List<HighScoreEntry> highScores = new List<HighScoreEntry>();
    }

    public HighScoreData highScoreData = new HighScoreData();
    private string savePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Application.persistentDataPath + "/highscores.json";
        LoadHighScores();
    }

    public void StartGame(string playerName)
    {
        currentPlayerName = playerName;
        currentScore = 0;
        SceneManager.LoadScene("main");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void AddScore(int score)
    {
        currentScore = score;
        SaveScoreIfHighEnough();
    }
    private void SaveScoreIfHighEnough()
    {
        highScoreData.highScores.Add(new HighScoreEntry { playerName = currentPlayerName, score = currentScore });
        highScoreData.highScores.Sort((a, b) => b.score.CompareTo(a.score));

        if (highScoreData.highScores.Count > 5)
        {
            highScoreData.highScores.RemoveAt(5); 
        }

        SaveHighScores();
    }
    private void SaveHighScores()
    {
        string json = JsonUtility.ToJson(highScoreData, true);
        File.WriteAllText(savePath, json);
    }

    private void LoadHighScores()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            highScoreData = JsonUtility.FromJson<HighScoreData>(json);
        }
    }
}

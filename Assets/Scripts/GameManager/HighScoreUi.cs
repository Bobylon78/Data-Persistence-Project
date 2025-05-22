using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreUi : MonoBehaviour
{
    public Text HighScoreText;
    // Start is called before the first frame update
    void Start()
    {
        HighScoreText.text = ""; // Réinitialise le texte

        var scores = GameManager.Instance.highScoreData.highScores;

        for (int i = 0; i < Mathf.Min(5, scores.Count); i++)
        {
            var entry = scores[i];
            HighScoreText.text += $"n°{i + 1} - {entry.score} - {entry.playerName}\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}

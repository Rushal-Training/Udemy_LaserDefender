using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
	TextMeshProUGUI scoreText;
	GameSession gameSession;

    void Start()
    {
		scoreText = GetComponent<TextMeshProUGUI>();
		gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
		scoreText.text = gameSession.Score.ToString();
    }
}

using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [Header("UI Text")]
    [SerializeField] GameObject score;
    [SerializeField] GameObject highScore;
    [SerializeField] GameObject highScoreLabel;

    public void ShowGameOverScreen(int playerScore, int gamehighScore)
    {
        HiddeAll();

        score.SetActive(true);
        highScore.SetActive(true);

        score.GetComponent<TextMeshProUGUI>().text = "You score: " + playerScore;
        highScore.GetComponent<TextMeshProUGUI>().text = "High score: " + gamehighScore;
    }

    public void ShowGameOverScreenHighScore(int playerScore)
    {
        HiddeAll();

        score.SetActive(false);
        highScore.SetActive(true);
        highScoreLabel.SetActive(true);

        score.GetComponent<TextMeshProUGUI>().text = "You score: " + playerScore;
        highScore.GetComponent<TextMeshProUGUI>().text = "High score: " + playerScore;
    }

    void HiddeAll()
    {
        score.SetActive(false);
        highScore.SetActive(false);
        highScoreLabel.SetActive(false);
    }
}

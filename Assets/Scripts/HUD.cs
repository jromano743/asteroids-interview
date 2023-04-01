using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject livesImage;
    List<GameObject> livesImages;
    int lives;

    private void Start()
    {
        livesImages = new List<GameObject>();

        SetLives(GameManager.instance.GetLives());
        InitHUD();
    }

    public void AddPoints(int points)
    {
        scoreText.text = "Score: " + points;
    }

    public void DecrementLives()
    {
        livesImages[lives].SetActive(false);
        lives--;
    }

    public void InitHUD()
    {
        lives = livesImages.Count - 1;
        scoreText.text = "Score: 0";

        foreach (GameObject live in livesImages)
        {
            live.SetActive(true);
        }
    }

    public void SetLives(int newLives = 3)
    {
        Transform livesParent = livesImage.transform.parent;

        lives = newLives;

        livesImages.Add(livesImage);
        Vector3 spawnposition = livesImage.transform.position;

        for (int i = 1; i < lives; i++)
        {
            spawnposition = new Vector3(spawnposition.x + 25, spawnposition.y, spawnposition.z);

            GameObject newLive = Instantiate(livesImage, spawnposition, livesImage.transform.rotation);
            newLive.transform.SetParent(livesParent);
            livesImages.Add(newLive);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player config")]
    [SerializeField] PlayerController player;
    [SerializeField] ParticleSystem particleExplosion;

    [Header("References")]
    [SerializeField] HUD HUD_Ref;
    [SerializeField] GameOverScreen GameOver_Ref;

    [Header("Settings")]
    [SerializeField] float respawnTime = 3.0f;
    [SerializeField] float respawnInvulnerability = 3.0f;
    [SerializeField] int initLives = 3;
    [SerializeField] int lives = 3;
    [SerializeField] int score = 0;
    int highScore = 0;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        lives = initLives;
        LoadData();
    }

    public void AsteroidDestroyed(Transform eliminatedPosition, float asteroidSize)
    {
        particleExplosion.transform.position = eliminatedPosition.position;
        particleExplosion.Play();

        AddPoints(asteroidSize);
    }

    private void AddPoints(float asteroidSize)
    {
        if(asteroidSize < 0.75f)
        {
            score += 100;
        }
        else if(asteroidSize < 1.2f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }

        HUD_Ref.AddPoints(score);
    }

    public void PlayerDied()
    {
        particleExplosion.transform.position = player.transform.position;
        particleExplosion.Play();

        lives--;
        HUD_Ref.DecrementLives();

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }
    }

    void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        player.gameObject.SetActive(true);
        player.Respawn(); //Play respawn sound
        
        Invoke(nameof(TurnCollisions), respawnInvulnerability);
    }

    void TurnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void GameOver()
    {
        if (score > highScore)
        {
            GameOver_Ref.ShowGameOverScreenHighScore(score);
            highScore = score;
            SaveData();
        }
        else
        {
            GameOver_Ref.ShowGameOverScreen(score, highScore);
        }

        GameOver_Ref.gameObject.SetActive(true);
    }

    public void ResetGame()
    {
        lives = initLives;
        score = 0;

        ClearLevel();

        Invoke(nameof(Respawn), respawnTime);
        HUD_Ref.InitHUD();
        GameOver_Ref.gameObject.SetActive(false);
    }

    void ClearLevel()
    {
        GameObject[] asteroidsInGame = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (GameObject asteroid in asteroidsInGame)
        {
            Destroy(asteroid);
        }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public int GetLives()
    {
        return lives;
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("highScore", highScore);
        PlayerPrefs.Save();
    }

    void LoadData()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }

    [ContextMenu("Reset Player Data")]
    void ResetPlayerPref()
    {
        PlayerPrefs.SetInt("highScore", 0);
    }
}

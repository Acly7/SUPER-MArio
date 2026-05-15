using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    // NUEVAS VARIABLES
    public int score { get; private set; }
    public float time = 300f;

    // TEXTOS UI
    public TextMeshProUGUI tiempoTXT;
    public TextMeshProUGUI monedasTXT;
    public TextMeshProUGUI puntajeTXT;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        NewGame();
    }

    // ACTUALIZAR CONTADORES
    private void Update()
    {
        time -= Time.deltaTime;

        if (tiempoTXT != null)
            tiempoTXT.text = "Tiempo: " + Mathf.Round(time);

        if (monedasTXT != null)
            monedasTXT.text = "Monedas: " + coins;

        if (puntajeTXT != null)
            puntajeTXT.text = "Puntaje: " + score;
    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;
        score = 0;
        time = 300f;

        LoadLevel(1, 1);
    }

    public void GameOver()
    {
        // TODO: show game over screen

        NewGame();
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        CancelInvoke(nameof(ResetLevel));
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if (lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    // MONEDAS
    public void AddCoin()
    {
        coins++;
        score += 100;

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
    }

    // VIDAS
    public void AddLife()
    {
        lives++;
    }

    // PUNTOS POR ENEMIGO
    public void AddEnemyPoints()
    {
        score += 200;
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] public DifficultySettings difficultySettings;
    private UIManager uiManager;

    public static GameManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadGameScene());
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }

    IEnumerator LoadGameScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("GameScene");

        uiManager.ShowLoadScreen();

        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f; // Normalize progress to 0-1 range
            uiManager.loadingBar.value = progress; // Update the loading bar fill amount
            
            yield return null; // Wait for the next frame
        }
    }
}

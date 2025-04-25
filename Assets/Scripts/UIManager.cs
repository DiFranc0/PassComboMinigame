using TMPro;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] public Slider loadingBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (comboText != null) 
            comboText.text = "";

        if(loadScreen != null)
            loadingBar = loadScreen.GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        ScoreManager.OnScoreUpdated += UpdateScore;
        ScoreManager.OnComboCountUpdated += UpdateComboCount;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreUpdated -= UpdateScore;
        ScoreManager.OnComboCountUpdated -= UpdateComboCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLoadScreen()
    {
        loadScreen.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    public void UpdateComboCount(int comboCount)
    {
        if (comboCount > 1 && GameManager.Instance.difficultySettings.numberOfPassesToCombo != 0)
        {
            comboText.text = "COMBO: x" + comboCount.ToString();
        }
        else
        {
            comboText.text = "";
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = "TIME LEFT: " + seconds.ToString();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        // Show game over screen
    }
}

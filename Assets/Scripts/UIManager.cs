using System.Collections;
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
    [SerializeField] private TMP_Text addedScoreText;
    [SerializeField] private TMP_Text addedPerfectScoreText;
    [SerializeField] public Slider loadingBar;
    [SerializeField] private TMP_Text scoreTextGameOver;

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
        TargetController.OnPerfectPass += PerfectTrigger;
        PlayerScript.OnMistakeMade += MistakeTrigger;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreUpdated -= UpdateScore;
        ScoreManager.OnComboCountUpdated -= UpdateComboCount;
        TargetController.OnPerfectPass -= PerfectTrigger;
        PlayerScript.OnMistakeMade -= MistakeTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLoadScreen()
    {
        loadScreen.SetActive(true);
    }

    public void UpdateScore(int score, int scoreAdded)
    {
        scoreText.text = "SCORE: " + score.ToString();
        if (scoreAdded > 0)
        {
            StartCoroutine(OnHit(scoreAdded));
        }
        else
        {
            StartCoroutine(OnMistake());
        }
    }

    public void UpdateComboCount(int comboCount)
    {
        if (comboCount > 1 && GameManager.Instance.difficultySettings.numberOfPassesToCombo != 0)
        {
            comboText.text = "COMBO: x " + comboCount.ToString();
        }
        else
        {
            comboText.text = "";
        }
    }

    public void PerfectTrigger()
    {
        StartCoroutine(OnPerfectHit());
    }

    public void MistakeTrigger()
    {
        StartCoroutine(OnMistake());
    }

    IEnumerator OnHit(int scoreAdded)
    {
        addedScoreText.color = Color.green;
        addedScoreText.text = "+" + scoreAdded.ToString();
        addedScoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        addedScoreText.gameObject.SetActive(false);
    }

    IEnumerator OnPerfectHit()
    {
        addedPerfectScoreText.color = Color.yellow;
        addedPerfectScoreText.text = "+" + GameManager.Instance.difficultySettings.perfectTimingBonus.ToString();
        addedPerfectScoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        addedPerfectScoreText.gameObject.SetActive(false);
    }

    IEnumerator OnMistake()
    {
        addedScoreText.color = Color.red;
        addedScoreText.text = "-" + GameManager.Instance.difficultySettings.mistakePointLoss.ToString();
        addedScoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        addedScoreText.gameObject.SetActive(false);
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
        scoreTextGameOver.text = scoreText.text;
        // Show game over screen
    }
}

using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    private int comboCount;
    private DifficultySettings difficultySettings;

    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnComboCountUpdated;

    private void OnEnable()
    {
        TargetController.OnTargetHit += AddScore;
        PlayerScript.OnMistakeMade += MadeMistake;
    }

    private void OnDisable()
    {
        TargetController.OnTargetHit -= AddScore;
        PlayerScript.OnMistakeMade -= MadeMistake;
    }

    private void Start()
    {
        difficultySettings = GameManager.Instance.difficultySettings;

        score = 0;
        comboCount = 0;
    }

    private void Update()
    {
        
    }

    public void AddScore()
    {
        score++;
        comboCount++;
        if (comboCount >= difficultySettings.numberOfPassesToCombo &&
            difficultySettings.numberOfPassesToCombo != 0)
        {
            score = score + difficultySettings.extraComboPoints;
        }
        OnScoreUpdated?.Invoke(score);
        OnComboCountUpdated?.Invoke(comboCount);
    }

    public void MadeMistake()
    {
        score -= difficultySettings.mistakePointLoss;
        comboCount = 0;
        OnScoreUpdated?.Invoke(score);
        OnComboCountUpdated?.Invoke(comboCount);
    }
}

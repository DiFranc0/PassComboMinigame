using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    private int perfectTimingBonus = 5; // Bonus points for perfect timing
    private int comboCount;
    private DifficultySettings difficultySettings;

    public static event Action<int, int> OnScoreUpdated;
    public static event Action<int> OnComboCountUpdated;

    private void OnEnable()
    {
        TargetController.OnTargetHit += AddScore;
        TargetController.OnPerfectPass += AddPerfetTimingBonus;
        PlayerScript.OnMistakeMade += MadeMistake;
    }

    private void OnDisable()
    {
        TargetController.OnTargetHit -= AddScore;
        TargetController.OnPerfectPass -= AddPerfetTimingBonus;
        PlayerScript.OnMistakeMade -= MadeMistake;
    }

    private void Start()
    {
        difficultySettings = GameManager.Instance.difficultySettings;
        perfectTimingBonus = difficultySettings.perfectTimingBonus;

        score = 0;
        comboCount = 0;
    }

    public void AddScore()
    {
        int scoreAdded = 1;
        score++;
        comboCount++;

        // Give extra points for combos
        if (comboCount >= difficultySettings.numberOfPassesToCombo &&
            difficultySettings.numberOfPassesToCombo != 0)
        {
            scoreAdded += difficultySettings.extraComboPoints;
            score += difficultySettings.extraComboPoints;
        }

        OnScoreUpdated?.Invoke(score, scoreAdded);
        OnComboCountUpdated?.Invoke(comboCount);
    }

    public void AddPerfetTimingBonus()
    {
        score += perfectTimingBonus;
        OnScoreUpdated?.Invoke(score, perfectTimingBonus);
    }

    public void MadeMistake()
    {
        int scoreSubtracted = -difficultySettings.mistakePointLoss;
        score -= difficultySettings.mistakePointLoss;
        comboCount = 0;
        OnScoreUpdated?.Invoke(score, scoreSubtracted);
        OnComboCountUpdated?.Invoke(comboCount);
    }
}

using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    public void SetEasy() => SetDifficulty(DifficultySettings.Difficulty.Easy);
    public void SetMedium() => SetDifficulty(DifficultySettings.Difficulty.Medium);
    public void SetHard() => SetDifficulty(DifficultySettings.Difficulty.Hard);

    private void SetDifficulty(DifficultySettings.Difficulty difficulty)
    {
       GameManager.Instance.difficultySettings.ApplyPreset(difficulty);
    }
}

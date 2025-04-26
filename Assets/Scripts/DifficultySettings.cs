using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "Scriptable Objects/Difficulty Settings")]
public class DifficultySettings : ScriptableObject
{
    public enum Difficulty { Easy, Medium, Hard }

    [Header("Game Parameters")]
    public int mistakePointLoss = 1;
    public int teammateCount = 4;
    public float targetSize = 1f;
    public float targetActiveTime = 1f;
    public bool comboPatternsEnabled = false;
    public int numberOfPassesToCombo = 3;
    public int extraComboPoints = 0;
    public int perfectTimingBonus = 5;


    [Header("Difficulty Presets")]
    public DifficultyPreset[] presets;

    public void ApplyPreset(Difficulty difficulty)
    {
        var preset = presets[(int)difficulty];
        mistakePointLoss = preset.mistakePointLoss;
        teammateCount = preset.teammateCount;
        targetSize = preset.targetSize;
        targetActiveTime = preset.targetActiveTime;
        comboPatternsEnabled = preset.comboPatternsEnabled;
        numberOfPassesToCombo = preset.numberOfPassesToCombo;
        extraComboPoints = preset.extraComboPoints;
        perfectTimingBonus = preset.perfectTimingBonus;
    }

    [System.Serializable]
    public class DifficultyPreset
    {
        public string name;
        public int mistakePointLoss;
        public int teammateCount;
        public float targetSize;
        public float targetActiveTime;
        public bool comboPatternsEnabled;
        public int numberOfPassesToCombo;
        public int extraComboPoints;
        public int perfectTimingBonus;
    }
}

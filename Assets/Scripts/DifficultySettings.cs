using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "Scriptable Objects/Difficulty Settings")]
public class DifficultySettings : ScriptableObject
{
    public enum Difficulty { Easy, Medium, Hard }

    [Header("Game Parameters")]
    public float mistakePointLoss = 1f;
    public int teammateCount = 4;
    public float targetSize = 1f;
    public float targetActiveTime = 1f;
    public bool comboPatternsEnabled = false;
    public int numberOfPassesToCombo = 3;
    public int extraComboPoints = 0;


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
        numberOfPassesToCombo = preset.extraComboPoints;
    }

    [System.Serializable]
    public class DifficultyPreset
    {
        public string name;
        public float mistakePointLoss;
        public int teammateCount;
        public float targetSize;
        public float targetActiveTime;
        public bool comboPatternsEnabled;
        public int numberOfPassesToCombo;
        public int extraComboPoints;
    }
}

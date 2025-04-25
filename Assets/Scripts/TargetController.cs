using System;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public static event Action OnTargetHit;

    private void Awake()
    {
        float targetSize = GameManager.Instance.difficultySettings.targetSize;
        gameObject.transform.localScale = new Vector3(targetSize, targetSize, targetSize);
    }

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
        OnTargetHit?.Invoke();
    }
}

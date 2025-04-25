using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetSprite;
    [SerializeField] private float scaleSize = 1.2f;
    [SerializeField] private float originalSize = 1f; // Original size of the target
    [SerializeField] private float pulseDuration = 0.5f;

    public static event Action OnTargetHit;

    private void Awake()
    {
        float targetSize = GameManager.Instance.difficultySettings.targetSize;
        gameObject.transform.localScale = new Vector3(targetSize, targetSize, targetSize);
        originalSize = targetSize;
        scaleSize = originalSize * 1.2f; // Set the scale size to 20% larger than the original size
        targetSprite = gameObject.GetComponent<SpriteRenderer>();
        pulseDuration = GameManager.Instance.difficultySettings.targetActiveTime / 2;
    }

    private void OnEnable()
    {
        // Create an infinite pulsating sequence
        Sequence pulseSequence = DOTween.Sequence();
        pulseSequence.Append(targetSprite.transform.DOScale(scaleSize, pulseDuration).SetEase(Ease.InOutSine));
        pulseSequence.Append(targetSprite.transform.DOScale(originalSize, pulseDuration).SetEase(Ease.InOutSine));
        pulseSequence.SetLoops(1, LoopType.Restart);
    }

    private void OnDisable()
    {
        // Kill the pulsating sequence when the target is disabled
        DOTween.Kill(targetSprite.transform);
        
    }


    private void Start()
    {
        
        
    }

    public void Hit()
    {
        gameObject.SetActive(false);
        OnTargetHit?.Invoke();
    }
}

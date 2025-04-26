using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    [SerializeField] private TMP_Text perfectText;
    [SerializeField] private float scaleSize = 1.2f;
    [SerializeField] private float originalSize = 1f; // Original size of the target
    [SerializeField] private float pulseDuration = 0.5f;
    [SerializeField] private AudioClip[] targetSounds;
    private AudioSource audioSource;
    private float reactionTime;
    private float targetActiveTime;
    private SpriteRenderer targetSprite;

    public static event Action OnTargetHit;
    public static event Action OnPerfectPass;

    private void Awake()
    {
        float targetSize = GameManager.Instance.difficultySettings.targetSize;
        gameObject.transform.localScale = new Vector3(targetSize, targetSize, targetSize);
        originalSize = targetSize;
        scaleSize = originalSize * 1.2f; // Set the scale size to 20% larger than the original size
        targetSprite = gameObject.GetComponent<SpriteRenderer>();
        pulseDuration = GameManager.Instance.difficultySettings.targetActiveTime / 2;
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void OnEnable()
    {
        StartTimer();
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


    void StartTimer()
    {
        targetActiveTime = Time.time;
       
    }

    void CalculateReactionTime()
    {
        reactionTime = (Time.time - targetActiveTime) * 1000;
        Debug.Log("Rection time:" + reactionTime);
        
        // Perfect timing bonus
        if (reactionTime < 500)
        {
            float originalTextScale = perfectText.transform.localScale.x;

            perfectText.gameObject.SetActive(true);
            audioSource.clip = targetSounds[0];
            audioSource.Play();

            perfectText.transform.DOScale(0.5f, pulseDuration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                perfectText.gameObject.SetActive(false);
                perfectText.transform.localScale = new Vector3(originalTextScale, originalTextScale, originalTextScale);
            });
            OnPerfectPass?.Invoke();
        }

        gameObject.SetActive(false);
    }

    public void Hit()
    {
        CalculateReactionTime();
        OnTargetHit?.Invoke();
    }
}

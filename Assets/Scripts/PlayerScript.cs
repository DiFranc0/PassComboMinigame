using System;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] Transform kickPoint;
    [SerializeField] private string ballTag;
    [SerializeField] private AudioClip[] playerSounds;

    private AudioSource audioSource;
    private Animator playerAnimator;

    public static event Action OnMistakeMade;
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        Vector2 pointerPos = Vector2.zero;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.IsPressed())
        {
            pointerPos = mainCamera.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
        }
        else if (Mouse.current != null)
        {
            pointerPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        Vector3 direction = pointerPos - (Vector2)transform.position;
        direction.z = 0; // Ensure the z component is zero for 2D rotation

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnAttack()
    {
        Vector2 clickPos = Vector2.zero;

        if (Touchscreen.current != null)
        {
            clickPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null)
        {
            clickPos = Mouse.current.position.ReadValue();
        }

        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(clickPos), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Target"))
            {
                hit.collider.GetComponent<TargetController>().Hit();
                ObjectPoolManager.Instance.SpawnFromPool(ballTag, kickPoint.position, kickPoint.rotation);
                playerAnimator.SetTrigger("pKick");
                audioSource.clip = playerSounds[0];
                audioSource.Play();
                return;
            }
            else
            {
                Debug.Log("Not a target");
                playerAnimator.SetTrigger("pSlip");
                audioSource.clip = playerSounds[1];
                audioSource.Play();
                OnMistakeMade?.Invoke();
            }
        }
        else
        {
            Debug.Log("Mistake Made");
            playerAnimator.SetTrigger("pSlip");
            audioSource.clip = playerSounds[1];
            audioSource.Play();
            OnMistakeMade?.Invoke();
        }
        //ObjectPoolManager.Instance.SpawnFromPool(ballTag, kickPoint.position, kickPoint.rotation);
        //playerAnimator.SetTrigger("pKick");
    }

    
}

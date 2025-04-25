using UnityEngine;

public class TimerManager : MonoBehaviour
{

    [SerializeField] private float timeRemaining = 30f;
    [SerializeField] private bool timerIsRunning = false;

    private UIManager uiManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            EnableTimer();
        }
    }

    void EnableTimer()
    {
        if (timeRemaining> 0)
        {
            timeRemaining -= Time.deltaTime;
            uiManager.DisplayTime(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            uiManager.ShowGameOverScreen();
            timerIsRunning = false;
           
        }
        
    }

    public void ResetTimer()
    {
        timeRemaining = 30f;
        timerIsRunning = true;
    }


}

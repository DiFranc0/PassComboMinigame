using UnityEngine;

public class TeammatesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] teammates;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateTeammates();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActivateTeammates()
    {
        int teammateCount = GameManager.Instance.difficultySettings.teammateCount;

        for (int i = 0; i < teammates.Length; i++)
        {
            if (i < teammateCount)
            {
                teammates[i].SetActive(true);
            }
            else
            {
                teammates[i].SetActive(false);
            }
        }
    }
}

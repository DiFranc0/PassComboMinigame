using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeammatesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] teammates;
    private float targetActiveTime;
    private List<GameObject> activeTeammates = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetActiveTime = GameManager.Instance.difficultySettings.targetActiveTime;
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
                activeTeammates.Add(teammates[i]);
                teammates[i].SetActive(true);
            }
            else
            {
                teammates[i].SetActive(false);
            }
        }

        StartCoroutine(ActivateTargetCoroutine());
    }

    private IEnumerator ActivateTargetCoroutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(targetActiveTime);
        WaitForSeconds delayToActitave = new WaitForSeconds(0.5f);
        GameObject target = activeTeammates[Random.Range(0, activeTeammates.Count)].transform.GetChild(0).gameObject;

        yield return delayToActitave;
        target.SetActive(true);
        yield return waitTime;
        target.SetActive(false);



        StartCoroutine(ActivateTargetCoroutine());

    }

    private IEnumerator ActivateComboPattern()
    {
        WaitForSeconds waitTime = new WaitForSeconds(targetActiveTime);
        WaitForSeconds delayToActitave = new WaitForSeconds(0.5f);
        List<GameObject> randomTeammates = GetRandomTeammates(activeTeammates, 4);

        yield return delayToActitave;
        foreach (GameObject teammate in randomTeammates)
        {
            teammate.transform.GetChild(0).gameObject.SetActive(true);
        }
        yield return waitTime;
        foreach (GameObject teammate in randomTeammates)
        {
            teammate.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    public List<GameObject> GetRandomTeammates(List<GameObject> teammatesList, int count)
    {
        List<GameObject> result = new List<GameObject>();
        List<GameObject> tempList = new List<GameObject>(teammatesList);

        count = Mathf.Min(count, tempList.Count); // Ensure count does not exceed available targets

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            result.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
        }

        return result;
    }
}

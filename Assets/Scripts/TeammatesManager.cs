using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeammatesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] teammates;
    private float delayToActiv = 0.5f; // Delay before activating the target
    private float targetActiveTime;
    private float comboPatternActivationChance = 0.3f; // 30% chance to activate combo pattern
    private bool comboPatternActive = false; // Flag to check if combo pattern is active
    private List<GameObject> activeTeammates = new List<GameObject>();


    private void OnEnable()
    {
        TargetController.OnTargetHit += CheckCoroutineToStart;
    }

    private void OnDisable()
    {
        TargetController.OnTargetHit -= CheckCoroutineToStart;
    }
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

        CheckCoroutineToStart();

    }

    private void CheckCoroutineToStart()
    {
        if (comboPatternActive)
        {
            return; // If combo pattern is active, do not start a new coroutine
        }

        StopAllCoroutines(); // Stop any existing coroutines

        float randomValue = Random.value;

        if (GameManager.Instance.difficultySettings.comboPatternsEnabled &&
           randomValue <= comboPatternActivationChance)
        {
            StartCoroutine(ActivateComboPattern());
        }
        else
        {
            StartCoroutine(ActivateTargetCoroutine());
        }
    }

    private IEnumerator ActivateTargetCoroutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(targetActiveTime);
        WaitForSeconds delayToActivate = new WaitForSeconds(delayToActiv);
        GameObject target = activeTeammates[Random.Range(0, activeTeammates.Count)].transform.GetChild(0).gameObject;

        yield return delayToActivate;
        target.SetActive(true);
        yield return waitTime;
        target.SetActive(false);

        CheckCoroutineToStart();

    }

    private IEnumerator ActivateComboPattern()
    {
        comboPatternActive = true;

        WaitForSeconds delayToActivate = new WaitForSeconds(delayToActiv);
        List<GameObject> randomTeammates = GetRandomTeammates(activeTeammates, 4);
        WaitForSeconds waitTime = new WaitForSeconds(targetActiveTime * randomTeammates.Count);

        // Activate targets for the combo pattern after delay
        yield return delayToActivate;
        for (int i = 0; i < randomTeammates.Count; i++)
        {
            randomTeammates[i].transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(targetActiveTime);
        }
        
        foreach (GameObject teammate in randomTeammates)
        {
            teammate.transform.GetChild(0).gameObject.SetActive(false);
        }

        comboPatternActive = false;
        CheckCoroutineToStart();

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

using System.Collections;
using UnityEngine;

public class TargetsManager : MonoBehaviour
{
   /* [SerializeField] private GameObject[] teammates;
    private float targetActiveTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetActiveTime = GameManager.Instance.difficultySettings.targetActiveTime;
        StartCoroutine(ActivateTargetCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator ActivateTargetCoroutine()
    {
        GameObject target = teammates[Random.Range(0, teammates.Length)].transform.GetChild(0).gameObject;
        target.SetActive(true);
        yield return new WaitForSeconds(targetActiveTime);
        target.SetActive(false);

        StartCoroutine(ActivateTargetCoroutine());

    }*/

}

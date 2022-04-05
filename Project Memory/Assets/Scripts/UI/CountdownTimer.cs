using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [Space]
    [SerializeField] private float timeLeft = 60;
    [SerializeField] private TextMeshProUGUI countdownText;

    [Space]
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject enemy;

    private void Update()
    {
        countdownText.text = "" + Mathf.CeilToInt(timeLeft);

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            //Death Approaches.
            text.SetActive(false);
            enemy.SetActive(true);
            
        }
    }


}

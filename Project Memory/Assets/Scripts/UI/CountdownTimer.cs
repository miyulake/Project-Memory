using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float timeLeft = 60;

    [SerializeField] private TextMeshProUGUI countdownText;

    void Update()
    {
        countdownText.text = "" + Mathf.CeilToInt(timeLeft);

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            //Death Approaches.
        }
    }


}

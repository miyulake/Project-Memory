using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashEvent : MonoBehaviour
{
    [SerializeField] private GameObject crashScreen;
    [SerializeField] private GameObject beforeCrash;
    [SerializeField] private GameObject afterCrash;

    private void OnTriggerEnter()
    {
        crashScreen.SetActive(true);
        beforeCrash.SetActive(false);
        afterCrash.SetActive(true);

        Invoke("LoadNextScene", 10);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

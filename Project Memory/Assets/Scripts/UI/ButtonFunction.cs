using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    [Header("Starting Scene")]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float waitTime = 10;

    public void LoadScene()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadSceneAsync(sceneToLoad);

        yield return null;
    }
}

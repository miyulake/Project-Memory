using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class CollisionNextScene : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [SerializeField] private float waitTime = 1;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        Debug.Log("Scene to load: " + sceneName);
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(sceneName);
    }
}

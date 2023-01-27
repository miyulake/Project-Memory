using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void Update()
    {
        Invoke("ExitGame", 10);
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Space]
    Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    [Space]
    [SerializeField] private string nextScene;
    [SerializeField] private string skippedScene;
    [SerializeField] bool skipIntro;

    [Header("Menu Audio")]
    [SerializeField] private AudioSource interactAudio;
    [SerializeField] private AudioClip menuSound;



    private void Awake()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void PlayGame()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        yield return new WaitForSeconds(10);

        if (!skipIntro)
        {
            SceneManager.LoadSceneAsync(nextScene);
        }
        else
        {
            SceneManager.LoadSceneAsync(skippedScene);
        }

        yield return null;
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Exit Game");
    }

    public void PlaySound()
    {
        interactAudio.PlayOneShot(menuSound);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetIntroState(bool introToggle) => skipIntro = introToggle;
}
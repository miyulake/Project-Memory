using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip damageSound;

    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject damageScreen;
    [SerializeField] private GameObject gameOverImage;
    [SerializeField] private GameObject gameOverText;

    [SerializeField] private GameObject playerEquipment;
    [SerializeField] private GameObject enemy;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            audioManager.PlayOneShot(damageSound);

            blackScreen.SetActive(true);
            damageScreen.SetActive(true);
            gameOverImage.SetActive(true);
            gameOverText.SetActive(true);

            playerEquipment.SetActive(false);
            enemy.SetActive(false);

            Invoke("ReloadScene", 10);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

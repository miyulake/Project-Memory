using UnityEngine;
using System.Collections;

public class FootstepSounds : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource audioManager;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip brickFootsteps;
    [SerializeField] private AudioClip grassFootsteps;

    [Header("Audio Values")]
    [SerializeField] private float footstepDelay;
    [Space]
    [SerializeField] private float audioVolume;
    [Space]
    [SerializeField] private float minimumPitch = 0.9f;
    [SerializeField] private float maximumPitch = 1f;

    [Header("Debug")]
    [SerializeField] private float nextFootstep;

    [SerializeField] private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    /// <summary>
    // If WASD is pressed it will check what tag the floor has.
    // It will then play the audio clip attached to that tag.
    /// </summary>
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {

            if (playerMovement.Velocity.y >= 0)
            {
                nextFootstep -= Time.deltaTime;
            }

            if (nextFootstep <= 0)
            {
                RaycastHit hit = new RaycastHit();
                string floortag;
                if (Physics.Raycast(transform.position, Vector3.down, out hit))
                {
                    floortag = hit.collider.gameObject.tag;

                    audioManager.pitch = Random.Range(minimumPitch, maximumPitch);

                    switch (floortag)
                    {
                        default:
                            audioManager.PlayOneShot(brickFootsteps, audioVolume);
                            break;

                        case "Grass":
                            audioManager.PlayOneShot(grassFootsteps, audioVolume);
                            break;
                    }
                }
                   nextFootstep += footstepDelay;
            }
        }
    }
}
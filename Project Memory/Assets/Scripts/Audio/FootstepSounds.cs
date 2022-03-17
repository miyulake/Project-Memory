using UnityEngine;
using System.Collections;

public class FootstepSounds : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource footstepAudio;

    [Header("Audio Clips")]
    public AudioClip brickFootsteps;
    public AudioClip grassFootsteps;

    [Header("Audio Values")]
    public float footstepDelay;
    [Space]
    public float audioVolume;
    [Space]
    public float minimumPitch = 0.9f;
    public float maximumPitch = 1f;

    [Header("Debug")]
    public float nextFootstep;

    /// <summary>
    // If WASD is pressed it will check what tag the floor has.
    // It will then play the audio clip attached to that tag.
    /// </summary>
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            nextFootstep -= Time.deltaTime;

            if (nextFootstep <= 0)
            {
                RaycastHit hit = new RaycastHit();
                string floortag;
                if (Physics.Raycast(transform.position, Vector3.down, out hit))
                {
                    floortag = hit.collider.gameObject.tag;

                    footstepAudio.pitch = Random.Range(minimumPitch, maximumPitch);

                    switch (floortag)
                    {
                        default:
                            footstepAudio.PlayOneShot(brickFootsteps, audioVolume);
                            break;

                        case "Grass":
                            footstepAudio.PlayOneShot(grassFootsteps, audioVolume);
                            break;
                    }
                }
                   nextFootstep += footstepDelay;
            }
        }
    }
}
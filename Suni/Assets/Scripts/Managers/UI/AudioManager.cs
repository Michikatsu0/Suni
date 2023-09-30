using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip soundToPlay;
    private AudioSource audioSource;

    public bool isBackgroundMusic = false;
    public bool playOnAwake = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (isBackgroundMusic && playOnAwake)
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        if (soundToPlay != null && audioSource != null)
        {
            audioSource.clip = soundToPlay;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("The audio clip or AudioSource is not set up.");
        }
    }
}


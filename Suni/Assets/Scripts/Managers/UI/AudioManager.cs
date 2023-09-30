using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] soundToPlay;
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
            PlaySound(0);
        }
    }

    public void PlaySound(int audioClip)
    {
        if (soundToPlay != null && audioSource != null)
        {
            if (audioClip == 0)
            {
                audioSource.clip = soundToPlay[audioClip];
                audioSource.Play();
                audioSource.loop = true;
            }
            else            
                audioSource.PlayOneShot(soundToPlay[audioClip]);                         
        }
        else
        {
            Debug.LogWarning("The audio clip or AudioSource is not set up.");
        }
    }
}


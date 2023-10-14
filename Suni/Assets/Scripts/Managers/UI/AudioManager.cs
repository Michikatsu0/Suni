using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip[] soundToPlay;
    public AudioSource audioSource;

    public bool isBackgroundMusic = false;
    public bool playOnAwake = false;
    public bool initScene = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        if (isBackgroundMusic || playOnAwake)
        {
            if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.LOGIN)
                PlaySound(0);
            if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.HOME)
                PlaySound(1);
        }
    }

    public void SetAudio()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.LOGIN)
            PlaySound(0);
        if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.HOME)
            PlaySound(1);
    }

    public void PlaySound(int audioClip)
    {
        
        if (soundToPlay != null && audioSource != null)
        {
            if (audioClip == 0 || audioClip == 1 || audioClip == 2)
            {
                audioSource.volume = 1f;
                audioSource.clip = soundToPlay[audioClip];
                audioSource.Play();
                audioSource.loop = true;
            }
            else
                audioSource.PlayOneShot(soundToPlay[audioClip]);
        }
    }
}


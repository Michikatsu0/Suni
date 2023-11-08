using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip[] soundToPlay;
    public AudioClip[] backgroundPlay;
    public AudioClip[] voicePlay;
    public AudioSource audioSource;
    public AudioSource[] audioSources;

    public float smooothVolumen = 1f, notificationVolumen = 1f,  voiceVolumen = 1f, musicVolumen = 1f, backgroundVolumen = 1f, uiVolumen = 1f;
    public bool isBackgroundMusic = false;
    public bool playOnAwake = false;
    public int notification, voice, music, background, ui;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GetSettings();
        if (isBackgroundMusic && playOnAwake)
        {
            if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.LOGIN)
            {
                PlaySoundBackground(0);
                PlaySound(0);
            }
            if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.HOME)
            {
                PlaySoundBackground(1);
                PlaySound(1);
            }
        }
    }

    public void GetSettings()
    {
        notification = PlayerPrefs.GetInt("SetNotification");
        voice = PlayerPrefs.GetInt("MuteVoice");
        music = PlayerPrefs.GetInt("MuteMusic");
        background = PlayerPrefs.GetInt("MuteNoise");
        ui = PlayerPrefs.GetInt("MuteUI");
        SetSettings();
    }

    public void SetSettings()
    {
        if (music != 0)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
            audioSource.Stop();
        

        if (notification != 0)
        {
            if (!audioSources[0].isPlaying)
                audioSources[0].Play();
        }
        else
            audioSources[0].Stop();
        

        if (voice != 0)
        {
            if (!audioSources[1].isPlaying)
                audioSources[1].Play();
        }
        else
            audioSources[1].Stop();
        

        if (background != 0)
        {
            if (!audioSources[2].isPlaying)
                audioSources[2].Play();
        }
        else
            audioSources[2].Stop();
        

        if (ui != 0)
        {
            if (!audioSources[3].isPlaying)
                audioSources[3].Play();
        }
        else
            audioSources[3].Stop();
        
    }

    private void Update()
    {
       audioSources[0].volume = Mathf.Lerp(audioSources[0].volume, notificationVolumen, smooothVolumen * Time.deltaTime);
       audioSources[1].volume = Mathf.Lerp(audioSources[1].volume, voiceVolumen, smooothVolumen * Time.deltaTime);
       audioSource.volume = Mathf.Lerp(audioSource.volume, musicVolumen, smooothVolumen * Time.deltaTime);
       audioSources[2].volume = Mathf.Lerp(audioSources[2].volume, backgroundVolumen, smooothVolumen * Time.deltaTime);
       audioSources[3].volume = Mathf.Lerp(audioSources[3].volume, uiVolumen, smooothVolumen * Time.deltaTime);
    }

    public void SetAudio()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.LOGIN)
        {
            PlaySound(0);
            PlaySoundBackground(0);
        }

        if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.HOME)
        {
            PlaySound(1);
            PlaySoundBackground(1);
        }
    }

    public void PlaySound(int audioClip)
    {

        if (soundToPlay != null && audioSources != null)
        {
            if (audioClip >= 0 && audioClip <= 2)
            {
                audioSource.volume = 1f;
                audioSource.clip = soundToPlay[audioClip];

                if (music != 0)
                    audioSource.Play();
                
                audioSource.loop = true;
            }
            else
            {
                if (audioClip >= 3) // Cambia esto según tus necesidades
                {
                    if (ui != 0)
                        audioSources[3].PlayOneShot(soundToPlay[audioClip]);
                }
            }
        }
    }

    public void PlaySoundBackground(int audioClip)
    {

        if (backgroundPlay != null && audioSources != null)
        {
            audioSources[2].volume = 1f;
            audioSources[2].clip = backgroundPlay[audioClip];
            
            if (background != 0)
                audioSources[2].Play();
            
            audioSources[2].loop = true;
        }
    }

    public void PlaySoundVoice(int audioClip)
    {

        if (voicePlay != null && audioSources != null)
        {
            audioSources[1].volume = 1f;
            audioSources[1].clip = backgroundPlay[audioClip];
            
            if (voice != 0)    
                audioSources[1].Play();
            
            audioSources[1].loop = true;
        }
    }
}


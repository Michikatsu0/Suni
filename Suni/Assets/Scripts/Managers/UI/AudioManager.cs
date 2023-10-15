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
    public AudioSource audioSource;
    public AudioSource[] audioSources;

    public float smooothVolumen = 1f, musicVolumen = 1f, backgroundVolumen = 1f;
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
        audioSource = GetComponent<AudioSource>();
        if (isBackgroundMusic && playOnAwake && music != 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.LOGIN)
                PlaySound(0);
            if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.HOME)
                PlaySound(1);
        }
    }

    public void GetSettings()
    {
        notification = PlayerPrefs.GetInt("SetNotification");
        voice = PlayerPrefs.GetInt("MuteVoice");
        music = PlayerPrefs.GetInt("MuteMusic");
        background = PlayerPrefs.GetInt("MuteBackground");
        ui = PlayerPrefs.GetInt("MuteUI");
        SetSettings();
    }

    public void SetSettings()
    {
        if (notification != 0)
        {
            if (audioSources[0].isPlaying) return;
            audioSources[0].Play();
        }
        else
            audioSources[0].Stop();

        if (voice != 0)
        {
            if (audioSources[1].isPlaying) return;
            audioSources[1].Play();
        }
        else
            audioSources[1].Stop();

        if (music != 0)
        {
            if (audioSource.isPlaying) return;
            audioSource.Play();
        }
        else
            audioSource.Stop();

        if (background != 0)
        {
            if (audioSources[2].isPlaying) return;
            audioSources[2].Play();
        }
        else
            audioSources[2].Stop();

        if (ui != 0)
        {
            if (audioSources[3].isPlaying) return;
            audioSources[3].Play();
        }
        else
            audioSources[3].Stop();
    }

    private void Update()
    {
       audioSource.volume = Mathf.Lerp(audioSource.volume, musicVolumen, smooothVolumen * Time.deltaTime);
       audioSources[3].volume = Mathf.Lerp(audioSources[3].volume, backgroundVolumen, smooothVolumen * Time.deltaTime);
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

        if (soundToPlay != null && audioSources != null)
        {
            if (audioClip == 0 || audioClip == 1 || audioClip == 2)
            {
                if (music != 0)
                {
                    audioSource.volume = 1f;
                    audioSource.clip = soundToPlay[audioClip];
                    audioSource.Play();
                    audioSource.loop = true;
                }
            }
            else
            {
                if (ui != 0)
                    audioSources[3].PlayOneShot(soundToPlay[audioClip]);
            }
        }
    }

    public void PlaySound(int audioClip, int sourcesIndex)
    {

        if (soundToPlay != null && audioSources != null)
        {
            if (audioClip == 0 || audioClip == 1 || audioClip == 2)
            {
                audioSources[sourcesIndex].volume = 1f;
                audioSources[sourcesIndex].clip = soundToPlay[audioClip];
                audioSources[sourcesIndex].Play();
                audioSources[sourcesIndex].loop = true;
            }
            else
                audioSources[3].PlayOneShot(soundToPlay[audioClip], 1f);
        }
    }
}


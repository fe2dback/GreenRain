using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;   
    public AudioSource musicsource;
    //public AudioSource musicSource2;
    private float soundVolume;
    public AudioSource audioSrc;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            DontDestroyOnLoad(audioSrc);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void SFXPlayBool(string sfxName, AudioClip clip, bool check)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = false;
        if (check == true)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
            Destroy(go);
        }
        
        

        //Destroy(go, clip.length);
    }
    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume;
        soundVolume = volume;
        DontDestroyOnLoad(musicsource);
    }

    public void MusicStop(bool stop)
    {
        if(stop == true)
        {
            musicsource.Stop();
            BattleMusic();

        }
    }

    public void BattleMusic()
    {
        audioSrc.volume = soundVolume;
        audioSrc.Play();
        //DontDestroyOnLoad(audioSrc);
    }

    /*
    public void SetMusicVolume2()
    {
        musicSource2.Play();
        musicsource.Stop();
        musicSource2.volume = soundVolume;
        DontDestroyOnLoad(musicSource2);
    }
    */
}

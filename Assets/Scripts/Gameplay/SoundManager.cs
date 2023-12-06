using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    public static SoundManager instance;
    
    [SerializeField] private string shootSoundName = "Shoot";
    [SerializeField] private string winSoundName = "Win";
    [SerializeField] private string loseSoundName = "Lose";
    [SerializeField] private string menuSoundName = "UI";
    [SerializeField] private string reloadSoundName = "Reload";
    [SerializeField] private string damageTakenSoundName = "DamageTaken";
    [SerializeField] private string switchSoundName = "Switch";

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound=> sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        if (s.name == shootSoundName)
        {
            s.source.pitch = Random.Range(0.90f, 1.50f);
            s.source.volume = Random.Range(0.1f, 0.2f);
        }
        
        s.source.Play();
    }

    public void PlayShoot()
    {
        Play(shootSoundName);
    }

    public void PlayWin()
    {
       Play(winSoundName);
    }

    public void PlayLose()
    { 
       Play(loseSoundName);
    }

    public void PlayMenu()
    {
        Play(menuSoundName);
    }

    public void PlayReload()
    {
        Play(reloadSoundName);
    }

    public void PlayDamageTaken()
    {
        Play(damageTakenSoundName);
    }

    public void PlaySwitch()
    {
        Play(switchSoundName);
    }
}

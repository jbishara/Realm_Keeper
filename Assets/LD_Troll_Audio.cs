using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class LD_Troll_Audio : MonoBehaviour
{
    // a array that stores all our audios
    public LD_Sound[] sounds;

    public AudioSource playerSource;

    // when script becomes created do this
    void Awake()
    {
        // foreach sound that we have in the array
        foreach (LD_Sound s in sounds)
        {
            // grabing values from LD_Sounds script where we then refences in the inspector of AudioManager in the scene
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    // finds the set sounds in the array based on the string name and plays it
    public void Play(string name)
    {
        LD_Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not found!");
            return;
        }
        s.source.Play();
    }


    void TakeDamages()
    {
        int min = 0;
        int max = 3;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("TakeDamages v1"); ;
                break;
            case 1:
                Play("TakeDamages v2"); ;
                break;
        }
    }

    void Die()
    {
        int min = 0;
        int max = 4;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("TakeDamages v1"); ;
                break;
            case 1:
                Play("TakeDamages v2"); ;
                break;
            case 2:
                Play("TakeDamages v3"); ;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class LD_Zylar_Audio : MonoBehaviour
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
    // Basic movement SFX
    void ZylarWalk()
    {
        Play("Zylar_walk");
    }
    void ZylarRun()
    {
        Play("Zylar_Run");
    }
    void ZylarJump()
    {
        int min = 0;
        int max = 4;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("Zylar_Jumpv1"); ;
                break;
            case 1:
                Play("Zylar_Jumpv2"); ;
                break;
            case 2:
                Play("Zylar_Jumpv3"); ;
                break;
        }

    }

    // Intraction SFX
    void ZylarTakeDamages()
    {
        int min = 0;
        int max = 5;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("Zylar_TakeDamagesv1"); ;
                break;
            case 1:
                Play("Zylar_TakeDamagesv2"); ;
                break;
            case 2:
                Play("Zylar_TakeDamagesv3"); ;
                break;
            case 3:
                Play("Zylar_TakeDamagesv4"); ;
                break;
        }

    }
    void ZylarDie()
    {
        int min = 0;
        int max = 5;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("Zylar_Die v1"); ;
                break;
            case 1:
                Play("Zylar_Die v2"); ;
                break;
            case 2:
                Play("Zylar_Die v3"); ;
                break;
            case 3:
                Play("Zylar_Die v4"); ;
                break;
        }

    }

    // Ablity SFX
    void ZylarBasicAttack()
    {
        int min = 0;
        int max = 4;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("Zylar_BasicAttack v1"); ;
                break;
            case 1:
                Play("Zylar_BasicAttack v2"); ;
                break;
            case 2:
                Play("Zylar_BasicAttack v3"); ;
                break;
        }

    }
    void ZylarBlink()
    {
        Play("Zylar_Blink");
    }
    void ZylarSwing()
    {
        Play("Zylar_Swing");
    }
    void ZylarColdSteel()
    {
        Play("Zylar_ColdSteel");
    }
}

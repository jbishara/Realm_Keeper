using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class LD_Freya_Audio : MonoBehaviour
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

    // freya SFX functions

    void freyaRun()
    {
        Play("Frey_Run");
    }
    void freyaJump()
    {
        int min = 0;
        int max = 2;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("FRE_EFFORT_Jump_Ah"); ;
                break;
            case 1:
                Play("FRE_EFFORT_Jump_Eh"); ;
                break;
        }
    }
    void freyaMagicArrow()
    {
        Play("FRE_Attack_MagicArrow");
    }
    void freyaMagicMissle()
    {
        Play("FRE_Attack_MagicMissle");
    }
    void freyaTakeDamages()
    {
        int min = 0;
        int max = 6;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("FRE_INJ_Longv1"); ;
                break;
            case 1:
                Play("FRE_INJ_Longv2"); ;
                break;
            case 2:
                Play("FRE_INJ_Ah"); ;
                break;
            case 3:
                Play("FRE_INJ_Oh"); ;
                break;
            case 4:
                Play("FRE_INJ_Cough"); ;
                break;
        }
    }
    void freyaDie()
    {
        int min = 0;
        int max = 6;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("FRE_DIE_Long_Oh"); ;
                break;
            case 1:
                Play("FRE_DIE_Long_Ah"); ;
                break;
            case 2:
                Play("FRE_DIE_Short"); ;
                break;
            case 3:
                Play("FRE_DIE_Short_Cough"); ;
                break;
            case 4:
                Play("FRE_DIE_Short_Breathe");
                break;
        }
    }
}

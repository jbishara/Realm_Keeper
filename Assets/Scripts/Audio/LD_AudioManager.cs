using UnityEngine.Audio;
using System;
using UnityEngine;

public class LD_AudioManager : MonoBehaviour
{
    // a array that stores all our audios
    public LD_Sound[] sounds;

    // makes stores a refences to it self
    public static LD_AudioManager instance;

    // when script becomes created do this
    void Awake()
    {
        // if there is no refences to this gameobject set this object to it
        if (instance == null)
            instance = this;
        // else destroy the new version
        else
        {
            Destroy(gameObject);
            return;
        }

        // don't destroy this object on start or loading new scene
        DontDestroyOnLoad(gameObject);

        // foreach sound that we have in the array
        foreach(LD_Sound s in sounds)
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
   public void Play (string name)
    {
        LD_Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not found!");
            return;
        }
        s.source.Play();
        
    }

    // Bagoblin SFX functions
    void BagoblinTakeDamages()
    {
        int min = 0;
        int max = 3;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("Bagoblin_INJ_Grunt");
                break;
            case 1:
                Play("Bagoblin_INJ_Short");
                break;
            case 2:
                Play("Bagoblin_INJ_Loud");
                break;
        }
    }
    void BagoblinDie()
    {
        int min = 0;
        int max = 3;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("Bagoblin_Die_Long");
                break;
            case 1:
                Play("Bagoblin_Die_Growl");
                break;
        }
    }

    // Troll SFX functions
    void trollTakeDamages()
    {
        int min = 0;
        int max = 3;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("Troll_INJ_Grunt");
                break;
            case 1:
                Play("Troll_INJ_Ooh");
                break;
        }
    }
    void trollTakeDie()
    {
        int min = 0;
        int max = 4;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("Troll_DIE_Short");
                break;
            case 1:
                Play("Troll_DIE_Long");
                break;
            case 2:
                Play("Troll_DIE_Grunt");
                break;
        }
    }

    // Cyclops SFX functions
    void cyclopsTakeDamages()
    {
        // missing animation to put this function into
        int min = 0;
        int max = 4;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("Cyclops_INJ_Shh");
                break;
            case 1:
                Play("Cyclops_INJ_Sss");
                break;
            case 2:
                Play("Cyclops_INJ_Nagh");
                break;
        }
    }
    void cyclopsDie()
    {
        // missing animation to put this function into
        int min = 0;
        int max = 5;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("Cyclops_DIE_Nargh");
                break;
            case 1:
                Play("Cyclops_DIE_Nsh");
                break;
            case 2:
                Play("Cyclops_DIE_Sss");
                break;
            case 3:
                Play("Cyclops_DIE_Shh");
                break;
        }
    }

    // Elemental SFX functions
    void elementalTakeDamges()
    {
        // missing animation to put this function into
        int min = 0;
        int max = 4;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("ELE_INJ_Ooh");
                break;
            case 1:
                Play("ELE_INJ_Ohh");
                break;
            case 2:
                Play("ELE_DIE_Ahh");
                break;
        }
    }
    void elementalDie()
    {
        // missing animation to put this function into
        int min = 0;
        int max = 4;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("ELE_DIE_Ahh");
                break;
            case 1:
                Play("ELE_DIE_Ooh");
                break;
            case 2:
                Play("ELE_DIE_Ohh");
                break;
        }
    }

    // stone Golem SFX functions
    void stoneGolemTakeDamges()
    {
        int min = 0;
        int max = 3;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("StoneGolem_INJ_Ugh");
                break;
            case 1:
                Play("StoneGolem_INJ_Grunt");
                break;
        }
    }
    void stoneGolemDie()
    {
        int min = 0;
        int max = 4;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("StoneGolem_DIE_Ho");
                break;
            case 1:
                Play("StoneGolem_DIE_Ugh");
                break;
            case 2:
                Play("StoneGolem_DIE_Grunt");
                break;
        }
    }

    // crystalGolem SFX functions
    void crystaGolemTakeDamges()
    {
        int min = 0;
        int max = 4;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("CrystalGolem_INJ_Rah");
                break;
            case 1:
                Play("CrystalGolem_INJ_Oh");
                break;
            case 2:
                Play("CrystalGolem_INJ_Ree");
                break;
        }
    }
    void crystalGolemDie()
    {
        int min = 0;
        int max = 5;
        int whatAudioFile = UnityEngine.Random.Range(min, max);
        switch (whatAudioFile)
        {
            case 0:
                Play("CrystalGolem_DIE_Rugh");
                break;
            case 1:
                Play("CrystalGolem_DIE_Double");
                break;
            case 2:
                Play("CrystalGolem_DIE_Triple");
                break;
            case 3:
                Play("CrystalGolem_DIE_Ahh");
                break;
        }
    }
}
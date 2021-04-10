using UnityEngine.Audio;
using System;
using UnityEngine;

public class LD_AudioManager : MonoBehaviour
{
    // a array that stores all our audios
    public LD_Sound[] sounds;

    // makes stores a refences to it self
    public static LD_AudioManager instance;

    private int whichLevel;

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
}

using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class LD_Sound
{
    // name of audiofile that the manager will seek out 
    public string name;

    // refences to what audio clip it is
    public AudioClip clip;

    // sliders that shows up in the inspektor so we add choice values
    [Range(0f, 1f)]
    public float volume;
    [Range(1f, 3f)]
    public float pitch;

    // check if we want to loop the sound or not
    public bool loop;

    // refrences to the source
    public AudioSource source;
}

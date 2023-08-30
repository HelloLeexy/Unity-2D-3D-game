using UnityEngine;
using System;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{

    public  Sound[] sounds;
    
    private void Awake()
    {
        
        foreach( Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
        }
    }

    public void play (string name)
    {
        Sound s =  Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}

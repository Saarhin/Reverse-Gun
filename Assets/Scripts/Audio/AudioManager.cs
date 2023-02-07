using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound _sound in sounds)
        {
            _sound.source = gameObject.AddComponent<AudioSource>();

            _sound.source.clip = _sound.clip;
            _sound.source.volume = _sound.volume;
            _sound.source.pitch = _sound.pitch;
            _sound.source.loop = _sound.loop;
        }
    }


    private void Start()
    {
        AudioManager.instance.Play("Main");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        float randomPitch = UnityEngine.Random.Range(-0.05f, 0.05f);
        s.source.pitch += randomPitch;
        s.source.Play();

    }
}

using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public bool loop;
        [Range(0f, 1f)] public float volume = 0.5f;
        [HideInInspector] public AudioSource source;
    }

    [SerializeField] private List<Sound> sounds;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = sounds.Find(sound => sound.name == name);
        if (sound != null)
        {
            sound.source.Play();
        }
        else
        {
            Debug.LogWarning($"Sound '{name}' not found!");
        }
    }

    public void Stop(string name)
    {
        Sound sound = sounds.Find(sound => sound.name == name);
        if (sound != null)
        {
            sound.source.Stop();
        }
    }
}

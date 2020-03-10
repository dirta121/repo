using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public List<Sound> sounds;
    private AudioSource source;
    private void Awake()
    {
        source=gameObject.AddComponent<AudioSource>();
        DontDestroyOnLoad(this);
        foreach (var s in sounds)
        {
            s.source = source;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name) {
        if (source.isPlaying & source.clip.name == name)
            return;


        var sound=sounds.SingleOrDefault(s => s.name == name);
        if (sound == null)
            return;
        sound.source.Play();
    }
}

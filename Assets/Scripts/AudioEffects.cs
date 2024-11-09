using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffects : MonoBehaviour
{
    [Serializable]
    public class ClipWithTempo
    {
        [SerializeField] public AudioClip clip;
        [SerializeField] public int level = 1;


    }
    public ClipWithTempo[] soundtracks;
    public AudioSource soundtrackSource;


    void Start()
    {
        soundtrackSource.clip = soundtracks[0].clip;
        soundtrackSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (soundtrackSource.isPlaying) return;
        int level = GameHardness.level;
        for (int i = 0; i < soundtracks.Length; i++)
        {
            var track = soundtracks[i];
            if(track.level == level)
            {
                soundtrackSource.clip = track.clip;
                break;
            } 
            if(i + 1 == soundtracks.Length)
            {
                soundtrackSource.clip = track.clip;
                break;
            }

            if(track.level < level && soundtracks[i+1].level > level)
            {
                soundtrackSource.clip = track.clip;
            }
        }
        soundtrackSource.Play();

    }
}

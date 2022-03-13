using KillingGround.Utilities;
using UnityEngine;
using System;

/// <summary>
/// This class is a singleton which is used to Play SoundEffects in the game. This is a scalable class and we can add more SoundTypes if we want.
/// </summary>
namespace KillingGround.Audio
{
    public class SoundManager : SingletonGeneric<SoundManager>
    {
        public AudioSource audioEffects;
        public AudioSource audioEffects2;
        public AudioSource audioMusic;

        public Sounds[] AudioList;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySoundEffects(SoundType soundType)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioEffects.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }

        public void PlaySoundEffects2(SoundType soundType)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioEffects2.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }

        public void PlayBackgroundMusic(SoundType soundType)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioMusic.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }

        public AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds st = Array.Find(AudioList, item => item.soundType == soundType);
            if (st != null)
            {
                return st.audio;
            }
            return null;
        }

        public void StopSoundEffect()             // Sets the audio clip to null.
        {
            audioEffects.Stop();
            audioEffects.clip = null;
        }

        public void StopSoundEffect2()             // Sets the audio clip to null.
        {
            audioEffects2.Stop();
            audioEffects2.clip = null;
        }

    }
}
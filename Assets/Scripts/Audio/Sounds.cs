using UnityEngine;
using System;

namespace KillingGround.Audio
{
    /// <summary>
    /// This class is used to bind a soundType with an audio clip.
    /// </summary>
    [Serializable]
    public class Sounds
    {
        public SoundType soundType;
        public AudioClip audio;
    }
}
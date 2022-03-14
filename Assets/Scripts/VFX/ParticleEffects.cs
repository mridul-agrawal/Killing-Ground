using UnityEngine;
using KillingGround.Utilities;

namespace KillingGround.VFX
{
    public class ParticleEffects : SingletonGeneric<ParticleEffects>
    {
        // References:
        [SerializeField] private ParticleSystem deathSplash;

        internal void PlaydeathSplashAt(Vector3 position)
        {
            deathSplash.transform.position = position;
            deathSplash.Play();
        }

    }
}
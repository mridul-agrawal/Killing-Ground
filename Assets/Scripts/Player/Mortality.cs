using UnityEngine;
using KillingGround.Player;
using KillingGround.Services;
using KillingGround.Audio;
using System;

namespace KillingGround.Player
{
    /// <summary>
    /// This class is responsible for managing the health of the gameobject it is attached to (Player).
    /// </summary>
    public class Mortality : MonoBehaviour
    {
        // Variables: 
        [SerializeField] private int health = 100;

        // Events:
        public static event Action OnPlayerDeath;


        // reduces health for the given damage and Invokes death event if applicable.
        public void TakeDamage(int damage)
        {
            if (health - damage > 0)
            {
                health -= damage;
            }
            else
            {
                health = 0;
                InvokePlayerDeathEvent();
            }
            UIService.Instance.UpdateHealthUI(health);
        }

        // A Method Used to Invoke Death Event.
        public void InvokePlayerDeathEvent()           
        {
            OnPlayerDeath?.Invoke();
            UIService.Instance.ToggleDeathOverlay(true);
            SoundManager.Instance.PlaySoundEffects(SoundType.deathSound);
        }

        // Checks collision for damage.
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Damager"))
            {
                TakeDamage(5);
            }
        }

    }
}

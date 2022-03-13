using TMPro;
using UnityEngine;
using KillingGround.Utilities;

namespace KillingGround.Services
{
    public class UIService : SingletonGeneric<UIService>
    {
        public GameObject deathOverlay;         // UI
        public TextMeshProUGUI hpText;          // UI

        protected override void Awake()
        {
            base.Awake();
            ToggleDeathOverlay(false);      // UI
        }

        // Switch for toggling death overlay.
        internal void ToggleDeathOverlay(bool isActive)
        {
            deathOverlay.SetActive(isActive);
        }

        // Update health text to display.
        internal void UpdateHealthUI(int health)
        {
            hpText.text = health.ToString();    // UI
        }

    }
}
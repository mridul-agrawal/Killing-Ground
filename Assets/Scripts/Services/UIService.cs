using TMPro;
using UnityEngine;
using KillingGround.Utilities;

namespace KillingGround.Services
{
    public class UIService : SingletonGeneric<UIService>
    {
        public GameObject deathOverlay;         
        public TextMeshProUGUI hpText;          
        public TextMeshProUGUI ZombiesLeftText; 
        public TextMeshProUGUI GameStatus;      
        public GameObject winOverlay;           
        public TextMeshProUGUI forceText;

        protected override void Awake()
        {
            base.Awake();
            ToggleDeathOverlay(false);      
        }

        // Switch for toggling death overlay.
        internal void ToggleDeathOverlay(bool isActive)
        {
            deathOverlay.SetActive(isActive);
        }

        // Update health text to display.
        internal void UpdateHealthUI(int health)
        {
            hpText.text = health.ToString();    
        }

        // Update enemies left text to display.
        internal void UpdateEnemyUI(int noOfEnemies)
        {
            ZombiesLeftText.text = noOfEnemies.ToString();   
        }

        // Update Win Overlay to display.
        internal void UpdateWinUI()
        {
            GameStatus.text = "YOU WON!!!";
            ZombiesLeftText.text = "";
            winOverlay.SetActive(true);
        }

        // Update force text to display.
        internal void UpdateForceUI(float throwForce)
        {
            forceText.text = throwForce.ToString();
        }
    }
}
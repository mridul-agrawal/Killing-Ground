using UnityEngine;
using KillingGround.Utilities;
using KillingGround.Player;
using UnityEngine.SceneManagement;

namespace KillingGround.Services
{
    /// <summary>
    /// A service which is used to execute game logic.
    /// </summary>
    public class GameService : SingletonGeneric<GameService>
    {
        protected override void Awake()
        {
            base.Awake();
            LockCursor();
            Mortality.OnPlayerDeath += ConfineCursor;
        }

        // Locks Cursor and makes it invisible at the start of game.
        private static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private static void ConfineCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        // Used to Restart the GameScene.
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

using UnityEngine;
using KillingGround.Utilities;
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
            SetUpCursor();
        }

        // Locks Cursor and makes it invisible at the start of game.
        private static void SetUpCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Used to Restart the GameScene.
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

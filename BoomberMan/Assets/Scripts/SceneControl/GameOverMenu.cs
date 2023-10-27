using Enum;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneControl
{
    public class GameOverMenu : MonoBehaviour
    {
        public GameObject gameOverMenu;

        public void GameOverPause()
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene((int)GameScene.MainScene);
        }
    }
}

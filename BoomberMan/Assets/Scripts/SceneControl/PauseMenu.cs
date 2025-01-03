using Enum;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneControl
{
    public class PauseMenu : MonoBehaviour
    {
        private bool pauseGame;
        public GameObject pauseGameMenu;

        private void Start()
        {
            pauseGame = false;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            if (pauseGame)
                Resume();
            else
                Pause();
        }

        public void Resume()
        {
            pauseGameMenu.SetActive(false);
            Time.timeScale = 1f;
            pauseGame = false;
        }

        private void Pause()
        {
            pauseGameMenu.SetActive(true);
            Time.timeScale = 0f;
            pauseGame = true;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene((int)GameScene.MainScene);
        }
    }
}
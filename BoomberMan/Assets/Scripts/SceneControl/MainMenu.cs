using SceneControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene((int)GameScene.GameScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

using Observer.Event.Interface;
using SceneControl;
using UnityEngine;

public class GameOver : IEventListenerGameOver
{
    public void CallGameOver()
    {
        var menu = Object.FindObjectOfType<GameOverMenu>();
        menu.GameOverPause();
    }
}
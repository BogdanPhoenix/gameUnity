using Observer.Event.Interface;
using SceneControl;
using UnityEngine;

public class LevelVictory : IEventListenerVictory
{
    public void Victory()
    {
        var menu = Object.FindObjectOfType<VictoryMenu>();
        menu.VictoryPause();
    }
}
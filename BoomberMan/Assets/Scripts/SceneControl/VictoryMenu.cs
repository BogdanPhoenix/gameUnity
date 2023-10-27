using Enum;
using Map.Generate;
using Observer.Event.Interface;
using Observer.Manager;
using Observer.Manager.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneControl
{
    public class VictoryMenu : MonoBehaviour
    {
        private EventManager<IEventListenerGenerateMap> EventVictory;
        
        public GameObject victoryGameMenu;

        private void Start()
        {
            EventVictory = new EventManagerGenerateMap(TypeActive.GenerateMap);
            EventVictory.Subscribe(TypeActive.GenerateMap, GenerateMapBuilder.GetInstance());
        }

        public void VictoryPause()
        {
            victoryGameMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Continue()
        {
            ((INotifySimple)EventVictory).Notify(TypeActive.GenerateMap);
            Resume();
        }
        
        private void Resume()
        {
            victoryGameMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene((int)GameScene.MainScene);
        }
    }
}

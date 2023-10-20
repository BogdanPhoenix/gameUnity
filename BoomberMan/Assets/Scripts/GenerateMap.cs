using System.Collections.Generic;
using Map;
using Map.Brick;
using Map.Enemy;
using Map.PowerUp;
using Map.Stone;
using UnityEngine;
using UnityEngine.Serialization;

public class GenerateMap : MonoBehaviour
{
    private static Vector2 _startPosition;
    private const int MaxLevel = 4;
    private static int Level = 0;
    
    private static GameObject[,] Field;
    private GenerateObject StoneBlocks;
    private GenerateObject BrickBlock;
    private GenerateObject PowerUpBlocks;
    private GenerateObject Enemy;

    [FormerlySerializedAs("MapSize")] public Vector2Int mapSize = new(17, 17);
    [FormerlySerializedAs("StonePrefab")] public GameObject stonePrefab;
    [FormerlySerializedAs("BrickPrefab")] public GameObject brickPrefab;
    [FormerlySerializedAs("EnemyPrefab")]public GameObject enemyPrefab;
    [FormerlySerializedAs("BomberMan")]public GameObject bomberMan;
    [FormerlySerializedAs("PowerUps")]public List<GameObject> powerUps;
    
    public void Start()
    {
        _startPosition = new Vector2(bomberMan.transform.position.x - 1, bomberMan.transform.position.y + 1);
        
        StoneBlocks = new GenerateStoneBlocks(stonePrefab);
        BrickBlock = new GenerateBrickBlock(bomberMan, brickPrefab);
        PowerUpBlocks = new GeneratePowerUpBlocks(powerUps, bomberMan);
        Enemy = new GenerateEnemy(enemyPrefab, bomberMan);
        
        NextLevel();
    }

    public void NextLevel()
    {
        if (Level >= MaxLevel)
        {
            Debug.Log("End game level " + Level);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
            return;
        }
        
        Debug.Log("New Level");
        
        FindObjectOfType<BomberMan>().transform.position = new Vector3(0, 0, bomberMan.transform.position.z);
        
        ++Level;
        Generate();
    }
    
    private void Generate()
    {
        Field = new GameObject[mapSize.x, mapSize.y];
        CreateObjects(StoneBlocks, BrickBlock, PowerUpBlocks, Enemy);
    }

    private static void CreateObjects(params GenerateObject[] create)
    {
        foreach (var element in create)
        {
            element.CreateObjects(Field);
        }
    }

    public static void DetonateBrick(Vector2 positionField)
    {
        var position = new Vector2Int(
            (int)(positionField.x - _startPosition.x), 
            (int)(positionField.y - _startPosition.y) * -1);
        
        Field[position.x, position.y] = null;
    }

    public static Vector2 GetStartPosition()
    {
        return _startPosition;
    }

    public static int GetLevel()
    {
        return Level;
    }
}

using System;
using System.Collections.Generic;
using BomberMan;
using Map;
using Map.Brick;
using Map.Enemy;
using Map.PowerUp;
using Map.Stone;
using UnityEngine;
using UnityEngine.Serialization;

public class GenerateMap : MonoBehaviour
{
    private static Vector2 _startPositionGenerateMap;
    private static Vector2 _startPositionBomberMan;
    private const int MaxLevel = 4;
    private static int Level = 0;
    
    private static TypeObject[,] Field;
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
        _startPositionGenerateMap = new Vector2(bomberMan.transform.position.x - 1, bomberMan.transform.position.y + 1);
        _startPositionBomberMan = new Vector2(bomberMan.transform.position.x, bomberMan.transform.position.y);
        
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
        
        FindObjectOfType<BomberManPlayer>().transform.position = new Vector3(_startPositionBomberMan.x, _startPositionBomberMan.y, bomberMan.transform.position.z);
        
        ++Level;
        Generate();
    }
    
    private void Generate()
    {
        Field = new TypeObject[mapSize.x, mapSize.y];
        FillArray2d(Field, TypeObject.None);
        
        CreateObjects(StoneBlocks, BrickBlock, PowerUpBlocks, Enemy);
    }

    private static void FillArray2d(TypeObject[,] field, TypeObject value)
    {
        var rows = field.GetLength(0);
        var cols = field.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                field[i, j] = value;
            }
        }
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
            (int)(positionField.x - _startPositionGenerateMap.x), 
            (int)(positionField.y - _startPositionGenerateMap.y) * -1);
        
        Field[position.x, position.y] = TypeObject.None;
    }

    public static Vector2 GetStartPosition()
    {
        return _startPositionGenerateMap;
    }

    public static int GetLevel()
    {
        return Level;
    }
}

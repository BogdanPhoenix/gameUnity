using GenerateField;
using GenerateField.Brick;
using UnityEngine;
using UnityEngine.Serialization;

public class GenerateMap : MonoBehaviour
{
    private static readonly Vector2 StartPosition = new(-1f, 1f);
    private GameObject[,] Field;
    private GenerateStoneBlocks StoneBlocks;
    private GenerateBrickBlock BrickBlock;

    [FormerlySerializedAs("MapSize")] public Vector2Int mapSize = new(17, 17);
    [FormerlySerializedAs("StonePrefab")] public GameObject stonePrefab;
    [FormerlySerializedAs("BrickPrefab")] public GameObject brickPrefab;
    [FormerlySerializedAs("BomberMan")]public GameObject bomberMan;
    
    private void Start()
    {
        StoneBlocks = new GenerateStoneBlocks(StartPosition, stonePrefab);
        BrickBlock = new GenerateBrickBlock(bomberMan, StartPosition, brickPrefab);
        
        Generate();
    }

    private void Generate()
    {
        Field = new GameObject[mapSize.x, mapSize.y];
        
        StoneBlocks.CreateStoneWall(Field, mapSize);
        BrickBlock.CreateBrickWall(Field, mapSize);
    }
}

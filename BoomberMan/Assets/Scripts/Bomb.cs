using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bomb : MonoBehaviour
{
    private static class SidesFireDirection
    {
        public static readonly Vector2 Left = new(-1, 0);
        public static readonly Vector2 Right = new(1, 0);
        public static readonly Vector2 Up = new(0, 1);
        public static readonly Vector2 Down = new(0, -1);
    }
    
    [System.Serializable]
    public struct FireObjects
    {
        [FormerlySerializedAs("Mid")] public GameObject mid;
        [FormerlySerializedAs("Horizontal")] public GameObject horizontal;
        [FormerlySerializedAs("Left")] public GameObject left;
        [FormerlySerializedAs("Right")] public GameObject right;
        [FormerlySerializedAs("Vertical")] public GameObject vertical;
        [FormerlySerializedAs("Top")] public GameObject top;
        [FormerlySerializedAs("Bottom")] public GameObject bottom;
    }
    
    private float Counter;
    private int FireLength;
    private BomberMan Bomberman;
    
    private List<Vector2> CellsToBlowR;
    private List<Vector2> CellsToBlowL;
    private List<Vector2> CellsToBlowU;
    private List<Vector2> CellsToBlowD;
    
    private bool Calculated;
    private bool CanTick;
    
    [FormerlySerializedAs("FireObjects")] public FireObjects fireObject;
    [FormerlySerializedAs("Delay")] public float delay;

    [FormerlySerializedAs("StoneLayer")] public LayerMask stoneLayer;
    [FormerlySerializedAs("BlowableLayer")] public LayerMask blowableLayer;
    
    private void Start()
    {
        Bomberman = FindObjectOfType<BomberMan>();
        CanTick = !Bomberman.CheckDetonator();
        Calculated = false;
        Counter = delay;
        
        CellsToBlowR = new List<Vector2>();
        CellsToBlowL = new List<Vector2>();
        CellsToBlowU = new List<Vector2>();
        CellsToBlowD = new List<Vector2>();
    }

    private void Update()
    {
        if (Counter <= 0)
        {
            Blow();
        }
        else if (CanTick)
        {
            Counter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Blow();
        }
    }

    /*
     * Метод для знищення бомби
     */
    public void Blow()
    {
        CalculateFireDirections();
        Instantiate(fireObject.mid, transform.position, transform.rotation);
        
        DefiningImpactPrefabs(CellsToBlowL, fireObject.horizontal, fireObject.left);
        DefiningImpactPrefabs(CellsToBlowR, fireObject.horizontal, fireObject.right);
        DefiningImpactPrefabs(CellsToBlowU, fireObject.vertical, fireObject.top);
        DefiningImpactPrefabs(CellsToBlowD, fireObject.vertical, fireObject.bottom);

        Destroy(gameObject);
    }

    /*
     * Визначення префабів вогню в одному із напрямків.
     */
    private void DefiningImpactPrefabs(IReadOnlyList<Vector2> listBlow, GameObject mainFire, GameObject ultimateFire)
    {
        if (listBlow.Count <= 0) return;
        
        for (var i = 0; i < listBlow.Count; i++)
        {
            Instantiate(i == listBlow.Count - 1 ? ultimateFire : mainFire, listBlow[i], transform.rotation);
        }
    }

    /*
     * Метод для визначення напрямків вогню від бомби
     */
    private void CalculateFireDirections()
    {
        if (Calculated) return;
        
        FireLength = Bomberman.GetFireLength();
        
        CalculateDirection(CellsToBlowL, SidesFireDirection.Left);
        CalculateDirection(CellsToBlowR, SidesFireDirection.Right);
        CalculateDirection(CellsToBlowU, SidesFireDirection.Up);
        CalculateDirection(CellsToBlowD, SidesFireDirection.Down);
        
        Calculated = true;
    }

    private void CalculateDirection(ICollection<Vector2> listBlow, Vector2 direction)
    {
        for (var i = 1; i <= FireLength; i++)
        {
            var coordinateCell = new Vector2(transform.position.x + i*direction.x, transform.position.y + i*direction.y);
            if (Physics2D.OverlapCircle(coordinateCell, 0.1f, stoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(coordinateCell, 0.1f, blowableLayer))
            {
                listBlow.Add(coordinateCell);
                break;
            }
            listBlow.Add(coordinateCell);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject FireMid;
    public GameObject FireHorizontal;
    public GameObject FireLeft;
    public GameObject FireRight;
    public GameObject FireVertical;
    public GameObject FireTop;
    public GameObject FireBottom;

    public float Delay;
    private float Counter;

    public LayerMask StoneLayer;
    public LayerMask BlowableLayer;

    public List<Vector2> CellsToBlowR;
    public List<Vector2> CellsToBlowL;
    public List<Vector2> CellsToBlowU;
    public List<Vector2> CellsToBlowD;

    private bool calculated;
    private bool canTick;

    private int FireLength;

    private BomberMan bomberman;
    
    private static class SidesFireDirection
    {
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Right = new Vector2(1, 0);
        public static readonly Vector2 Up = new Vector2(0, 1);
        public static readonly Vector2 Down = new Vector2(0, -1);
    }
    
    private void Start()
    {
        bomberman = FindObjectOfType<BomberMan>();
        canTick = !bomberman.CheckDetonator();
        calculated = false;
        Counter = Delay;
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
        else if (canTick)
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
     * Метод для побудови вогню від бомби
     */
    public void Blow()
    {
        CalculateFireDirections();
        Instantiate(FireMid, transform.position, transform.rotation);
        
        DefiningImpactPrefabs(CellsToBlowL, FireHorizontal, FireLeft);
        DefiningImpactPrefabs(CellsToBlowR, FireHorizontal, FireRight);
        DefiningImpactPrefabs(CellsToBlowU, FireVertical, FireTop);
        DefiningImpactPrefabs(CellsToBlowD, FireVertical, FireBottom);

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
        if (calculated) return;
        
        FireLength = bomberman.GetFireLength();
        
        CalculateDirection(CellsToBlowL, SidesFireDirection.Left);
        CalculateDirection(CellsToBlowR, SidesFireDirection.Right);
        CalculateDirection(CellsToBlowU, SidesFireDirection.Up);
        CalculateDirection(CellsToBlowD, SidesFireDirection.Down);
        
        calculated = true;
    }

    private void CalculateDirection(ICollection<Vector2> listBlow, Vector2 direction)
    {
        for (var i = 1; i <= FireLength; i++)
        {
            var coordinateCell = new Vector2(transform.position.x + i*direction.x, transform.position.y + i*direction.y);
            if (Physics2D.OverlapCircle(coordinateCell, 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(coordinateCell, 0.1f, BlowableLayer))
            {
                listBlow.Add(coordinateCell);
                break;
            }
            listBlow.Add(coordinateCell);
        }
    }
}

using ChainResponsibility.Command.Button;
using ChainResponsibility.Direction;
using Enum;
using UnityEngine;
using UnityEngine.Serialization;

public class BomberMan : MonoBehaviour
{
    private DirectionPerson NextStep;
    private ChooseDirection Direction;
    private ButtonActiveCommand Detonator;
    private ButtonActiveCommand AddBomb;

    private int BombsAllowed;
    private int FireLength;
    private bool NoclipWalls;
    private bool NoclipBombs;
    private bool NoclipFire;
    private bool HasDetonator;
    
    private bool CanMove;
    private bool IsMoving;
    private bool InsideBomb;

    public Transform Sensor;
    public float SensorSize = 0.7f;
    public float SensorRange = 0.4f;

    public float MoveSpeed = 2;
    public float SpeedBoostPower = 0.5f;

    public LayerMask StoneLayer;
    public LayerMask BombLayer;
    public LayerMask BrickLayer;
    public LayerMask FireLayer;

    [FormerlySerializedAs("Bomb")] public GameObject BombPrefab;
    public GameObject DeathEffect;

    private void Start()
    {
        BombsAllowed = 1;
        FireLength = 1;
        NextStep = DirectionPerson.Stop;

        Detonator = new DetonatorBombButton(KeyCode.X);
        AddBomb = new AddBombButton(KeyCode.Z, BombPrefab);
        
        Direction = ChooseDirection.Link(
            new CheckDirection(KeyCode.LeftArrow, DirectionPerson.Left),
            new CheckDirection(KeyCode.UpArrow, DirectionPerson.Up),
            new CheckDirection(KeyCode.RightArrow, DirectionPerson.Right),
            new CheckDirection(KeyCode.DownArrow, DirectionPerson.Down)
            );
    }

    private void Update()
    {
        HandleSensor();
        HandleBombs();
        Move();

        Animate();
    }
    
    private void HandleSensor()
    {
        NextStep = Direction.Check();
        
        Sensor.transform.localPosition = new Vector2(0, 0);
        var size = new Vector2(SensorSize, SensorSize);
        
        InsideBomb = CheckLayer(size, BombLayer);
        
        Sensor.transform.localPosition = NextStep switch
        {
            DirectionPerson.Down => new Vector2(0, -SensorRange),
            DirectionPerson.Left => new Vector2(-SensorRange, 0),
            DirectionPerson.Right => new Vector2(SensorRange, 0),
            DirectionPerson.Up => new Vector2(0, SensorRange),
            _ => Sensor.transform.localPosition
        };
        
        CanMove = !CheckLayer(size, StoneLayer) &&
                  (NoclipWalls || !CheckLayer(size, BrickLayer)) &&
                  (InsideBomb || NoclipBombs || !CheckLayer(size, BombLayer));
    }

    private bool CheckLayer(Vector2 size, LayerMask layer)
    {
        return Physics2D.OverlapBox(Sensor.position, size, 0, layer);
    }
    
    private void HandleBombs()
    {
        AddBomb.Execute();
        Detonator.Execute();
    }
    
    private void Move()
    {
        if (!CanMove)
        {
            IsMoving = false;
            return;
        }
        IsMoving = true;
        
        switch (NextStep)
        {            
            case DirectionPerson.Down:
                transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y - MoveSpeed * Time.deltaTime);
                break;
            case DirectionPerson.Left:
                transform.position = new Vector2(transform.position.x - MoveSpeed * Time.deltaTime, Mathf.Round(transform.position.y));
                GetComponent<SpriteRenderer>().flipX = false;
                break;
            case DirectionPerson.Right:
                transform.position = new Vector2(transform.position.x + MoveSpeed * Time.deltaTime, Mathf.Round(transform.position.y));
                GetComponent<SpriteRenderer>().flipX = true;
                break;
            case DirectionPerson.Up:
                transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y + MoveSpeed * Time.deltaTime);
                break;
            case DirectionPerson.Stop:
                IsMoving = false;
                break;
        }
    }
    
    private void Animate()
    {
        var animator = GetComponent<Animator>();
        animator.SetInteger("Direction", (int)NextStep);
        animator.SetBool("Moving", IsMoving);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("PowerUp")) return;
        switch(other.GetComponent<PowerUpElement>().type)
        {
            case PowerUpType.EXTRA_BOMB:
                AddExtraBomb();
                break;
            case PowerUpType.FIRE:
                GetExtraFire();
                break;
            case PowerUpType.SPEED:
                GetExtraSpeed();
                break;
            case PowerUpType.NOCLIP_WALL:
                GetNoclipWalls();
                break;
            case PowerUpType.NOCLIP_FIRE:
                GetNoclipFire();
                break;
            case PowerUpType.NOCLIP_BOMB:
                GetNoclipBombs();
                break;
            case PowerUpType.DETONATOR:
                GetDetonator();
                break;
        }
        Destroy(other.gameObject);
    }

    public void Damage(TypeDamage source)
    {
        if (source == TypeDamage.Enemy || (source == TypeDamage.Fire && !NoclipFire)) Die();
    }

    private void Die()
    {
        Instantiate(DeathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void GetDetonator()
    {
        HasDetonator = true;
    }

    private void GetNoclipWalls()
    {
        NoclipWalls = true;
    }

    private void GetNoclipBombs()
    {
        NoclipBombs = true;
    }

    private void GetNoclipFire()
    {
        NoclipFire = true;
    }

    private void GetExtraSpeed()
    {
        MoveSpeed += SpeedBoostPower;
    }

    private void GetExtraFire()
    {
        ++FireLength;
    }

    private void AddExtraBomb()
    {
        ++BombsAllowed;
    }

    public int GetExtraBomb()
    {
        return BombsAllowed;
    }

    public bool CheckDetonator()
    {
        return HasDetonator;
    }

    public int GetFireLength()
    {
        return FireLength;
    }
}

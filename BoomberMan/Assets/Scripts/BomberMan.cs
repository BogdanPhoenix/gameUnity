using UnityEngine;

public class BomberMan : MonoBehaviour
{
    private bool ButtonLeft;
    private bool ButtonRight;
    private bool ButtonUp;
    private bool ButtonDown;
    private bool ButtonBomb;
    private bool ButtonDetonate;
    private bool CanMove;
    private bool InsideBomb;
    
    private int BomsAllowed;
    private int FireLenght;
    
    public int Direction;
    public Transform Sensor;
    public float SensorSize = 0.7f;
    public float SensorRange = 0.4f;
    public LayerMask StoneLayer;
    public LayerMask BombLayer;
    public LayerMask BrickLayer;
    public float MoveSpeed = 2;
    public GameObject Bomb;
    
    // Start is called before the first frame update
    void Start()
    {
        BomsAllowed = 2;
        FireLenght = 5;
    }

    void Update()
    {
        GetInput();
        GetDirection();
        HandleSensore();
        HandleBombs();
        Move();
    }

    void GetInput()
    {
        ButtonLeft = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) &&
                     !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
        ButtonRight = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) &&
                     !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
        ButtonUp = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow) &&
                     !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow);
        ButtonDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) &&
                     !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow);

        ButtonBomb = Input.GetKeyDown(KeyCode.F);
        ButtonDetonate = Input.GetKeyDown(KeyCode.X);
    }

    void GetDirection()
    {
        Direction = 5;
        if (ButtonLeft)
        {
            Direction = 4;
        }

        if (ButtonRight)
        {
            Direction = 6;
        }

        if (ButtonUp)
        {
            Direction = 8;
        }

        if (ButtonDown)
        {
            Direction = 2;
        }
    }

    void HandleSensore()
    {
        Sensor.transform.localPosition = new Vector2(0, 0);
        InsideBomb = Physics2D.OverlapBox(Sensor.position, new Vector2(SensorSize, SensorSize), 0, BombLayer);
        switch (Direction)
        {
            case 8:
                Sensor.transform.localPosition = new Vector2(0, SensorRange);
                break;
            case 2:
                Sensor.transform.localPosition = new Vector2(0, -SensorRange);
                break;
            case 4:
                Sensor.transform.localPosition = new Vector2(-SensorRange, 0);
                break;
            case 6:
                Sensor.transform.localPosition = new Vector2(SensorRange, 0);
                break;
        }

        CanMove = ! Physics2D.OverlapBox(Sensor.position, new Vector2(SensorSize, SensorSize), 0, StoneLayer);

        if (CanMove)
        {
            CanMove = ! Physics2D.OverlapBox(Sensor.position, new Vector2(SensorSize, SensorSize), 0, BrickLayer);
        }
        
        if (CanMove && !InsideBomb)
        {
            CanMove = ! Physics2D.OverlapBox(Sensor.position, new Vector2(SensorSize, SensorSize), 0, BombLayer);
        }
    }

    private void HandleBombs()
    {
        if (ButtonBomb && GameObject.FindGameObjectsWithTag("Bomb").Length < BomsAllowed)
        {
            Instantiate(Bomb, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), transform.rotation);
        }
    }
    
    void Move()
    {
        if(!CanMove) return;
        
        switch (Direction)
        {
            case 2:
                transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y - MoveSpeed*Time.deltaTime);
                break;
            case 4:
                transform.position = new Vector2(transform.position.x - MoveSpeed*Time.deltaTime, Mathf.Round(transform.position.y));
                break;
            case 6:
                transform.position = new Vector2(transform.position.x + MoveSpeed*Time.deltaTime, Mathf.Round(transform.position.y));
                break;
            case 8:
                transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y + MoveSpeed*Time.deltaTime);
                break;
        }
    }
    
    public void AddBomb()
    {
        ++BomsAllowed;
    }

    public void AddFire()
    {
        ++FireLenght;
    }

    public int GetFireLength()
    {
        return FireLenght;
    }
}

using ChainResponsibility.Command.Button;
using ChainResponsibility.Direction;
using Command.Button;
using Damage;
using Enum;
using UnityEngine;
using UnityEngine.Serialization;

namespace BomberMan
{
    public class BomberManPlayer : MonoBehaviour, IDamage
    {
        private DirectionPerson NextStep;
        private ChooseDirection Direction;
        private ButtonActiveCommand Detonator;
        private ButtonActiveCommand AddBomb;
        private BomberManPower BomberManPower;
    
        private bool CanMove;
        private bool IsMoving;

        [FormerlySerializedAs("Sensor")] public Transform sensor;
        [FormerlySerializedAs("SensorSize")] public float sensorSize = 0.7f;
        [FormerlySerializedAs("SensorRange")] public float sensorRange = 0.4f;

        [FormerlySerializedAs("MoveSpeed")] public float moveSpeed = 2;

        [FormerlySerializedAs("StoneLayer")] public LayerMask stoneLayer;
        [FormerlySerializedAs("BombLayer")] public LayerMask bombLayer;
        [FormerlySerializedAs("BrickLayer")] public LayerMask brickLayer;
        [FormerlySerializedAs("FireLayer")] public LayerMask fireLayer;

        [FormerlySerializedAs("Bomb")] public GameObject BombPrefab;
        [FormerlySerializedAs("DeathEffect")] public GameObject deathEffect;

        private void Start()
        {
            NextStep = DirectionPerson.Stop;

            Detonator = new DetonatorBombButton(KeyCode.X);
            AddBomb = new AddBombButton(KeyCode.Z, BombPrefab);
            BomberManPower = BomberManPower.GetInstance();
        
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
        
            sensor.transform.localPosition = new Vector2(0, 0);
            var size = new Vector2(sensorSize, sensorSize);
        
            var insideBomb = CheckLayer(size, bombLayer);
        
            sensor.transform.localPosition = NextStep switch
            {
                DirectionPerson.Down => new Vector2(0, -sensorRange),
                DirectionPerson.Left => new Vector2(-sensorRange, 0),
                DirectionPerson.Right => new Vector2(sensorRange, 0),
                DirectionPerson.Up => new Vector2(0, sensorRange),
                _ => sensor.transform.localPosition
            };
        
            CanMove = !CheckLayer(size, stoneLayer) &&
                      (BomberManPower.NoClipWalls || !CheckLayer(size, brickLayer)) &&
                      (insideBomb || BomberManPower.NoClipBombs || !CheckLayer(size, bombLayer));
        }

        private bool CheckLayer(Vector2 size, LayerMask layer)
        {
            return Physics2D.OverlapBox(sensor.position, size, 0, layer);
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
                    transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y - moveSpeed * Time.deltaTime);
                    break;
                case DirectionPerson.Left:
                    transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, Mathf.Round(transform.position.y));
                    GetComponent<SpriteRenderer>().flipX = false;
                    break;
                case DirectionPerson.Right:
                    transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, Mathf.Round(transform.position.y));
                    GetComponent<SpriteRenderer>().flipX = true;
                    break;
                case DirectionPerson.Up:
                    transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y + moveSpeed * Time.deltaTime);
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
        
            var power = other.GetComponent<PowerUpElement>();
            BomberManPower.UpdatePower(power);
            Destroy(other.gameObject);
        }

        public void Damage(TypeDamage source)
        {
            if (source == TypeDamage.Enemy || (source == TypeDamage.Fire && !BomberManPower.NoClipFire)) Die();
        }

        private void Die()
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}

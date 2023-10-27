using ChainResponsibility.Direction;
using Command.Button;
using Damage;
using Enum;
using Map.PowerUp;
using Observer.Event.Interface;
using Observer.Manager;
using Observer.Manager.Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace BomberMan
{
    public class BomberManPlayer : MonoBehaviour, IDamage
    {
        private static readonly int DirectionAnimate = Animator.StringToHash("Direction");
        private static readonly int MovingAnimate = Animator.StringToHash("Moving");
        
        private ButtonActiveCommand AddBomb;
        private ButtonActiveCommand Detonator;
        private ChooseDirection Direction;
        private DirectionPerson NextStep;
        private EventManager<IEventListenerGameOver> EventManagerGameOver;
        
        [FormerlySerializedAs("Sensor")] public Transform sensor;
        [FormerlySerializedAs("SensorSize")] public float sensorSize = 0.7f;
        [FormerlySerializedAs("SensorRange")] public float sensorRange = 0.4f;

        [FormerlySerializedAs("StoneLayer")] public LayerMask stoneLayer;
        [FormerlySerializedAs("BombLayer")] public LayerMask bombLayer;
        [FormerlySerializedAs("BrickLayer")] public LayerMask brickLayer;
        [FormerlySerializedAs("FireLayer")] public LayerMask fireLayer;

        [FormerlySerializedAs("Bomb")] public GameObject BombPrefab;
        [FormerlySerializedAs("DeathEffect")] public GameObject deathEffect;
        
        public BomberManPower Power { get; private set; }

        private void Start()
        {
            NextStep = DirectionPerson.Stop;

            Detonator = new DetonatorBombButton(KeyCode.X);
            AddBomb = new AddBombButton(KeyCode.Z, BombPrefab);
            Power = BomberManPower.GetInstance();

            Direction = ChooseDirection.Link(
                new CheckDirection(KeyCode.LeftArrow, DirectionPerson.Left),
                new CheckDirection(KeyCode.UpArrow, DirectionPerson.Up),
                new CheckDirection(KeyCode.RightArrow, DirectionPerson.Right),
                new CheckDirection(KeyCode.DownArrow, DirectionPerson.Down)
            );

            EventManagerGameOver = new EventManagerGameOver(TypeActive.GameOver);
            EventManagerGameOver.Subscribe(TypeActive.GameOver, new GameOver());
        }

        private void Update()
        {
            HandleBombs();
            Move();
            Animate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("PowerUp")) return;

            var power = other.GetComponent<PowerUpElement>();
            Power.UpdatePower(power);
            Destroy(other.gameObject);
        }

        public void Damage(TypeDamage source)
        {
            if (source == TypeDamage.Enemy || (source == TypeDamage.Fire && !Power.NoClipFire)) Die();
        }

        private void HandleBombs()
        {
            AddBomb.Execute();
            Detonator.Execute();
        }

        private void Move()
        {
            var canMove = HandleSensor();
            if (!canMove) return;
            
            transform.position = NextStep switch
            {
                DirectionPerson.Down => VerticalMove(-Power.MoveSpeed),
                DirectionPerson.Up => VerticalMove(Power.MoveSpeed),
                DirectionPerson.Left => HorizontalMove(-Power.MoveSpeed),
                DirectionPerson.Right => HorizontalMove(Power.MoveSpeed),
                _ => transform.position
            };
        }

        private bool HandleSensor()
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

            return !CheckLayer(size, stoneLayer) &&
                   (Power.NoClipWalls || !CheckLayer(size, brickLayer)) &&
                   (insideBomb || Power.NoClipBombs || !CheckLayer(size, bombLayer));
        }

        private bool CheckLayer(Vector2 size, LayerMask layer)
        {
            return Physics2D.OverlapBox(sensor.position, size, 0, layer);
        }

        private Vector2 HorizontalMove(float speed)
        {
            GetComponent<SpriteRenderer>().flipX = speed > 0;
            return new Vector2(transform.position.x + speed * Time.deltaTime,
                Mathf.Round(transform.position.y));
        }

        private Vector2 VerticalMove(float speed)
        {
            return new Vector2(Mathf.Round(transform.position.x),
                transform.position.y + speed * Time.deltaTime);
        }

        private void Animate()
        {
            var animator = GetComponent<Animator>();
            animator.SetInteger(DirectionAnimate, (int)NextStep);
            animator.SetBool(MovingAnimate, NextStep != DirectionPerson.Stop);
        }

        private void Die()
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            ((INotifySimple)EventManagerGameOver).Notify(TypeActive.GameOver);
        }
    }
}
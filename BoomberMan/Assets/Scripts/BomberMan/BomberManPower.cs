using System;
using Enum;
using Map.PowerUp;

namespace BomberMan
{
    public class BomberManPower
    {
        private static BomberManPower _bomberManPower;
        private float SpeedBoostPower;

        public float MoveSpeed { get; private set; }
        public int BombsAllowed { get; private set; }

        public int FireLength { get; private set; }

        public bool NoClipWalls { get; private set; }

        public bool NoClipBombs { get; private set; }

        public bool NoClipFire { get; private set; }

        public bool HasDetonator { get; private set; }

        private BomberManPower()
        {
            ResetPower();
        }
        
        public static BomberManPower GetInstance()
        {
            return _bomberManPower ??= new BomberManPower();
        }

        public void ResetPower()
        {
            MoveSpeed = 2f;
            SpeedBoostPower = 0.5f;
            BombsAllowed = 1;
            FireLength = 1;
            NoClipWalls = false;
            NoClipBombs = false;
            NoClipFire = false;
            HasDetonator = false;
        }

        public void UpdatePower(PowerUpElement power)
        {
            power.ActivateSound();

            switch (power.type)
            {
                case PowerUpType.ExtraBomb:
                    AddExtraBomb();
                    break;
                case PowerUpType.Fire:
                    IncreasePowerBomb();
                    break;
                case PowerUpType.Speed:
                    IncreaseSpeed();
                    break;
                case PowerUpType.NoClipWall:
                    ActiveNoClipWalls();
                    break;
                case PowerUpType.NoClipFire:
                    ActiveNoClipFire();
                    break;
                case PowerUpType.NoClipBomb:
                    ActiveNoClipBombs();
                    break;
                case PowerUpType.Detonator:
                    AddDetonator();
                    break;
                case PowerUpType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddDetonator()
        {
            HasDetonator = true;
        }

        private void ActiveNoClipWalls()
        {
            NoClipWalls = true;
        }

        private void ActiveNoClipBombs()
        {
            NoClipBombs = true;
        }

        private void ActiveNoClipFire()
        {
            NoClipFire = true;
        }

        private void IncreaseSpeed()
        {
            MoveSpeed += SpeedBoostPower;
        }

        private void IncreasePowerBomb()
        {
            ++FireLength;
        }

        private void AddExtraBomb()
        {
            ++BombsAllowed;
        }
    }
}
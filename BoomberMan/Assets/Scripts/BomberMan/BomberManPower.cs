using Enum;

namespace BomberMan
{
    public class BomberManPower
    {
        private static BomberManPower _bomberManPower;
        private readonly float SpeedBoostPower;

        private float MoveSpeed = 0.5f;

        public int BombsAllowed { get; private set; }

        public int FireLength { get; private set; }

        public bool NoClipWalls { get; private set; }

        public bool NoClipBombs { get; private set; }

        public bool NoClipFire { get; private set; }

        public bool HasDetonator { get; private set; }

        private BomberManPower()
        {
            SpeedBoostPower = 0.5f;
            BombsAllowed = 1;
            FireLength = 1;
            NoClipWalls = false;
            NoClipBombs = false;
            NoClipFire = false;
            HasDetonator = false;
        }

        public static BomberManPower GetInstance()
        {
            return _bomberManPower ??= new BomberManPower();
        }

        public void UpdatePower(PowerUpElement power)
        {
            power.ActivateSound();
        
            switch(power.type)
            {
                case PowerUpType.EXTRA_BOMB:
                    AddExtraBomb();
                    break;
                case PowerUpType.FIRE:
                    IncreasePowerBomb();
                    break;
                case PowerUpType.SPEED:
                    IncreaseSpeed();
                    break;
                case PowerUpType.NOCLIP_WALL:
                    ActiveNoClipWalls();
                    break;
                case PowerUpType.NOCLIP_FIRE:
                    ActiveNoClipFire();
                    break;
                case PowerUpType.NOCLIP_BOMB:
                    ActiveNoClipBombs();
                    break;
                case PowerUpType.DETONATOR:
                    AddDetonator();
                    break;
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
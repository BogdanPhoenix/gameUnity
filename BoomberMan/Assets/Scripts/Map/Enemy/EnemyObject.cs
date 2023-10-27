using Damage;
using Enum;
using Map.ActionsOnObjects;
using UnityEngine;

namespace Map.Enemy
{
    public class EnemyObject : MonoBehaviour, IDamage
    {
        private IActionRemove<EnemyObject> EnemiesOnMap;
        private PathFinder PathFinder;
        private Vector2Int NextStep;
        private bool IsMoving;
    
        public GameObject BomberMan;
        public GameObject DeathEffect;
        public float MoveSpeed;
        public LayerMask SolidLayer;

        private void Start()
        {
            PathFinder = new PathFinder(gameObject, SolidLayer);
            EnemiesOnMap = EnemyOnMap.GetInstance();

            ReCalculateNextStep();
            IsMoving = true;
            InvokeRepeating(nameof(Update), 1f, 1f);
        }

        private void Update()
        {
            if (BomberMan == null) return;

            if (NextStep.Equals(Vector2Int.zero) || !IsMoving)
            {
                if (!(Vector2.Distance(transform.position, BomberMan.transform.position) > 0.5f)) return;

                ReCalculateNextStep();
                IsMoving = true;
            }
            else
            {
                MoveTowardsTarget();
            }
        }

        public void Damage(TypeDamage source)
        {
            if (source != TypeDamage.Fire) return;

            Instantiate(DeathEffect, transform.position, transform.rotation);
            EnemiesOnMap.Remove(this);
            Destroy(gameObject);
        }

        public void ReCalculateNextStep()
        {
            if(BomberMan == null) return;
            
            AssignNewStep(BomberMan.transform.position);

            if (!NextStep.Equals(Vector2Int.zero)) return;

            AssignNewStep(PathFinder.GetRandomPositionOnPath());
        }

        private void AssignNewStep(Vector2 position)
        {
            NextStep = PathFinder.NextStep(position);
        }

        private void MoveTowardsTarget()
        {
            var distance = Vector2.Distance(transform.position, NextStep);
            if (distance > 0.1f)
                transform.position = Vector2.MoveTowards(transform.position, NextStep, MoveSpeed * Time.deltaTime);
            else
                IsMoving = false;
        }
    }
}
using Enum;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PathFinder PathFinder;
    private bool IsMoving;
    private Vector2Int NextStep;

    public GameObject BomberMan;
    public GameObject DeathEffect;
    public float MoveSpeed;
    public LayerMask SolidLayer;

    public static void RebuildRoute()
    {
        var enemies = FindObjectsOfType<Enemy>();
        foreach (var item in enemies)
        {
            item.ReCalculateNextStep();
        }
    }
    
    private void Start()
    {
        PathFinder = new PathFinder(gameObject, SolidLayer);
        ReCalculateNextStep();
        IsMoving = true;
    }
    
    private void Update()
    {
        if(BomberMan == null) return;
        
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
    
    private void ReCalculateNextStep()
    {
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
        {
            transform.position = Vector2.MoveTowards(transform.position, NextStep, MoveSpeed * Time.deltaTime);
        }
        else
        {
            IsMoving = false;
        }
    }

    public void Damage(TypeDamage source)
    {
        if (source == TypeDamage.Enemy) return;
        
        Instantiate(DeathEffect, transform.position, transform.rotation);
        Destroy (gameObject);
    }
}
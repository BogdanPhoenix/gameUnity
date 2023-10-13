using System.Collections.Generic;
using Enum;
using UnityEngine;

public class BehaviorEnemy : MonoBehaviour
{
    private PathFinder PathFinder;
    private bool IsMoving;
    private Vector2Int NextStep;

    public GameObject BomberMan;
    public GameObject DeathEffect;
    public float MoveSpeed;
    public LayerMask SolidLayer;

    private static readonly ISet<BehaviorEnemy> Enemies = new HashSet<BehaviorEnemy>();

    public static void AddEnemy(GameObject enemy)
    {
        Enemies.Add(enemy.GetComponent<BehaviorEnemy>());
    }

    private static void RemoveEnemy(BehaviorEnemy behaviorEnemy)
    {
        Enemies.Remove(behaviorEnemy);
        
        if (Enemies.Count != 0) return;
        
        Debug.Log("You win");
        FindObjectOfType<GenerateMap>().NextLevel();
    }
    
    public static void RebuildRoute()
    {
        foreach (var item in Enemies)
        {
            item.ReCalculateNextStep();
        }
    }
    
    private void Start()
    {
        PathFinder = new PathFinder(gameObject, SolidLayer);
        
        ReCalculateNextStep();
        IsMoving = true;
        InvokeRepeating(nameof(Update), 1f, 1f);
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
        RemoveEnemy(this);
        Destroy (gameObject);
    }
}
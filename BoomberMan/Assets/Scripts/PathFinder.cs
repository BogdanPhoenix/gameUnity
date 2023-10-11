using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder
{
    private System.Collections.Generic.ISet<Node> CheckedNodes;
    private System.Collections.Generic.ISet<Node> FreeNodes = new HashSet<Node>();
    private IList<Node> WaitingNodes;
    private readonly LayerMask SolidLayer;
    private readonly GameObject EnemyObject;

    public PathFinder(GameObject enemyObject, LayerMask solidLayer)
    {
        EnemyObject = enemyObject;
        SolidLayer = solidLayer;
    }

    public Vector2Int GetRandomPositionOnPath()
    {
        if (FreeNodes.Count == 0)
        {
            return Vector2Int.zero;
        }
        
        var randomPos = Random.Range(0, FreeNodes.Count);
        var node = FreeNodes.ElementAt(randomPos);
        
        return node!.Position;
    }

    public Vector2Int NextStep(Vector2 target)
    {
        var path = GetPath(target);
        return path.Count == 0 ? Vector2Int.zero : path[^1];
    }
    
    private IList<Vector2Int> GetPath(Vector2 target)
    {
        var startPosition = Vector2Int.RoundToInt(EnemyObject.transform.position);
        var targetPosition = Vector2Int.RoundToInt(target);
        
        if(startPosition == targetPosition) return new List<Vector2Int>();
        
        var startNode = new Node(0, startPosition, targetPosition, null);
        
        WaitingNodes = new List<Node>();
        CheckedNodes = new HashSet<Node>();
        
        CheckedNodes.Add(startNode);
        WaitingNodes.AddRange(GetNeighbourNodes(startNode));
        
        while(WaitingNodes.Count > 0)
        {
            var nodeToCheck = WaitingNodes.FirstOrDefault(x => x.F == WaitingNodes.Min(y => y.F));
    
            if (nodeToCheck!.Position == targetPosition)
            {
                return CalculatePathFromNode(nodeToCheck).ToList();
            }
    
            if(CheckWalkable(nodeToCheck))
            {
                WaitingNodes.Remove(nodeToCheck);
                if (CheckedNodes.Any(x => x.Position == nodeToCheck.Position)) continue;
                CheckedNodes.Add(nodeToCheck);
                WaitingNodes.AddRange(GetNeighbourNodes(nodeToCheck));
            }
            else
            {
                WaitingNodes.Remove(nodeToCheck);
                CheckedNodes.Add(nodeToCheck);
            }
        }
        FreeNodes = CheckedNodes;
        
        return new List<Vector2Int>();
    }
    
    //     while(WaitingNodes.Count > 0)
    //     {
    //         var nodeToCheck = WaitingNodes.FirstOrDefault(x => x.F == WaitingNodes.Min(y => y.F));
    //
    //         if (nodeToCheck!.Position == targetPosition)
    //         {
    //             return CalculatePathFromNode(nodeToCheck).ToList();
    //         }
    //
    //         if (!CheckWalkable(nodeToCheck) || CheckedNodes.Any(x => x.Position == nodeToCheck.Position)) continue;
    //         
    //         CheckedNodes.Add(nodeToCheck);
    //         WaitingNodes.AddRange(GetNeighbourNodes(nodeToCheck));
    //     }

    private IEnumerable<Vector2Int> CalculatePathFromNode(Node node)
    {
        var path = new List<Vector2Int>();
        var currentNode = node;

        while(currentNode.PreviousNode != null)
        {
            if (CheckWalkable(currentNode))
            {
                path.Add(currentNode.Position);
            }
            currentNode = currentNode.PreviousNode;
        }

        return path;
    }

    private bool CheckWalkable(Node node)
    {
        return !Physics2D.OverlapCircle(node.Position, 0.1f, SolidLayer);
    }

    private static IEnumerable<Node> GetNeighbourNodes (Node node)
    {
        var neighbours = new List<Node>
        {
            new(node.DistanceFromStartToNode + 1, new Vector2Int(
                    node.Position.x-1, node.Position.y), node.TargetPosition, node),
            new(node.DistanceFromStartToNode + 1, new Vector2Int(
                    node.Position.x+1, node.Position.y), node.TargetPosition, node),
            new(node.DistanceFromStartToNode + 1, new Vector2Int(
                    node.Position.x, node.Position.y-1), node.TargetPosition, node),
            new(node.DistanceFromStartToNode + 1, new Vector2Int(
                    node.Position.x, node.Position.y+1), node.TargetPosition, node)
        };

        return neighbours;
    }
    private class Node
    {
        public readonly Vector2Int Position;
        public readonly Vector2Int TargetPosition;
        public readonly Node PreviousNode; // Use Node? instead of Node
        public readonly int F;
        public readonly int DistanceFromStartToNode;

        public Node(int distanceFromStartToNode, Vector2Int nodePosition, Vector2Int targetPosition, Node previousNode)
        {
            Position = nodePosition;
            TargetPosition = targetPosition;
            PreviousNode = previousNode;
            DistanceFromStartToNode = distanceFromStartToNode;
            var distanceFromNodeToTarget = (int)Vector2Int.Distance(targetPosition, Position);
            F = DistanceFromStartToNode + distanceFromNodeToTarget;
        }
    }

}

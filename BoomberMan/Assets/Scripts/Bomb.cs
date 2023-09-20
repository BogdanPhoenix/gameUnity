using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bomb : MonoBehaviour
{
    private float Counter;
    private int FireLenght;
    
    private List<Vector2> CellsToBlowL;
    private List<Vector2> CellsToBlowR;
    private List<Vector2> CellsToBlowU;
    private List<Vector2> CellsToBlowD;
    
    public GameObject FireMid;
    public GameObject FireHorizontal;
    public GameObject FireLeft;
    public GameObject FireRight;
    public GameObject FireVertical;
    public GameObject FireTop;
    public GameObject FireBottom;
    
    public float Delay;
    public LayerMask StoneLayer;
    public LayerMask BrickLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        Counter = Delay;
        CellsToBlowL = new List<Vector2>();
        CellsToBlowR = new List<Vector2>();
        CellsToBlowU = new List<Vector2>();
        CellsToBlowD = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Counter > 0)
        {
            Counter -= Time.deltaTime;
        }
        else
        {
            Blow();
        }
    }

    private void Blow()
    {
        CalculateFire();
        Instantiate(FireMid, transform.position, transform.rotation);
        
        //L
        for (int i = 0; i < CellsToBlowL.Count; ++i)
        {
            if (i == CellsToBlowL.Count - 1)
            {
                Instantiate(FireLeft, CellsToBlowL[i], transform.rotation);
            }
            else
            {
                Instantiate(FireHorizontal, CellsToBlowL[i], transform.rotation);
            }
        }
        
        //R
        for (int i = 0; i < CellsToBlowR.Count; ++i)
        {
            if (i == CellsToBlowR.Count - 1)
            {
                Instantiate(FireRight, CellsToBlowR[i], transform.rotation);
            }
            else
            {
                Instantiate(FireHorizontal, CellsToBlowR[i], transform.rotation);
            }
        }
        
        Debug.Log(CellsToBlowU.Count);
        //U
        for (int i = 0; i < CellsToBlowU.Count; ++i)
        {
            if (i == CellsToBlowU.Count - 1)
            {
                Instantiate(FireTop, CellsToBlowU[i], transform.rotation);
            }
            else
            {
                Instantiate(FireVertical, CellsToBlowU[i], transform.rotation);
            }
        }
        
        //U
        for (int i = 0; i < CellsToBlowD.Count; ++i)
        {
            if (i == CellsToBlowD.Count - 1)
            {
                Instantiate(FireBottom, CellsToBlowD[i], transform.rotation);
            }
            else
            {
                Instantiate(FireVertical, CellsToBlowD[i], transform.rotation);
            }
        }
        Destroy(gameObject);
    }

    private void CalculateFire()
    {
        FireLenght = FindObjectOfType<BomberMan>().GetFireLength();
        CellsToBlowL.Clear();
        
        //L
        for (int i = 1; i <= FireLenght; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x - i, transform.position.y), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x - i, transform.position.y), 0.1f, BrickLayer))
            {
                CellsToBlowL.Add(new Vector2(transform.position.x - i, transform.position.y));
                break;
            }
            CellsToBlowL.Add(new Vector2(transform.position.x - i, transform.position.y));
        }
        
        CellsToBlowR.Clear();
        
        //R
        for (int i = 1; i <= FireLenght; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x + i, transform.position.y), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x + i, transform.position.y), 0.1f, BrickLayer))
            {
                CellsToBlowR.Add(new Vector2(transform.position.x + i, transform.position.y));
                break;
            }
            CellsToBlowR.Add(new Vector2(transform.position.x + i, transform.position.y));
        }
        
        CellsToBlowU.Clear();
        
        //U
        for (int i = 1; i <= FireLenght; i++)
        {
            // if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + i), 0.1f, StoneLayer))
            // {
            //     break;
            // }
            // if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + i), 0.1f, BrickLayer))
            // {
            //     CellsToBlowU.Add(new Vector2(transform.position.x, transform.position.y + i));
            //     break;
            // }
            // CellsToBlowU.Add(new Vector2(transform.position.x, transform.position.y + i));
            if (Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(transform.position.x, transform.position.y + i), StoneLayer))
            {
                break;
            }

            if (Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(transform.position.x, transform.position.y + i), BrickLayer))
            {
                CellsToBlowU.Add(new Vector2(transform.position.x, transform.position.y + i));
                break;
            }
            CellsToBlowU.Add(new Vector2(transform.position.x, transform.position.y + i));
        }
        
        CellsToBlowD.Clear();
        
        //U
        for (int i = 1; i <= FireLenght; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - i), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - i), 0.1f, BrickLayer))
            {
                CellsToBlowD.Add(new Vector2(transform.position.x, transform.position.y - i));
                break;
            }
            CellsToBlowD.Add(new Vector2(transform.position.x, transform.position.y - i));
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var item in CellsToBlowL)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(item, 0.2f);
        }
        foreach (var item in CellsToBlowR)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(item, 0.2f);
        }
        foreach (var item in CellsToBlowU)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(item, 0.2f);
        }
        foreach (var item in CellsToBlowD)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(item, 0.2f);
        }
    }
}

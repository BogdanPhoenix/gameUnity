using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Field field;
    void Start()
    {
        var listOfStone = GameObject.FindGameObjectsWithTag("Stone").ToArray();
        field = new Field()
        {
            MinX = listOfStone.Min(x => x.transform.position.x) - 0.5f,
            MinY = listOfStone.Min(y => y.transform.position.y) - 0.5f,
            MaxX = listOfStone.Max(x => x.transform.position.x) + 0.5f,
            MaxY = listOfStone.Max(y => y.transform.position.y) + 0.5f,
        };
    }

    void Update()
    {
        float cameraHalfHeight = GetComponent<Camera>().orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * ((float)Screen.width / Screen.height);

        var bomberman = FindObjectOfType<BomberMan>().transform.position;
        var x = bomberman.x;
        var y = bomberman.y;

        x = Mathf.Clamp(x, field.MinX + cameraHalfWidth, field.MaxX - cameraHalfWidth);
        y = Mathf.Clamp(y, field.MinY + cameraHalfHeight, field.MaxY - cameraHalfHeight);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(field.MinX, field.MinY), new Vector2(field.MaxX, field.MinY));
        Gizmos.DrawLine(new Vector2(field.MinX, field.MaxY), new Vector2(field.MaxX, field.MaxY));
    }

    public struct Field
    {
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;
    }
}

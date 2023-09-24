using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Field field;
    
    // Start is called before the first frame update
    private void Start()
    {
        var listOfStone = GameObject.FindGameObjectsWithTag("Stone").ToArray();
        field = new Field()
        {
            MinX = listOfStone.Min(x => x.transform.position.x) - 0.5f,
            MinY = listOfStone.Min(x => x.transform.position.y) - 0.5f,
            MaxX = listOfStone.Max(x => x.transform.position.x) + 0.5f,
            MaxY = listOfStone.Max(x => x.transform.position.y) + 0.5f
        };
    }

    // Update is called once per frame
    private void Update()
    {
        var bomberman = FindObjectOfType<BomberMan>();
        if (bomberman == null) return;

        float cameraHalfHeight = GetComponent<Camera>().orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * ((float)Screen.width / Screen.height);

        var bombermanPosition = bomberman.transform.position;
        var x = bombermanPosition.x;
        var y = bombermanPosition.y;

        x = Mathf.Clamp(x, field.MinX + cameraHalfWidth, field.MaxX - cameraHalfWidth);
        y = Mathf.Clamp(y, field.MinY + cameraHalfHeight, field.MaxY - cameraHalfHeight);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public struct Field
    {
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;
    }
}

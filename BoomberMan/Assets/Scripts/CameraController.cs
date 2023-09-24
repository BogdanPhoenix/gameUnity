using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Field field;
    private Camera Camera;
    private BomberMan Bomberman;

    private void Start()
    {
        Bomberman = FindObjectOfType<BomberMan>();
        Camera = GetComponent<Camera>();
        var listOfStone = GameObject.FindGameObjectsWithTag("Stone").ToArray();
        field = new Field()
        {
            MinX = listOfStone.Min(x => x.transform.position.x) - 0.5f,
            MinY = listOfStone.Min(x => x.transform.position.y) - 0.5f,
            MaxX = listOfStone.Max(x => x.transform.position.x) + 0.5f,
            MaxY = listOfStone.Max(x => x.transform.position.y) + 0.5f
        };
    }

    private void Update()
    {
        /*
         * Видалити в майбутьному.
         * Причина - виникають помилки, якщо гравець загинув, а оновлення сцени продовжується.
         */
        if (Bomberman == null) return;

        float cameraHalfHeight = Camera.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * ((float)Screen.width / Screen.height);

        var bombermanPosition = Bomberman.transform.position;
        
        var x = bombermanPosition.x;
        var y = bombermanPosition.y;
        
        x = Mathf.Clamp(x, field.MinX + cameraHalfWidth, field.MaxX - cameraHalfWidth);
        y = Mathf.Clamp(y, field.MinY + cameraHalfHeight, field.MaxY - cameraHalfHeight);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    private struct Field
    {
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;
    }
}

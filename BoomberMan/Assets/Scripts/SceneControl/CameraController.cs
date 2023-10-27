using System.Linq;
using BomberMan;
using UnityEngine;

namespace SceneControl
{
    public class CameraController : MonoBehaviour
    {
        private const string TagStone = "Stone";
    
        private BomberManPlayer Bomberman;
        private Camera Camera;
        private Field field;

        public void Start()
        {
            Bomberman = FindObjectOfType<BomberManPlayer>();
            Camera = GetComponent<Camera>();
            var listOfStone = GameObject.FindGameObjectsWithTag(TagStone).ToArray();

            field = new Field
            {
                MinX = listOfStone.Min(x => x.transform.position.x) - 0.5f,
                MinY = listOfStone.Min(x => x.transform.position.y) - 0.5f,
                MaxX = listOfStone.Max(x => x.transform.position.x) + 0.5f,
                MaxY = listOfStone.Max(x => x.transform.position.y) + 0.5f
            };
        }

        private void Update()
        {
            if (Bomberman == null) return;

            var cameraHalfHeight = Camera.orthographicSize;
            var cameraHalfWidth = cameraHalfHeight * ((float)Screen.width / Screen.height);

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
}
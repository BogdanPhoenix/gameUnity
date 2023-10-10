using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float Delay;
    private float Counter;
    
    private void Start()
    {
        Counter = Delay;
    }

    private void Update()
    {
        if (Counter > 0)
        {
            Counter -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

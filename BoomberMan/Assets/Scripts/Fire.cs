using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject BrickDeathEffect;
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            Destroy(other.gameObject);
            Instantiate(BrickDeathEffect, transform.position, transform.rotation);
        }
    }
}

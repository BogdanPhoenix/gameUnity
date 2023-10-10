using UnityEngine;
using UnityEngine.Serialization;

public class Fire : MonoBehaviour
{
    [FormerlySerializedAs("BrickDeathEffect")] public GameObject brickDeathEffect;

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Brick")) return;
        Destroy(other.gameObject);
        Instantiate(brickDeathEffect, transform.position, transform.rotation);
    }
}

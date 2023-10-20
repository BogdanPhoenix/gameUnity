using Enum;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float invincibilityTime;

    private void Update()
    {
        if (invincibilityTime > 0)
        {
            invincibilityTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Fire") && invincibilityTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}

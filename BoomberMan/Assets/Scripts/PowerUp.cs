using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // 0 - extra bomb
    // 1 - fire
    // 2 - speed
    // 3 - noclip wall
    // 4 - noclip fire
    // 5 - noclip bomb
    // 6 - detonator

    public int Type;
    public PowerUpType NewType;

    public float InvincibilityTime;

    void Update()
    {
        if (InvincibilityTime > 0) InvincibilityTime -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Fire") && InvincibilityTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}

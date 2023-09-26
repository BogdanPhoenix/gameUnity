using Enum;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public TypeDamage source;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<BomberMan>().Damage(source);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Damage(source);
        }
    }
}

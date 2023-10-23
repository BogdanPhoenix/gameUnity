using Enum;
using UnityEngine;

namespace Damage
{
    public class CallDamage : MonoBehaviour
    {
        public TypeDamage source;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damage = other.GetComponent<IDamage>();
            damage?.Damage(source);
        }
    }
}
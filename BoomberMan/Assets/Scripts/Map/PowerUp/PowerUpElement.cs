using Audio;
using Enum;
using UnityEngine;

namespace Map.PowerUp
{
    public class PowerUpElement : MonoBehaviour
    {
        public AudioClip clip;
        public PowerUpType type;
        public float invincibilityTime;

        [Range(1, 100)] public int Weight = 50;

        private void Update()
        {
            if (invincibilityTime > 0) invincibilityTime -= Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Fire") && invincibilityTime <= 0) Destroy(gameObject);
        }

        public void ActivateSound()
        {
            var volume = VolumeControl.CalculateMaxValue("SoundVolume");
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
    }
}
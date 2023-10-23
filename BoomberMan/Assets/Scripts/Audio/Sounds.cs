using UnityEngine;

namespace Audio
{
    public class Sounds : MonoBehaviour
    {
        public AudioClip[] sounds;
        public float maxDistance = 10f;
        public float minVolume = 0.1f;
        public float maxVolume = 1f;
        private GameObject player;
        private float targetVolume;
        private AudioSource soundSource => gameObject.AddComponent<AudioSource>();

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            soundSource.minDistance = 0;
            soundSource.maxDistance = maxDistance;
        }

        private void Update()
        {
            var distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            var distanceRatio = Mathf.Clamp01(distanceToPlayer / maxDistance);
            targetVolume = Mathf.Lerp(minVolume, maxVolume, 1f - distanceRatio);

            soundSource.volume = targetVolume;
        }

        protected void PlaySound(bool random = false, bool destroyed = false, float p1 = 0.85f, float p2 = 1.2f)
        {
            var clip = random ? sounds[Random.Range(0, sounds.Length)] : sounds[0];
            soundSource.pitch = Random.Range(p1, p2);

            if (destroyed)
                AudioSource.PlayClipAtPoint(clip, transform.position, targetVolume);
            else
                soundSource.PlayOneShot(clip, targetVolume);
        }
    }
}
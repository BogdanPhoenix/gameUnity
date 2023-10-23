using UnityEngine;

namespace Audio
{
    public class SounderController : MonoBehaviour
    {
        private const string TagPlayer = "Player";
        
        [Range(0, 1)] public float maxVolume;

        [Range(0, 1)] public float minVolume;

        public float maxDistance = 10f;
        private AudioSource AudioSource;
        private GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag(TagPlayer);
            AudioSource = GetComponent<AudioSource>();
            AudioSource.minDistance = 0;
            AudioSource.maxDistance = maxDistance;
        }

        private void Update()
        {
            if (player == null) return;

            var distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            var distanceRatio = Mathf.Clamp01(distanceToPlayer / maxDistance);
            var targetVolume = Mathf.Lerp(minVolume, maxVolume, 1f - distanceRatio);

            AudioSource.volume = targetVolume;
        }
    }
}
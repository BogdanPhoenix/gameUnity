using Enum;
using UnityEngine;

public class PowerUpElement : MonoBehaviour
{
    private static readonly float volume = 1f;
    
    public AudioClip clip;
    public PowerUpType type;
    public float invincibilityTime;
    
    [Range(1, 100)]
    public int Weight = 50;

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

    public void ActivateSound()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}

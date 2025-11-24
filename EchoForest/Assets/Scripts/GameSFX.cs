using UnityEngine;

public class GameSFX : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip shootClip;       
    public AudioClip meleeClip;        
    public AudioClip pickupTotemClip;  
    public AudioClip placeTotemClip;          
    public AudioClip enemyDeathClip;   

    void Awake()
    {
        if (sfxSource == null)
            sfxSource = GetComponent<AudioSource>();
    }

    public void PlayShoot()
    {
        if (shootClip != null)
            sfxSource.PlayOneShot(shootClip);
    }

    public void PlayMelee()
    {
        if (meleeClip != null)
            sfxSource.PlayOneShot(meleeClip);
    }

    public void PlayPickupTotem()
    {
        if (pickupTotemClip != null)
            sfxSource.PlayOneShot(pickupTotemClip);
    }

    public void PlayPlaceTotem()
    {
        if (placeTotemClip != null)
            sfxSource.PlayOneShot(placeTotemClip);
    }

    public void PlayEnemyDeath()
    {
        if (enemyDeathClip != null)
            sfxSource.PlayOneShot(enemyDeathClip);
    }
}

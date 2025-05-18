using UnityEngine;

public class PlayAttackAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip roarClip;

    private void PlayAttackingAudio()
    {
        audioSource.PlayOneShot(attackClip);
    }
    
    private void PlayRoaringAudio()
    {
        audioSource.PlayOneShot(roarClip);
    }
}

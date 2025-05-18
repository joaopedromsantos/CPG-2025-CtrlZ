using UnityEngine;

public class PlayJumpAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jumpAnimationStart;
    public AudioClip jumpAnimationEnd;
    public AudioClip doubleJumpAnimationStart;

    private void PlayAudio()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController.isGrounded)
        {
            audioSource.PlayOneShot(jumpAnimationStart);
            return;
        }

        if (playerController.isJumping)
        {
            audioSource.PlayOneShot(doubleJumpAnimationStart);
            return;
        }
        
        audioSource.PlayOneShot(jumpAnimationEnd);
    }
}

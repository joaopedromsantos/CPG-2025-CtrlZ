using UnityEngine;

public class PlayWalkAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private void PlayAudio()
    {
        audioSource.PlayOneShot(
            audioClips[Random.Range(0, audioClips.Length)]
        );
    }
}
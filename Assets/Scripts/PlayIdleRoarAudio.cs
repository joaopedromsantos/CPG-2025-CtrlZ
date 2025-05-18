using UnityEngine;
using System.Collections;

public class PlayIdleRoarAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip roarClip;
    public float cooldownTime = 6f;
    private double? lastRoarTime = null;

    public float minInterval = 4f;
    public float maxInterval = 8f;

    public void StartRoaring()
    {
        if (audioSource.isPlaying)
        {
            lastRoarTime = Time.timeAsDouble;
            return;
        }

        if (lastRoarTime.HasValue && !(Time.timeAsDouble - lastRoarTime.Value >= cooldownTime))
        {
            return;
        }
        
        PlayIdleRoaringAudio();
        cooldownTime = Random.Range(minInterval, maxInterval);
    }

    private void PlayIdleRoaringAudio()
    {
        audioSource.PlayOneShot(roarClip);
    }
}
using UnityEngine;

public class EnemySwordSfx : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip swingClip;
    [SerializeField, Range(0f, 1f)] private float volume = 0.8f;
    [SerializeField, Range(0.9f, 1.1f)] private float pitchMin = 0.95f;
    [SerializeField, Range(0.9f, 1.1f)] private float pitchMax = 1.05f;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlaySwing()
    {
        if (swingClip == null || audioSource == null) return;

        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.PlayOneShot(swingClip,volume);
    }
}
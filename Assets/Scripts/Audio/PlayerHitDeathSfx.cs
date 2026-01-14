using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerHitDeathSfx : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioClip[] hitClips;
    [SerializeField] private AudioClip[] deathClips;

    [Header("Tuning")]
    [SerializeField, Range(0f, 1f)] private float hitVolume = 0.9f;
    [SerializeField, Range(0f, 1f)] private float deathVolume = 1f;

    private AudioSource _audioSource;
    private bool _playedDeath;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(BindWhenReady());
    }

    private IEnumerator BindWhenReady()
    {
        while (Player.Instance == null)
            yield return null;

        // дебаг: щоб ти 100% бачив що підписались
        Debug.Log("[PlayerHitDeathSfx] Bound to Player events");

        Player.Instance.OnFlashBlink += OnHit;
        Player.Instance.OnPlayerDeath += OnDeath;
    }

    private void OnDestroy()
    {
        if (Player.Instance == null) return;
        Player.Instance.OnFlashBlink -= OnHit;
        Player.Instance.OnPlayerDeath -= OnDeath;
    }

    private void OnHit(object sender, EventArgs e)
    {
        // Debug.Log("[PlayerHitDeathSfx] HIT event");
        PlayRandom(hitClips, hitVolume);
    }

    private void OnDeath(object sender, EventArgs e)
    {
        // Debug.Log("[PlayerHitDeathSfx] DEATH event");
        if (_playedDeath) return;
        _playedDeath = true;
        PlayRandom(deathClips, deathVolume);
    }

    private void PlayRandom(AudioClip[] clips, float volume)
    {
        if (clips == null || clips.Length == 0) return;

        var clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        if (clip == null) return;

        _audioSource.PlayOneShot(clip, volume);
    }
}
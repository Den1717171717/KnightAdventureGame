using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyHitDeathSfx : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyEntity enemyEntity;

    [Header("Clips")]
    [SerializeField] private AudioClip[] hitClips;
    [SerializeField] private AudioClip[] deathClips;

    [Header("Tuning")]
    [SerializeField, Range(0f, 1f)] private float hitVolume = 0.9f;
    [SerializeField, Range(0f, 1f)] private float deathVolume = 1f;
    [SerializeField, Range(0.8f, 1.2f)] private float pitchMin = 0.95f;
    [SerializeField, Range(0.8f, 1.2f)] private float pitchMax = 1.05f;

    private AudioSource _audioSource;
    private bool _playedDeath;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (enemyEntity == null) enemyEntity = GetComponentInChildren<EnemyEntity>();
    }

    private void OnEnable()
    {
        if (enemyEntity == null) return;
        enemyEntity.OnTakeHit += OnTakeHit;
        enemyEntity.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        if (enemyEntity == null) return;
        enemyEntity.OnTakeHit -= OnTakeHit;
        enemyEntity.OnDeath -= OnDeath;
    }

    private void OnTakeHit(object sender, EventArgs e)
    {
        PlayRandom(hitClips, hitVolume);
    }

    private void OnDeath(object sender, EventArgs e)
    {
        if (_playedDeath) return;
        _playedDeath = true;
        PlayRandom(deathClips, deathVolume);
    }

    private void PlayRandom(AudioClip[] clips, float volume)
    {
        if (_audioSource == null || clips == null || clips.Length == 0) return;

        var clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        if (clip == null) return;

        _audioSource.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
        _audioSource.PlayOneShot(clip, volume);
    }
}
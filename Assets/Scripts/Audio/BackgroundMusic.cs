using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic _instance;
    private AudioSource _source;

    [SerializeField] private AudioClip musicClip;
    [SerializeField, Range(0f, 1f)] private float volume = 0.5f;
    [SerializeField] private bool dontDestroyOnLoad = true;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);

        _source = GetComponent<AudioSource>();
        if (_source == null) _source = gameObject.AddComponent<AudioSource>();

        _source.clip = musicClip;
        _source.loop = true;
        _source.spatialBlend = 0f;
        _source.volume = volume;

        if (!_source.isPlaying && musicClip != null)
            _source.Play();
    }
}

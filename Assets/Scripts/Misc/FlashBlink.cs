using UnityEngine;

public class FlashBlink : MonoBehaviour
{
    [SerializeField] private MonoBehaviour damagableObject;
    [SerializeField] private Material blinkMaterial;
    [SerializeField] private float blinkDuration = 0.2f;

    private float _blinkTimer;
    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;
    private bool _isBlinking;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
        _isBlinking = false;
    }

    private void Start()
    {
        if (damagableObject is Player player)
        {
            player.OnFlashBlink += OnFlashBlink;
        }
    }

    private void OnFlashBlink(object sender, System.EventArgs e)
    {
        StartBlinking();
    }

    private void Update()
    {
        if (!_isBlinking) return;

        _blinkTimer -= Time.deltaTime;
        if (_blinkTimer <= 0f)
        {
            StopBlinking();
        }
    }

    private void StartBlinking()
    {
        _isBlinking = true;
        _blinkTimer = blinkDuration;
        _spriteRenderer.material = blinkMaterial;
    }

    public void StopBlinking()
    {
        _isBlinking = false;
        _spriteRenderer.material = _defaultMaterial;
    }

    private void OnDestroy()
    {
        if (damagableObject is Player player)
        {
            player.OnFlashBlink -= OnFlashBlink;
        }
    }
}
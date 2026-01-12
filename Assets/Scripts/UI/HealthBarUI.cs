using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates a UI Image fill based on the player's health.
/// Setup:
/// - Create a UI Image (bar fill) with Image Type = Filled.
/// - Assign it to fillImage.
/// </summary>
public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private Player _player;

    private void Start()
    {
        _player = Player.Instance;
        if (_player != null)
        {
            _player.OnHealthChanged += Player_OnHealthChanged;
            UpdateFill(_player.GetCurrentHealth(), _player.GetMaxHealth());
        }
    }

    private void OnDestroy()
    {
        if (_player != null)
        {
            _player.OnHealthChanged -= Player_OnHealthChanged;
        }
    }

    private void Player_OnHealthChanged(object sender, Player.HealthChangedEventArgs e)
    {
        UpdateFill(e.CurrentHealth, e.MaxHealth);
    }

    private void UpdateFill(int current, int max)
    {
        if (fillImage == null) return;
        float t = max <= 0 ? 0f : (float)current / max;
        fillImage.fillAmount = Mathf.Clamp01(t);
    }
}

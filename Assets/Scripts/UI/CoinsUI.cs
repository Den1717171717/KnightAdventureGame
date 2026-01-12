using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays current coin amount in a UI Text.
/// Assign a Text component to coinText.
/// </summary>
public class CoinsUI : MonoBehaviour
{
    [SerializeField] private Text coinText;

    private void Start()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnCoinsChanged += CurrencyManager_OnCoinsChanged;
            SetText(CurrencyManager.Instance.Coins);
        }
    }

    private void OnDestroy()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnCoinsChanged -= CurrencyManager_OnCoinsChanged;
        }
    }

    private void CurrencyManager_OnCoinsChanged(object sender, int newTotal)
    {
        SetText(newTotal);
    }

    private void SetText(int value)
    {
        if (coinText == null) return;
        coinText.text = value.ToString();
    }
}

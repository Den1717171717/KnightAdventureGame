using System;
using UnityEngine;

/// <summary>
/// Keeps track of coins for the current run.
/// Put this on a GameObject in the scene (e.g. "GameManager").
/// </summary>
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }
    
    public event EventHandler<int> OnCoinsChanged;

    [SerializeField] private int startingCoins = 0;

    private int _coins;

    public int Coins => _coins;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _coins = startingCoins;
    }

    private void Start()
    {
        OnCoinsChanged?.Invoke(this, _coins);
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;
        _coins += amount;
        OnCoinsChanged?.Invoke(this, _coins);
    }

    public void ResetCoins()
    {
        _coins = startingCoins;
        OnCoinsChanged?.Invoke(this, _coins);
    }
}

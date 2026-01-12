using UnityEngine;

public class OutOfBoundsRestart : MonoBehaviour
{
    [SerializeField] private Collider2D playerBodyCollider;
    private bool _locked;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_locked) return;
        if (playerBodyCollider == null || other != playerBodyCollider) return;

        _locked = true;

        var reset = Object.FindAnyObjectByType<LevelResetManager>();
        if (reset != null)
            reset.ResetLevel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerBodyCollider != null && other == playerBodyCollider)
            _locked = false;
    }
}
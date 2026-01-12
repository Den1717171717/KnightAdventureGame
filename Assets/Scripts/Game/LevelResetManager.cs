using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelResetManager : MonoBehaviour
{
    [Header("Assign in Inspector")] [SerializeField]
    private GameObject levelRootPrefab;
    [SerializeField] private Transform levelRootParent;    
    [SerializeField] private Transform playerSpawnPoint;  

    [Header("Tuning")]
    [SerializeField] private float resetCooldown = 0.25f;
    [SerializeField] private string levelRootName = "LevelRoot";

    private GameObject _currentLevelRoot;
    private bool _isResetting;

    private void Awake()
    {
        _currentLevelRoot = FindLevelRootInScene();
    }

    public void ResetLevel()
    {
        if (_isResetting) return;

        if (levelRootPrefab == null)
        {
            Debug.LogError("LevelResetManager: Level Root Prefab is NOT assigned (drag prefab from Project).");
            return;
        }

        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        _isResetting = true;

        if (_currentLevelRoot == null)
            _currentLevelRoot = FindLevelRootInScene();

        if (_currentLevelRoot != null)
            Destroy(_currentLevelRoot);

        yield return null;

        _currentLevelRoot = (levelRootParent != null)
            ? Instantiate(levelRootPrefab, levelRootParent)
            : Instantiate(levelRootPrefab);

        _currentLevelRoot.name = levelRootName;

        var player = Object.FindAnyObjectByType<Player>();
        if (player != null && playerSpawnPoint != null)
        {
            player.transform.position = playerSpawnPoint.position;

            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }

        yield return new WaitForSeconds(resetCooldown);
        _isResetting = false;
    }

    private GameObject FindLevelRootInScene()
    {
        var exact = GameObject.Find(levelRootName);
        if (exact != null) return exact;

        var roots = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var go in roots)
        {
            if (go == null) continue;
            if (go.name == levelRootName) return go;
            if (go.name.StartsWith(levelRootName)) return go;
        }

        return null;
    }
}

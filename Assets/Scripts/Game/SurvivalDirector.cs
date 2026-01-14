using UnityEngine;

public class SurvivalDirector : MonoBehaviour
{
    [Header("Round")]
    [SerializeField] private float roundDurationSeconds = 5f * 60f; // 5 minutes

    [Header("Spawning pacing")]
    [SerializeField] private Vector2 normalInterval = new Vector2(5f, 20f);
    [SerializeField] private Vector2 lastMinuteInterval = new Vector2(5f, 15f);

    [Header("Spawners growth")]
    [SerializeField] private int startSpawners = 1;          // start
    [SerializeField] private int lastMinuteSpawners = 10;    // last minute

    [Header("References")]
    [SerializeField] private SpawnerManager spawnerManager;

    public float TimeLeft { get; private set; }
    public float Elapsed => roundDurationSeconds - TimeLeft;

    private int _lastWholeMinuteApplied = -1;

    private void Awake()
    {
        if (spawnerManager == null)
            spawnerManager = FindFirstObjectByType<SpawnerManager>();
    }

    private void Start()
    {
        TimeLeft = roundDurationSeconds;
        
        spawnerManager.SetActiveSpawners(startSpawners);
        
        spawnerManager.SetSpawnIntervalRange(normalInterval);
    }

    private void Update()
    {
        if (TimeLeft <= 0f) return;

        TimeLeft -= Time.deltaTime;
        if (TimeLeft < 0f) TimeLeft = 0f;

        ApplyDifficulty();
        CheckWin();
    }

    private void ApplyDifficulty()
    {
        float minutesElapsed = Elapsed / 60f;
        int wholeMinutesElapsed = Mathf.FloorToInt(minutesElapsed);

        bool isLastMinute = TimeLeft <= 60f;
        
        spawnerManager.SetSpawnIntervalRange(isLastMinute ? lastMinuteInterval : normalInterval);
        
        if (isLastMinute)
        {
            spawnerManager.SetActiveSpawners(lastMinuteSpawners);
            return;
        }

        if (wholeMinutesElapsed != _lastWholeMinuteApplied)
        {
            _lastWholeMinuteApplied = wholeMinutesElapsed;

            int target = startSpawners + wholeMinutesElapsed;
            spawnerManager.SetActiveSpawners(target);
        }
    }

    private void CheckWin()
    {
        if (TimeLeft <= 0f)
        {
            Debug.Log("SURVIVED! You win!");
        }
    }
}

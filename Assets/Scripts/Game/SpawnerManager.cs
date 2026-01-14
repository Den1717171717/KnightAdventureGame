using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> spawners = new();

    private Vector2 _intervalRange = new Vector2(5f, 20f);

    private void Awake()
    {
        if (spawners == null || spawners.Count == 0)
        {
            spawners = new List<EnemySpawner>(GetComponentsInChildren<EnemySpawner>(true));
        }
    }

    public void SetSpawnIntervalRange(Vector2 range)
    {
        _intervalRange = range;

        foreach (var s in spawners)
        {
            if (s == null) continue;
            s.SetIntervalRange(_intervalRange);
        }
    }

    public void SetActiveSpawners(int count)
    {
        count = Mathf.Clamp(count, 0, spawners.Count);

        for (int i = 0; i < spawners.Count; i++)
        {
            bool active = i < count;
            if (spawners[i] != null)
                spawners[i].SetSpawningEnabled(active);
        }
    }
}
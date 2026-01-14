using System;
using System.Collections;
using UnityEngine;

public class EnemyCorpseFadeOut : MonoBehaviour
{
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private float despawnDelay = 4f;
    [SerializeField] private float fadeDuration = 1f;

    private SpriteRenderer[] _renderers;
    private bool _started;

    private void Awake()
    {
        if (enemyEntity == null)
            enemyEntity = GetComponentInChildren<EnemyEntity>();

        _renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (enemyEntity != null)
            enemyEntity.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        if (enemyEntity != null)
            enemyEntity.OnDeath -= OnDeath;
    }

    private void OnDeath(object sender, EventArgs e)
    {
        if (_started) return;
        _started = true;
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(despawnDelay);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = 1f - (timer / fadeDuration);

            foreach (var sr in _renderers)
            {
                if (sr == null) continue;
                Color c = sr.color;
                c.a = t;
                sr.color = c;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
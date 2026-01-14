using TMPro;
using UnityEngine;

public class SurvivalTimerUI : MonoBehaviour
{
    [SerializeField] private SurvivalDirector survivalDirector;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (survivalDirector == null)
            survivalDirector = FindFirstObjectByType<SurvivalDirector>();

        if (timerText == null)
            timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (survivalDirector == null) return;

        UpdateTimer(survivalDirector.TimeLeft);
    }

    private void UpdateTimer(float timeLeft)
    {
        timeLeft = Mathf.Max(0f, timeLeft);

        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
        
        if (timeLeft <= 60f)
            timerText.color = Color.red;
        else
            timerText.color = Color.white;
        
        if (timeLeft <= 10f)
        {
            float scale = 1f + Mathf.Sin(Time.time * 6f) * 0.1f;
            timerText.transform.localScale = Vector3.one * scale;
        }
        else
        {
            timerText.transform.localScale = Vector3.one;
        }


    }
}
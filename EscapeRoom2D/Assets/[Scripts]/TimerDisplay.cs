using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timerText.SetText("Time: " + Mathf.RoundToInt(GameManager.instance.GetElapsedTime()).ToString());
    }
}

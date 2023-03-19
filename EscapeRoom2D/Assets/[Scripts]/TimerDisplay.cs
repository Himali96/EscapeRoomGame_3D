using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public int levelIndex;
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        GameManager.instance.StartLevelTimer(levelIndex); // add 1 to the level index to account for the main menu scene
    }

    private void Update()
    {
        float timeElapsed = Time.time - GameManager.instance.GetLevelTime(levelIndex);
        timerText.SetText("Level " + (levelIndex) + " Time: " + Mathf.RoundToInt(timeElapsed));
    }

    private void OnDestroy()
    {
        GameManager.instance.EndLevelTimer(levelIndex);
    }
}
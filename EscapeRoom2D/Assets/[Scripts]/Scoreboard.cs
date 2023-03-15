using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.SetText("Level 1 Time: " + Mathf.RoundToInt(GameManager.instance.GetLevelTime(1)).ToString() + "s\n" +
                           "Level 2 Time: " + Mathf.RoundToInt(GameManager.instance.GetLevelTime(2)).ToString() + "s\n" 
                           );
    }
}

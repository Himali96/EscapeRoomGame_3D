using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.SetText( Mathf.RoundToInt(GameManager.instance.GetElapsedTime()).ToString() + "Sec");
    }
}

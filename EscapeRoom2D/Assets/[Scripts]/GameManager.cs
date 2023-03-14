using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool isGameStarted = false;
    private float startTime = 0f;
    private float endTime = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        startTime = Time.time;
    }

    public void EndGame()
    {
        isGameStarted = false;
        endTime = Time.time;
        SceneManager.LoadScene("Scoreboard");
    }

    public float GetElapsedTime()
    {
        if (isGameStarted)
        {
            return Time.time - startTime;
        }
        else
        {
            return endTime - startTime;
        }
    }
}

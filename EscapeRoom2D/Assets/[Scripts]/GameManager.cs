using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float[] levelTimes;

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

        levelTimes = new float[SceneManager.sceneCountInBuildSettings]; // -1 to exclude the starting scene
    }

    public void StartLevelTimer(int levelIndex)
    {
        levelTimes[levelIndex] = Time.time;
    }

    public void EndLevelTimer(int levelIndex)
    {
        levelTimes[levelIndex] = Time.time - levelTimes[levelIndex];
    }

    public float GetLevelTime(int levelIndex)
    {
        return levelTimes[levelIndex];
    }

    public float GetTotalTime()
    {
        float totalTime = 0f;
        for (int i = 0; i < levelTimes.Length; i++)
        {
            totalTime += levelTimes[i];
        }
        return totalTime;
    }
}

using UnityEngine;

// Manager which task have been completed

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public GameObject levelCompletePanel;

    public bool[] tasksCompleted;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (IsAllTaskCompleted())
        {
            levelCompletePanel.gameObject.SetActive(true);
            enabled = false;
        }
    }

    public bool IsAllTaskCompleted()
    {
        foreach (bool task in tasksCompleted)
        {
            if (task == false) // This task is still missing to complete
                return false;
        }

        Debug.Log("level complete");
        // if code arrive here, all task have been completed
        return true;
    }
}
using UnityEngine;

// Manager which task have been completed

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public bool[] tasksCompleted;

    void Awake()
    {
        Instance = this;
    }

    public bool IsAllTaskCompleted()
    {
        foreach (bool task in tasksCompleted)
        {
            if (task == false) // This task is still missing to complete
                return false;
        }

        // if code arrive here, all task have been completed
        return true;
    }
}

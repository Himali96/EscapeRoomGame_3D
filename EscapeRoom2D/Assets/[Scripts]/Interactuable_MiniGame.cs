using UnityEngine;

public class Interactuable_MiniGame : MonoBehaviour, IInteractuableObject
{

    bool completed = false; 

    [SerializeField] int taskID = -1;

    public void OnClick()
    {
        if (completed) return;

        MiniGameLoader.Instance.onMiniGameClose += onCloseLevel;
        MiniGameLoader.Instance.LoadLevel(taskID);
    }

    void onCloseLevel(bool _wasCompleted)
    {
        completed = _wasCompleted;

        LevelManager.Instance.tasksCompleted[taskID] = _wasCompleted;

        MiniGameLoader.Instance.onMiniGameClose -= onCloseLevel;
    }
}

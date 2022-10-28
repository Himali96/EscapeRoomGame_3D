using UnityEngine;

public class Interactuable_MiniGame : MonoBehaviour, IInteractuableObject
{

    bool completed = false; 

    [SerializeField] string levelToLoad = "";
    [SerializeField] int taskID = -1;

    public void OnClick()
    {
        if (completed) return;

        MiniGameLoader.Instance.onMiniGameClose += onCloseLevel;
        MiniGameLoader.Instance.LoadLevel(levelToLoad);
    }

    void onCloseLevel(bool _wasCompleted)
    {
        completed = _wasCompleted;

        LevelManager.Instance.tasksCompleted[taskID] = _wasCompleted;

        MiniGameLoader.Instance.onMiniGameClose -= onCloseLevel;
    }
}

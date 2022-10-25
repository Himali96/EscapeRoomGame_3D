using UnityEngine;

public class Interactuable_MiniGame : MonoBehaviour, IInteractuableObject
{

    bool completed = false; 

    [SerializeField] string levelToLoad = "";

    public void OnClick()
    {
        if (completed) return;

        MiniGameLoader.Instance.onMiniGameClose += onCloseLevel;
        MiniGameLoader.Instance.LoadLevel(levelToLoad);
    }

    void onCloseLevel(bool _wasCompleted)
    {
        completed = _wasCompleted;
        MiniGameLoader.Instance.onMiniGameClose -= onCloseLevel;
    }
}

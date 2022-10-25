using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class MiniGameLoader : MonoBehaviour
{

    public static MiniGameLoader Instance { get; private set; }

    string lastLevelName = "";

    public Action<bool> onRoomViewChange; // isVisible
    public Action onMiniGameOpen;
    public Action<bool> onMiniGameClose; // wasCompleted

    void Awake()
    {
        Instance = this;
    }

    public void LoadLevel(string _levelName)
    {
        lastLevelName = _levelName;
        SceneManager.LoadScene(_levelName, LoadSceneMode.Additive);
        onRoomViewChange?.Invoke(false);
        onMiniGameOpen?.Invoke();
    }

    public void UnLoadLastLevel(bool _wasCompleted)
    {
        SceneManager.UnloadSceneAsync(lastLevelName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        onMiniGameClose?.Invoke(_wasCompleted);
        onRoomViewChange?.Invoke(true);
    }

}

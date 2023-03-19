using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class MiniGameLoader : MonoBehaviour
{
    public static MiniGameLoader Instance { get; private set; }

    [SerializeField] GameObject[] roomGameObjectsToHide = null;
    [SerializeField] SubSceneMiniGame[] miniGames = null;

    public bool isMinigameDisplaying;

    int lastLevelIndex = 0;

    // Observable patterns
    public Action<bool> onRoomViewChange; // isVisible
    public Action onMiniGameOpen;
    public Action<bool> onMiniGameClose; // wasCompleted

    void Awake()
    {
        Instance = this;
    }

    public void LoadLevel(int _levelIndex)
    {
        lastLevelIndex = _levelIndex;

        foreach (GameObject go in roomGameObjectsToHide)
        {
            if (go)
                go.SetActive(false);
        }

        SubSceneMiniGame game = miniGames[_levelIndex];
        game.gameObject.SetActive(true);

        isMinigameDisplaying = true;

        onRoomViewChange?.Invoke(false);
        onMiniGameOpen?.Invoke();
    }

    public void UnLoadLastLevel(bool _wasCompleted)
    {
        SubSceneMiniGame game = miniGames[lastLevelIndex];
        game.gameObject.SetActive(false);

        foreach (GameObject go in roomGameObjectsToHide)
        {
            if (go)
                go.SetActive(true);
        }

        isMinigameDisplaying = false;

        onMiniGameClose?.Invoke(_wasCompleted);
        onRoomViewChange?.Invoke(true);
    }

    [Serializable]
    public class SubSceneMiniGame
    {
        public string name;
        public GameObject gameObject;
    }
}
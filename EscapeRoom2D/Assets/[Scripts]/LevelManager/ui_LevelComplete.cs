using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_LevelComplete : MonoBehaviour
{
    [SerializeField] String levelToLoad;

    public void BtnContinue()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
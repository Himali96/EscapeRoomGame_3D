using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadLevel()
    {
        if (PlayerPrefs.GetInt(sceneToLoad) == 1)
        {
            StartCoroutine(WaitToLoadLevel());
        }
    }

    public void Locklevel()
    {
        PlayerPrefs.SetInt("Level_2", 0);

        PlayerPrefs.SetInt("Level_3", 0);

        PlayerPrefs.SetInt("Level_4", 0);
    }

    IEnumerator WaitToLoadLevel()
    {
        yield return new WaitForSeconds(1f);

        // Scene Load
        SceneManager.LoadScene(sceneToLoad);
    }
}

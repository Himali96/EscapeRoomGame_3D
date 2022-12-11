using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.Burst.CompilerServices;

// To change the size of numbers, just update the 'numberButtons' array

public class mg_pressInOrder : MonoBehaviour
{
    public GameObject InOrderMGObject;
    LevelManager levelManager = null;
    [SerializeField] mg_pressInOrderButton[] numberButtons = null;

    int nextNumberShouldBePressed = 1;

    [SerializeField] Image progressImg = null;
    [SerializeField] Sprite correctImg = null, incorrectImg = null;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        SetUpNumbers();
        levelManager = GetComponent<LevelManager>();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("InOrder") && levelManager.tasksCompleted[0]) //&& levelManager.tasksCompleted[0])
            {
                InOrderMGObject.SetActive(true);
                //levelManager.tasksCompleted[1] = true;
            }
        }
    }

    public void InOrderExitButtonClicked()
    {
        InOrderMGObject.SetActive(false);
    }

    void SetUpNumbers()
    {
        int[] numbersArray = new int[numberButtons.Length];

        // Fill answer
        for (int i = 0; i < numberButtons.Length; i++)
        {
            numbersArray[i] = i+1;
        }

        // Random it
        numbersArray.Shuffle();

        // Fill numbers in the table
        for (var i = 0; i < numberButtons.Length; i++)
        {
            numberButtons[i].SetNumber(this, numbersArray[i]);
        }
    }

    public void NumberPressed(int _numberPressed, int _index)
    {
        DOTween.KillAll();
        progressImg.color = new Color(progressImg.color.r, progressImg.color.g, progressImg.color.b, 1f); ;
        progressImg.color = Color.green;
        if (nextNumberShouldBePressed == _numberPressed)
        {
            // Correct
            nextNumberShouldBePressed++;
            ChangeSprite(correctImg);
            numberButtons[_index].GetComponent<Image>().color = new Color32(100,255,0,200);

            if (nextNumberShouldBePressed == numberButtons.Length + 1) // because number begin from 1
            {
                InOrderMGObject.SetActive(false);
                Level_1_LevelFlowManager._instance.isTask2Completed = true;
            }

        }
        else
        {
             // Error
            nextNumberShouldBePressed = 1;
            ChangeSprite(incorrectImg);
            foreach (var num in numberButtons)
            {
                num.GetComponent<Image>().color = Color.white;
            }
        }
    }

    void ChangeSprite(Sprite s)
    {
        progressImg.sprite = s;
        progressImg.DOFade(0.0f, 2.0f);
    }
}

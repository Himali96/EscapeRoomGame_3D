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
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("InOrder") && !levelManager.tasksCompleted[1]) //&& levelManager.tasksCompleted[0])
            {
                InOrderMGObject.SetActive(true);
                levelManager.tasksCompleted[1] = true;
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

    public void NumberPressed(int _numberPressed)
    {
        DOTween.KillAll();
        progressImg.color = new Color(progressImg.color.r, progressImg.color.g, progressImg.color.b, 1f); ;

        if (nextNumberShouldBePressed == _numberPressed)
        {
            // Correct
            nextNumberShouldBePressed++;
            ChangeSprite(correctImg);

            //ChnageColorProgressImg(Color.green);
            //transform.GetChild(serialNumber).

            if (nextNumberShouldBePressed == numberButtons.Length + 1) // because number begin from 1
            {
                //print("Level completed!");
                InOrderMGObject.SetActive(false);
                //MiniGameLoader.Instance.UnLoadLastLevel(true);
            }

        }

        else
        {
             // Error
            nextNumberShouldBePressed = 1;
            ChangeSprite(incorrectImg);
            //ChnageColorProgressImg(Color.red);
        }
    }

    void ChangeSprite(Sprite s)
    {
        progressImg.sprite = s;
        progressImg.DOFade(0.0f, 2.0f);
    }

    void ChnageColorProgressImg(Color _color)
    {
        progressImg.color = _color;
        progressImg.DOColor(Color.white, 0.3f).SetDelay(2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class mg_pressInOrder : MonoBehaviour
{
    [SerializeField] mg_pressInOrderButton[] numberButtons = null;

    int nextNumberShouldBePressed = 1;

    [SerializeField] Image progressImg = null;


    void Start()
    {
        SetUpNumbers();
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
        if (nextNumberShouldBePressed == _numberPressed)
        {
            // Correct
            nextNumberShouldBePressed++;
            ChnageColorProgressImg(Color.green);

            if (nextNumberShouldBePressed == numberButtons.Length+1) // because number begin from 1
            {
                print("Level completed!");
                MiniGameLoader.Instance.UnLoadLastLevel(true);
            }

        }
        else
        {
             // Error
            nextNumberShouldBePressed = 1;
            ChnageColorProgressImg(Color.red);
        }
    }

    void ChnageColorProgressImg(Color _color)
    {
        progressImg.color = _color;
        progressImg.DOColor(Color.white, 0.3f).SetDelay(2f);

    }
}

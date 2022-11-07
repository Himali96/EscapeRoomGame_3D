using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

// To change the size of numbers, just update the 'numberButtons' array

public class mg_pressInOrder : MonoBehaviour
{
    [SerializeField] mg_pressInOrderButton[] numberButtons = null;

    int nextNumberShouldBePressed = 1;

    [SerializeField] Image progressImg = null;
    [SerializeField] Sprite correctImg = null, incorrectImg = null;


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
                print("Level completed!");
                MiniGameLoader.Instance.UnLoadLastLevel(true);
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

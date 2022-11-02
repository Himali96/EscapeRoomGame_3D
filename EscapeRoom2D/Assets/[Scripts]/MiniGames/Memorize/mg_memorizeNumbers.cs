using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// To change the numbers to memorize just update the 'numberToMemorize' array

public class mg_memorizeNumbers : MonoBehaviour
{
    [SerializeField] float timeDisplayingNumbers = 4f;

    [SerializeField] mg_memorizeNumbersButton[] numberButtons = null;
    [SerializeField] CanvasGroup numbersToMemorizeCanvas = null;
    [SerializeField] TextMeshProUGUI[] numberToMemorizeTxt = null;

    int[] numbersToRemember;
    int numbersToRememberIndex = 0;

    [SerializeField] Image progressImg = null;

    Tween tween_colorToWhite = null;

    void Start()
    {
        SetUpNumbers();
        Invoke(nameof(GenerateRandomsToMemorize), 1f);
    }

    void SetUpNumbers()
    {
        // Fill numbers in the table
        for (var i = 0; i < numberButtons.Length; i++)
        {
            numberButtons[i].SetNumber(this, i+1);
            numberButtons[i].SetInteractuable(false);
        }
    }

    public void GenerateRandomsToMemorize()
    {
        numbersToRemember ??= new int[numberToMemorizeTxt.Length];

        List<int> numbers = new List<int>(numberButtons.Length);
        for (int i = 0; i < numberButtons.Length; i++)
        {
            numbers.Add(i + 1);
        }

        for (int i = 0; i < numbersToRemember.Length; i++)
        {
            int numberIndex = Random.Range(0, numbers.Count);
            numbersToRemember[i] = numbers[numberIndex];
            numberToMemorizeTxt[i].SetText(numbers[numberIndex].ToString());

            numbers.RemoveAt(numberIndex);
        }

        numbersToMemorizeCanvas.alpha = 1f;
        numbersToMemorizeCanvas.gameObject.SetActive(true);

        numbersToRememberIndex = 0;

        for (var i = 0; i < numberButtons.Length; i++)
        {
            numberButtons[i].SetInteractuable(false);
        }

        Invoke(nameof(HideNumbers), timeDisplayingNumbers);
    }

    void HideNumbers()
    {
        numbersToMemorizeCanvas.DOFade(0f, 0.5f).OnComplete( () =>
        {
            numbersToMemorizeCanvas.gameObject.SetActive(false);
            for (var i = 0; i < numberButtons.Length; i++)
            {
                numberButtons[i].SetInteractuable(true);
            }
        } );
    }

    public void NumberPressed(int number)
    {
        if (numbersToRemember[numbersToRememberIndex] == number)
        {
            ChnageColorProgressImg(Color.green);

            numbersToRememberIndex++;

            if (numbersToRememberIndex == numbersToRemember.Length) // because number begin from 1
            {
                print("Level completed!");
                MiniGameLoader.Instance.UnLoadLastLevel(true);
            }
        }
        else // Incorrect
        {
            ChnageColorProgressImg(Color.red);

            Invoke(nameof(GenerateRandomsToMemorize), 2f);
        }
    }

    void ChnageColorProgressImg(Color _color)
    {
        progressImg.color = _color;

        if (tween_colorToWhite.IsActive() && tween_colorToWhite.IsPlaying())
        {
            tween_colorToWhite.Kill();
        }

        tween_colorToWhite= progressImg.DOColor(Color.white, 0.3f).SetDelay(2f);
    }

}

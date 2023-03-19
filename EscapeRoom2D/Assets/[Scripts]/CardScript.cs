using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Sprite backImage;
    public Sprite[] cardImages;

    private int imageIndex;
    private bool isFlipped = false;
    private static CardScript firstCard = null;
    private static CardScript secondCard = null;
    // private static int score = 0;
    //  private static Text scoreText;
    private static List<int> usedIndexes = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        // scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        GenerateImageIndex();
        GetComponent<SpriteRenderer>().sprite = backImage;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        if (!isFlipped)
        {
            FlipCard();
            if (firstCard == null)
            {
                firstCard = this;
            }
            else
            {
                secondCard = this;
                CheckMatch();
            }
        }
    }

    void FlipCard()
    {
        isFlipped = !isFlipped;
        if (isFlipped)
        {
            GetComponent<SpriteRenderer>().sprite = cardImages[imageIndex];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = backImage;
        }
    }

    void GenerateImageIndex()
    {
        bool indexUsed = true;
        while (indexUsed)
        {
            imageIndex = Random.Range(0, cardImages.Length);
            if (!usedIndexes.Contains(imageIndex))
            {
                usedIndexes.Add(imageIndex);
                indexUsed = false;
            }
        }
    }

    void CheckMatch()
    {
        if (firstCard.imageIndex == secondCard.imageIndex)
        {
            // score += 10;
            // scoreText.text = "Score: " + score;
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);
            firstCard = null;
            secondCard = null;
        }
        else
        {
            StartCoroutine(FlipCardsBack());
        }
    }

    IEnumerator FlipCardsBack()
    {
        yield return new WaitForSeconds(1);
        firstCard.FlipCard();
        secondCard.FlipCard();
        firstCard = null;
        secondCard = null;
    }
}
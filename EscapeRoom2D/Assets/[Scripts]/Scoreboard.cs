using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public Sprite starSprite; // the star sprite
    public GameObject[] starContainers; // an array of star containers
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();

        // Get the time taken for each level
        float level1Time = GameManager.instance.GetLevelTime(1);
        float level2Time = GameManager.instance.GetLevelTime(2);

        // Display the time taken for each level
        timerText.SetText("Level 1 Time: " + Mathf.RoundToInt(level1Time).ToString() + "s\n\n" +
                           "Level 2 Time: " + Mathf.RoundToInt(level2Time).ToString() + "s");

        // Determine the number of stars for each level based on the time taken
        int level1Stars = CalculateStars(level1Time);
        int level2Stars = CalculateStars(level2Time);

        // Display the appropriate number of stars for each level
        DisplayStars(starContainers[0], level1Stars);
        DisplayStars2(starContainers[1], level2Stars);
    }

    // Calculates the number of stars based on the time taken
    private int CalculateStars(float timeTaken)
    {
        if (timeTaken <= 30f)
        {
            return 3;
        }
        else if (timeTaken <= 40f)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    // Displays the appropriate number of stars
    private void DisplayStars(GameObject starContainer, int numStars)
    {
        for (int i = 0; i < numStars; i++)
        {
            // Create a new sprite object for each star and set its sprite to the star sprite
            GameObject starObject = new GameObject("Star");
            SpriteRenderer renderer = starObject.AddComponent<SpriteRenderer>();
            renderer.sprite = starSprite;

            // Set the position of the star object based on its index in the container
            starObject.transform.SetParent(starContainer.transform);
            starObject.transform.localPosition = new Vector3(i * 0.75f, -0.65f, 0);
            starObject.transform.localScale = new Vector3(0.03f, 0.03f, 1f);
        }
    }

    private void DisplayStars2(GameObject starContainer, int numStars)
    {
        for (int i = 0; i < numStars; i++)
        {
            // Create a new sprite object for each star and set its sprite to the star sprite
            GameObject starObject = new GameObject("Star");
            SpriteRenderer renderer = starObject.AddComponent<SpriteRenderer>();
            renderer.sprite = starSprite;

            // Set the position of the star object based on its index in the container
            starObject.transform.SetParent(starContainer.transform);
            starObject.transform.localPosition = new Vector3(i * 0.75f, -2.0f, 0);
            starObject.transform.localScale = new Vector3(0.03f, 0.03f, 1f);
        }
    }
}
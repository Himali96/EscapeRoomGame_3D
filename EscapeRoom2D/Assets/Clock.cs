using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private Transform minuteHand, hourHand;

    [SerializeField]
    private GameObject winText;

    [SerializeField]
    private GameObject camera;

    private void Start()
    {
        //winText.SetActive(false);
    }

    private void OnMouseDown()
    {
        minuteHand.Rotate(Vector3.back, 30);
        hourHand.Rotate(Vector3.back, 2.5f);

        if ((Mathf.Round(minuteHand.rotation.eulerAngles.z * 2) / 2) == 30
            && (Mathf.Round(hourHand.rotation.eulerAngles.z * 2) / 2) == 212.5f)
        {
            MiniGameLoader.Instance.UnLoadLastLevel(true);
            //winText.SetActive(true);
        }
    }
}

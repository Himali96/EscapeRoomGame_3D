using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Level_1_LevelFlowManager : MonoBehaviour
{
    public static Level_1_LevelFlowManager _instance;

    // References
    public Transform wallClock, roomDoor, carpet;
    public GameObject clockHand, wallClockHand;
    public TextMeshPro txtInstructions;
    public AudioSource clickSound;

    public bool isClockHandFound, isCarpetMoved, isTask1Completed, isTask2Completed;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        txtInstructions.text = "Welcome to room 1!\nFind clues and solve puzzles to escape...";
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(carpet.localPosition.z);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.CompareTag("Carpet") && !isCarpetMoved)
            {
                clickSound.Play();
                carpet.DOLocalMoveZ(-3f, 1f);
                txtInstructions.text = "";
                isCarpetMoved = true;
            }

            if (hit.collider.CompareTag("Carpet") && isCarpetMoved)
            {
                clickSound.Play();
                txtInstructions.text = "";
                isCarpetMoved = false;
            }

            if (hit.collider.CompareTag("ClockHand") && !isClockHandFound)
            {
                clickSound.Play();
                clockHand.SetActive(false);
                wallClockHand.SetActive(true);
                wallClock.GetComponent<BoxCollider>().enabled = true;
                txtInstructions.text = "You got clock's missing hand.";
                isClockHandFound = true;
            }

            if (hit.collider.CompareTag("Door"))
            {
                clickSound.Play();
                if (!isTask2Completed) txtInstructions.text = "Fix panel first!";
                else txtInstructions.text = "";
            }

            if (hit.collider.CompareTag("InOrder"))
            {
                clickSound.Play();
                if (!isTask1Completed) txtInstructions.text = "Fix Clock first!";
                else txtInstructions.text = "";
            }

            if (hit.collider.CompareTag("Menu"))
            {
                clickSound.Play();
                SceneManager.LoadSceneAsync("Level_2");
            }
        }

        if (isTask1Completed && isTask2Completed)
        {
            PlayerPrefs.SetInt("Level_1", 1);

            roomDoor.DOLocalRotate(new Vector3(0, 0, 0), 3f);
        }
    }
}
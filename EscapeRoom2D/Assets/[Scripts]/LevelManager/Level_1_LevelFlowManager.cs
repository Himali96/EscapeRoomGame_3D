using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Level_1_LevelFlowManager : MonoBehaviour
{
    public Transform wallClock, roomDoor, carpet;
    public GameObject clockHand, wallClockHand;
    public bool isClockHandFound, isCarpetMoved, isTask1Completed, isTask2Completed;
    public TextMeshPro txtInstructions;

    public static Level_1_LevelFlowManager _instance;

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
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Carpet") && !isCarpetMoved)
            {
                carpet.DOLocalMoveZ(-3f, 1f);
                txtInstructions.text = "";
                isCarpetMoved = true;
            }
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Carpet") && isCarpetMoved)
            {
                //carpet.DOMoveZ(0f, 1f);
                txtInstructions.text = "";
                isCarpetMoved = false;
            }
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("ClockHand") && !isClockHandFound)
            {
                clockHand.SetActive(false);
                wallClockHand.SetActive(true);
                wallClock.GetComponent<BoxCollider>().enabled = true;
                txtInstructions.text = "You got clock's missing hand.";
                isClockHandFound = true;
            }
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Door"))
            {
                if (!isTask2Completed) txtInstructions.text = "Fix panel first!";
                else txtInstructions.text = "";
            }
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("InOrder"))
            {
                if (!isTask1Completed) txtInstructions.text = "Fix Clock first!";
                else txtInstructions.text = "";
            }
            if (isTask1Completed && isTask2Completed) {
                roomDoor.DOLocalRotate(new Vector3(0, 0, 0), 3f);
            }
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Menu"))
            {
                SceneManager.LoadSceneAsync("Level_2");
            }
        }
    }
}

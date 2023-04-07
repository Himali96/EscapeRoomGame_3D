using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class Level_4_LevelFlowManager : MonoBehaviour
{
    public static Level_4_LevelFlowManager _instance;

    // References
    public TextMeshPro txtInstructions;
    LevelManager levelManager = null;
    public AudioSource clickSound;
    public Transform roomDoor;
    public GameObject oven;
    public GameObject clock;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    void Start()
    {
        levelManager = GetComponent<LevelManager>();
        clock.GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {

        if (!levelManager.tasksCompleted[0])
        {
            txtInstructions.text = "Cook the chicken in oven";
        }

        if (levelManager.tasksCompleted[0])
        {
            txtInstructions.text = "Set the time on Clock";
            oven.GetComponent<BoxCollider>().enabled = false;
            clock.GetComponent<BoxCollider>().enabled = true;
        }

        if (levelManager.tasksCompleted[0] && levelManager.tasksCompleted[1])
        {
            UnlockTheDoor();
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {

            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Menu"))
            {
                clickSound.Play();
                SceneManager.LoadSceneAsync("Scoreboard");
            }
        }
    }

    public void UnlockTheDoor()
    {
        txtInstructions.text = "Game Completed";
        roomDoor.DORotate(new Vector3(0, 90f, 0), 3f);
    }
}
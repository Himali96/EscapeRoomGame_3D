using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class Level_3_LevelFlowManager : MonoBehaviour
{
    public static Level_3_LevelFlowManager _instance;

    // References
    public TextMeshPro txtInstructions;
    LevelManager levelManager = null;
    public AudioSource clickSound;
    public Transform roomDoor;
    public GameObject monitor;
    public GameObject circuitboard;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    void Start()
    {
        levelManager = GetComponent<LevelManager>();
        circuitboard.GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {

        if (!levelManager.tasksCompleted[0])
        {
            txtInstructions.text = "Hack the computer";
        }

        if (levelManager.tasksCompleted[0])
        {
            txtInstructions.text = "Corrupt the switch";
            monitor.GetComponent<BoxCollider>().enabled = false;
            circuitboard.GetComponent<BoxCollider>().enabled = true;
        }

        if (levelManager.tasksCompleted[0] && levelManager.tasksCompleted[1])
        {
            UnlockTheDoor();
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("MemoryCircuit") && !levelManager.tasksCompleted[1])
            //{
            //    clickSound.Play();
            //    txtInstructions.text = "You need to crack the code first!";
            //}

            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Menu"))
            {
                clickSound.Play();
                SceneManager.LoadSceneAsync("Level_4");
            }
        }
    }

    public void UnlockTheDoor()
    {
        PlayerPrefs.SetInt("Level_3", 1);

        txtInstructions.text = "";
        roomDoor.DORotate(new Vector3(0, 90f, 0), 3f);
    }
}
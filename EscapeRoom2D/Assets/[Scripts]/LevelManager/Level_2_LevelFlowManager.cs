using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Level_2_LevelFlowManager : MonoBehaviour
{
    public bool isHandleFound, isScrewDriverFound;
    public TextMeshPro txtInstructions, txtToolsNames;
    LevelManager levelManager = null;

    public Transform safeDoor, roomDoor, tableDrawer, frame;
    public GameObject drawerKnob, tools;
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GetComponent<LevelManager>();
        //UnlockTheDoor();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Knob") && levelManager.tasksCompleted[1])
            {
                drawerKnob.SetActive(false);
                txtInstructions.text = "You got Drawer Knob";
                isHandleFound = true;
            }
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Drawer") && !isScrewDriverFound)
            {
                if (isHandleFound)
                {
                    tools.SetActive(true);
                    tableDrawer.localEulerAngles = new Vector3(0, 0, 0);
                    txtInstructions.text = "";
                } else
                    txtInstructions.text = "Need a knob to open the drawer";
            }
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Screwdriver"))
            {
                tools.SetActive(false);
                txtInstructions.text = "You got Screwdriver";
                isScrewDriverFound = true;
            }
            if (hit.collider.CompareTag("Tools") || hit.collider.CompareTag("Screwdriver"))
            {
                txtToolsNames.text = hit.collider.gameObject.name.ToString();
            }
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Door"))
            {
                if (!levelManager.tasksCompleted[0] && !levelManager.tasksCompleted[1])
                {
                    txtInstructions.text = "Fix the frame to escape";
                }
            }
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Menu"))
            {
                SceneManager.LoadSceneAsync("MainMenu");
            }
        }
    }
    public void UnlockTheSafe ()
    {
        safeDoor.DORotate(new Vector3(0, 90f, 0), 3f); 
    }

    public void UnlockTheDoor()
    {
        frame.DORotate(new Vector3(0, 180, 90), 3f).OnComplete(FixTheFrame);
    }

    public void FixTheFrame()
    {
        txtInstructions.text = "";
        roomDoor.DORotate(new Vector3(0, 90f, 0), 3f);
    }

    public void ToolsExitButtonClicked()
    {
        tools.SetActive(false);
    }
}

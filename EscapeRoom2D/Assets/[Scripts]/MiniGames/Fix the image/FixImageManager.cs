using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixImageManager : MonoBehaviour
{
    [SerializeField] RotateTheImageParts[] imageParts = null;
    LevelManager levelManager = null;
    Level_2_LevelFlowManager flowManager = null;
    public GameObject pictureGameObject;

    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
        flowManager = GetComponent<Level_2_LevelFlowManager>();
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Picture") && !levelManager.tasksCompleted[0])
            {
                Level_2_LevelFlowManager._instance.clickSound.Play();

                if (flowManager.isScrewDriverFound)
                    pictureGameObject.SetActive(true);
                else
                    flowManager.txtInstructions.text = "Need Screwdriver to fix the Image";
            }
        }

        if (ImageFixedChecker())
        {
            levelManager.tasksCompleted[0] = true;
            pictureGameObject.SetActive(false);
            flowManager.UnlockTheDoor();
        }
    }

    public void PictureExitButtonClicked()
    {
        pictureGameObject.SetActive(false);
    }

    private bool ImageFixedChecker()
    {
        for (int i = 0; i < imageParts.Length; i++)
        {
            if (!imageParts[i].isImgFixed)
            {
                return false;
            }
        }

        return true;
    }
}
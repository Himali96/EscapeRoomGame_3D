using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixImageManager : MonoBehaviour
{
    [SerializeField] RotateTheImageParts[] imageParts = null;
    LevelManager levelManager = null;
    public GameObject pictureGameObject;

    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
    }
    // Update is called once per frame
    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Picture") && !levelManager.tasksCompleted[0])
            {
                pictureGameObject.SetActive(true);

            }
        }

        if (ImageFixedChecker())
        {
            //Debug.Log("Picture Puzzle Done!");
            levelManager.tasksCompleted[0] = true;
            pictureGameObject.SetActive(false);
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
            if(!imageParts[i].isImgFixed)
            {
                return false;
            }
        }
        return true;
    }
}

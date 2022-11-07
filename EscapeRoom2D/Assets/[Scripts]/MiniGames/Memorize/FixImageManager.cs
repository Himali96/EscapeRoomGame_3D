using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixImageManager : MonoBehaviour
{
    [SerializeField] RotateTheImageParts[] imageParts = null;

    // Update is called once per frame
    void Update()
    {
        if(ImageFixedChecker())
        {
            Debug.Log("Puzzle Done!");
        }
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

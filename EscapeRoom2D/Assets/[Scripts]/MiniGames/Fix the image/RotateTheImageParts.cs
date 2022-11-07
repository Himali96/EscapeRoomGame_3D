using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateTheImageParts : MonoBehaviour
{
    public bool isImgFixed = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, 90 * Random.Range(1, 4));
    }

    public void OnRotate()
    {
        transform.Rotate(0f, 0f, 90f);
        Debug.Log(gameObject.name + " " + transform.eulerAngles.z);
        if ((int)transform.eulerAngles.z == 0) isImgFixed = true;
        else isImgFixed = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Test_Level2
{
    [SetUp]
    public void SetUp(){
        SceneManager.LoadScene("Level_2");
    }
    
    
    [Test]
    public void Test_OpenTaks1()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width * 0.8f, Screen.height * 0.2f));
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        
        Assert.True(hit.collider != null && hit.collider.name("mario"));
    }
    
    [Test]
    public void Test_OpenTaks2()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width * 0.9f, Screen.height * 0.7f));
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        
        Assert.True(hit.collider != null && hit.collider.name("Safe A"));
    }
    
    [Test]
    public void Test_OpenTaks3()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width * 0.2f, Screen.height * 0.5f));
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        
        Assert.True(hit.collider != null && hit.collider.name("Knob"));
    }

    [UnityTest]
    public IEnumerator Test_CheckStartLevel2()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        
        // Find Canvas
        GameObject canvas = GameObject.Find("Canvas");

        // All minigmaes start disable
        Assert.IsTrue(canvas.transform.GetChild(0).gameObject.activeSelf == false 
                      && canvas.transform.GetChild(1).gameObject.activeSelf == false 
                      && canvas.transform.GetChild(2).gameObject.activeSelf == false);
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Test_Click
{
    [SetUp]
    public void SetUp(){
        //SceneManager.LoadScene("Level_1");
    }
    
    // A Test behaves as an ordinary method
    [Test]
    public void Test_OpenTaks1()
    {
        /*Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width * 0.8f, Screen.height * 0.2f));
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        
        Assert.True(hit.collider != null && hit.collider.name("Clock"));*/
    }
    
    [Test]
    public void Test_OpenTaks2()
    {
        /*Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width * 0.8f, Screen.height * 0.2f));
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        
        Assert.True(hit.collider != null && hit.collider.name("panel_task2"));*/
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Test_CheckStartLevel1()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        
        // Find Canvas
        /*GameObject canvas = GameObject.Find("Canvas");

        // All minigmaes start disable
        Assert.IsTrue(canvas.transform.GetChild(0).gameObject.activeSelf == false 
                      && canvas.transform.GetChild(1).gameObject.activeSelf == false);*/
    }
}

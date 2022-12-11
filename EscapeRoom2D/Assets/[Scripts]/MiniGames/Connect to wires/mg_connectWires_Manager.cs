using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mg_connectWires_Manager : MonoBehaviour
{

    [Header("Nodes")]

    [SerializeField] int gridSizeX = 5;
    [SerializeField] int gridSizeY = 5;
    public float gridSize = 1;

    [SerializeField] GameObject nodePrefab = null;

    [Header("References")]
    [SerializeField] Camera cam= null;
    [SerializeField] mg_connectWires_Node[] startedNodes = null;
    [SerializeField] Button resetBtn = null;

    bool isDragging = false;
    mg_connectWires_Node currentNode = null;

    RaycastHit2D hit;

    void OnEnable()
    {
        resetBtn.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        if(resetBtn)
            resetBtn.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            hit = Physics2D.Raycast(ray.origin, Vector2.zero);

            if (hit)
            {
                currentNode = hit.collider.GetComponent<mg_connectWires_Node>();

                if (currentNode == null)
                    return;

                if (currentNode.sprRender.color.IsEqual(Color.white))
                {
                    currentNode = null;
                    return;
                }

                isDragging = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            currentNode = null;
        }
        else if(isDragging && Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            hit = Physics2D.Raycast(ray.origin, Vector2.zero);

            if (hit)
            {
                mg_connectWires_Node hitNode = hit.collider.GetComponent<mg_connectWires_Node>();

                if (!hitNode)
                    return;

                if (hitNode == currentNode) // Not process if is the same
                    return;

                float distance = Vector3.Distance(currentNode.transform.position, hitNode.transform.position);
                distance /= gridSize;

                if (distance > gridSize) // Is not connected
                    return;

                hitNode.Connect(currentNode);

                currentNode = hitNode;

                CheckIfAllAreConnected();
            }
        }

    }

    public void BtnReset()
    {
        foreach(mg_connectWires_Node node in FindObjectsOfType<mg_connectWires_Node>())
        {
            node.Clean();
            node.connectionNode = null;
            node.connectionByNode = null;
        }
    }

    // This use a DFS to check if all nodes are connected
    void CheckIfAllAreConnected()
    {
        Dictionary<int, bool> connectionsSuccessMap = new Dictionary<int, bool>();

        foreach (mg_connectWires_Node node in startedNodes)
        {
            if (connectionsSuccessMap.ContainsKey(node.startIdColor) && connectionsSuccessMap[node.startIdColor]) // Is already connected?
            {
                continue;
            }

            int whileSecurity = 0;
            mg_connectWires_Node currentNodeTest = node;
            List<mg_connectWires_Node> nodesVisistes = new List<mg_connectWires_Node>(10);
            nodesVisistes.Add(currentNodeTest);
            
            // DFS for each node connected until we reach the end or the other point
            while (currentNodeTest.connectionNode != null || currentNodeTest.connectionByNode != null)
            {
                whileSecurity++;
                if (whileSecurity == 50)
                {
                    print("Exit from infinity while");
                    return;
                }

                // Check if the node have a connection
                if (currentNodeTest.connectionNode && !nodesVisistes.Contains(currentNodeTest.connectionNode))
                {
                    currentNodeTest = currentNodeTest.connectionNode;
                }
                else if (currentNodeTest.connectionByNode && !nodesVisistes.Contains(currentNodeTest.connectionByNode))
                {
                    currentNodeTest = currentNodeTest.connectionByNode;
                }
                else
                {
                    break;
                }

                if (currentNodeTest == null)
                    continue;

                // Add to process nodes
                nodesVisistes.Add(currentNodeTest);

                if (currentNodeTest.startPoint && currentNodeTest != node) // we reach the other start point
                {
                    connectionsSuccessMap.Add(node.startIdColor, true);
                    break;
                }
            }
        }

        // All colors are connected?
        foreach (mg_connectWires_Node node in startedNodes)
        {
            if (!connectionsSuccessMap.ContainsKey(node.startIdColor))
            {
                return;
            }
        }
        
        this.enabled = false;
        Level_1_LevelFlowManager._instance.isTask1Completed = true;
        MiniGameLoader.Instance.UnLoadLastLevel(true);
        Level_1_LevelFlowManager._instance.txtInstructions.text = "You can fix the panel now!";
    }
    
    public void WiresExitButtonClicked()
    {
        gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    [ContextMenu("Create nodes")]
    void CreateMap()
    {
        Vector3 offset = new Vector3(gridSize / 2f, gridSize / 2f, 0f);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 pos = transform.position;
                pos.x = x * gridSize;
                pos.y = y * gridSize;
                pos += offset;
                
                GameObject newNode = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(nodePrefab);
                newNode.transform.position = pos;
                newNode.GetComponent<mg_connectWires_Node>().manager = this;
                newNode.transform.parent = transform;
            }
        }
    }
    #endif

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        for (int x = 0; x <= gridSizeX; x++)
        {
            Vector3 pos1 = new Vector3(x * gridSize, 0f, 0f);
            Vector3 pos2 = new Vector3(x * gridSize, gridSizeY * gridSize, 0f);

            Gizmos.DrawLine(pos1, pos2);
        }

        for (int y = 0; y <= gridSizeY; y++)
        {
            Vector3 pos1 = new Vector3(0f, y * gridSize, 0f);
            Vector3 pos2 = new Vector3(gridSizeX * gridSize, y * gridSize, 0f);

            Gizmos.DrawLine(pos1, pos2);
        }

    }
}

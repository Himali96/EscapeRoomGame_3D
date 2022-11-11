using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    bool isDragging = false;
    mg_connectWires_Node currentNode = null;

    RaycastHit2D hit;

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

                if (currentNode.sprRender.color == Color.white)
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

    void CheckIfAllAreConnected()
    {
        Dictionary<Color, bool> connectionsSuccessMap = new Dictionary<Color, bool>();

        foreach (mg_connectWires_Node node in startedNodes)
        {
            if (connectionsSuccessMap.ContainsKey(node.sprRender.color) && connectionsSuccessMap[node.sprRender.color]) // Is already connected?
            {
                continue;
            }

            int whileSecurity = 0;
            mg_connectWires_Node currentNode = node;
            while (currentNode.connectionNode != null)
            {
                whileSecurity++;

                if (whileSecurity == 50)
                {
                    print("Exit from infinity while");
                    return;
                }

                currentNode = currentNode.connectionNode;

                if (currentNode.startPoint) // we reach the other start point
                {
                    connectionsSuccessMap.Add(node.sprRender.color, true);
                    break;
                }
            }
        }

        foreach (mg_connectWires_Node node in startedNodes)
        {
            if (!connectionsSuccessMap.ContainsKey(node.sprRender.color))
            {
                return;
            }
        }

        print("All conected!!!");
        this.enabled = false;
    }


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
                
                GameObject newNode = (GameObject)PrefabUtility.InstantiatePrefab(nodePrefab);
                newNode.transform.position = pos;
                newNode.GetComponent<mg_connectWires_Node>().manager = this;
                newNode.transform.parent = transform;
            }
        }

    }

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

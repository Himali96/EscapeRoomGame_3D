using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is only required set start nodes

public class mg_connectWires_Node : MonoBehaviour
{
    public mg_connectWires_Manager manager;

    public mg_connectWires_Node connectionNode;
    public mg_connectWires_Node connectionByNode;

    public bool startPoint;
    public int startIdColor;

    [System.NonSerialized] public SpriteRenderer sprRender;
    LineRenderer lineRenderer;

    void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();

        if (startPoint)
        {
            Gradient colorGradient = lineRenderer.colorGradient;
            colorGradient.SetKeys(
                new[] {new GradientColorKey(sprRender.color, 0.0f), new GradientColorKey(sprRender.color, 1.0f)},
                new[] {new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(1f, 1.0f)}
            );
            lineRenderer.colorGradient = colorGradient;
        }
    }

    public void Connect(mg_connectWires_Node _otherNode)
    {
        if (startPoint)
        {
            if (sprRender.color.IsEqual(_otherNode.sprRender.color) == false)
            {
                print("Denied for different color");
                return;
            }
        }

        if (startPoint == false)
        {
            //Color
            Gradient colorGradient = lineRenderer.colorGradient;
            colorGradient.SetKeys(
                new[] {new GradientColorKey(_otherNode.sprRender.color, 0.0f), new GradientColorKey(_otherNode.sprRender.color, 1.0f)},
                new[] {new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(1f, 1.0f)}
            );
            lineRenderer.colorGradient = colorGradient;
            sprRender.color = _otherNode.sprRender.color;
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, _otherNode.transform.position);

        lineRenderer.enabled = true;

        if ((transform.position.x - _otherNode.transform.position.x > 0.1f) ||
            (transform.position.y - _otherNode.transform.position.y > 0.1f))
        {
            connectionNode = _otherNode;
            _otherNode.connectionByNode = this;
        }
        else
        {
            _otherNode.connectionNode = this;
            connectionByNode = _otherNode;
        }
    }

    public void Clean()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        if (startPoint == false)
        {
            sprRender.color = Color.white;
        }
    }
}
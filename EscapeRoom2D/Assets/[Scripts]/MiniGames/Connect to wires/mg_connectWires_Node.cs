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

    [System.NonSerialized]
    public SpriteRenderer sprRender;
    LineRenderer lineRenderer;

    void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();

        if (startPoint)
        {
            Gradient colorGradient = lineRenderer.colorGradient;
            colorGradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(sprRender.color, 0.0f), new GradientColorKey(sprRender.color, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(1f, 1.0f) }
            );
            lineRenderer.colorGradient = colorGradient;
        }
    }

    public void Connect(mg_connectWires_Node _otherNode)
    {
        if (startPoint)
        {
            if (sprRender.color != _otherNode.sprRender.color)
                return;
        }

        if (connectionByNode && connectionByNode.sprRender.color != sprRender.color)
        {
            connectionByNode.lineRenderer.enabled = false;
            if (connectionByNode.connectionNode == this)
            {
                connectionByNode.connectionNode = null;
            }
        }

        lineRenderer.enabled = _otherNode != null;

        if (_otherNode)
        {
            if (startPoint == false)
            {
                //Color
                Gradient colorGradient = lineRenderer.colorGradient;
                colorGradient.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(_otherNode.sprRender.color, 0.0f), new GradientColorKey(_otherNode.sprRender.color, 1.0f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(1f, 1.0f) }
                );
                lineRenderer.colorGradient = colorGradient;

                sprRender.color = _otherNode.sprRender.color;
            }

            //Connection
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _otherNode.transform.position);

            if (_otherNode.startPoint)
            {
                lineRenderer.SetPosition(0, _otherNode.transform.position);
                lineRenderer.SetPosition(1, transform.position);
            }
        }

        connectionNode = _otherNode;
        _otherNode.connectionByNode = this;
    }
}

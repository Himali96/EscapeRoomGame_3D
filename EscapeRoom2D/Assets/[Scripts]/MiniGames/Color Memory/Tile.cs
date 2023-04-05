using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Red,
    Green,
    Blue,
    Yellow,
    Magenta,
    Black,
    Orange,
    Cyan
}

public class Tile : MonoBehaviour
{
    private TileType type;
    private Color lockedColor;

    private static Color[] ColorLookup = {Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.black, new Color(1.0f, 0.5f, 0.0f), Color.cyan};

    public void Init(TileType type)
    {
        this.type = type;
        lockedColor = GetComponent<SpriteRenderer>().color;
    }

    public TileType Type
    {
        get => type;
        private set => type = value;
    }

    public void Unlock()
    {
        GetComponent<SpriteRenderer>().color = ColorLookup[(int) type];
    }

    public void Lock()
    {
        GetComponent<SpriteRenderer>().color = lockedColor;
    }
}
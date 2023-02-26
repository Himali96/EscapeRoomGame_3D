using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Queue<Tile> tiles = new Queue<Tile> ();

    private bool busy = false;
    private int winCount = 0;

    [SerializeField]
    private float destroyDelay;

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0) && !busy)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            Tile tile = hit.collider?.GetComponent<Tile>();
            if (tile != null)
            {
                bool queue = true;

                if(tiles.Count >= 2) {
                    tiles.Clear();
                }
                else if(tiles.Count == 1){
                    if(tile.Type != tiles.Peek().Type)
                    {
                        tiles.Peek().Lock();
                        tiles.Clear();
                    }
                    else
                    {
                        queue = false;
                        StartCoroutine(DestroyTiles(tile));
                    }
                }
                if (queue)
                {
                    tile.Unlock();
                    tiles.Enqueue(tile);
                }
            }
        }
    }

    private IEnumerator DestroyTiles(Tile tile)
    {
        winCount++;
        busy = true;
        tile.Unlock();
        yield return new WaitForSeconds(destroyDelay);
        Destroy(tiles.Peek().gameObject);
        Destroy(tile.gameObject);
        tiles.Clear();
        busy = false;
        if (winCount == 8)
        {
            Debug.Log("You won");
        }
    }
}

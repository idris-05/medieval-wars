using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    [SerializeField] List<Tile> Grasses = new List<Tile>();
    [SerializeField] List<Tile> Mountains = new List<Tile>();


    readonly Dictionary<Tile, int> tilesDictionary = new Dictionary<Tile, int>();
    // Start is called before the first frame update

    void Awake()
    {
        Grasses.ForEach(grass => tilesDictionary.Add(grass, 0));
        Mountains.ForEach(mountain => tilesDictionary.Add(mountain, 1));
        // 3mer kolch .
    }

    void Start()
    {
        // Tilemap tilemap = GetComponent<Tilemap>();
        Tilemap tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0); // z-coordinate is typically 0 in a 2D tilemap
                TileBase tile = tilemap.GetTile(cellPosition);

                if (tile != null)
                {
                    // Do something with the tile
                    Debug.Log("Tile at position (" + x + ", " + y + ") is " + tile.name);
                }
                else
                {
                    Debug.Log("No tile at position (" + x + ", " + y + ")");
                }
            }
        }

    }

    // // Update is called once per frame
    void Update()
    {

    }



}

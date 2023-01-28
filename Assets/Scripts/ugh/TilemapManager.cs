using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private List<TileData> tileDatas;

    private List<TileBase> lighteningTiles;

    private bool startEnumerating = false;

    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake()
    {
        lighteningTiles = new List<TileBase>();
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    private void Update()
    {
        if (startEnumerating)
        {
            foreach (TileBase tileBase in lighteningTiles)
            {
                if (dataFromTiles[tileBase].tileColor.a <= 0.98)
                {

                    //tilemap.SetColor(tileBase.)
                    dataFromTiles[tileBase].tileColor = new Color(1, 1, 1, dataFromTiles[tileBase].tileColor.a + Time.deltaTime);
                }
                else
                {
                    //lighteningTiles.Remove(tileBase);
                }
            }
            //if (lighteningTiles)
        }

    }

    public void ReApplyDarkness(List<TileBase> tileBases)
    {
        lighteningTiles = tileBases;
        startEnumerating = true;
    }
}

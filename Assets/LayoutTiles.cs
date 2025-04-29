using System;
using System.Collections.Generic;
using UnityEngine;

public class LayoutTiles : MonoBehaviour
{
    public Vector2Int gridSize;
    public Vector3 spacing;

    public TileScript prefab;

    public List<GameObject> generated;

    public GameScript gameScript;
    
    [EditorCools.Button]
    public void Layout()
    {
        foreach(var g in generated) DestroyImmediate(g);
        generated.Clear();
        gameScript.tiles.Clear();

        var startX = -(gridSize.x - 1) / 2.0f * spacing.x;
        var startY = -(gridSize.y - 1) / 2.0f * spacing.y;        
        for (var x = 0; x < gridSize.x; x++)
        {
            for (var y = 0; y < gridSize.y; y++)
            {
                var tileScript = Instantiate(prefab, this.transform);
                var g = tileScript.gameObject;
                generated.Add(g);
                g.transform.localPosition = new Vector3(startX + x * spacing.x, startY + y * spacing.y, 0);
                
                gameScript.tiles.Add(tileScript);
            }
        }
    }
}

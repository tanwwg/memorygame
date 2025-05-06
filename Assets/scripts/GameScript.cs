using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EditorCools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameScript : MonoBehaviour
{
    public List<TileScript> tiles;
    public TileLibrary lib;

    public float hideWaitTime;
    public float winWaitTime;

    public UnityEvent onWin;

    public LayoutTiles grid;

    public string restartScene = "SampleScene";

    public void Start()
    {
        grid.Layout();
        
        var tilesLeft = tiles.ToList();
        tilesLeft.Shuffle();
        var choose = lib.tiles.ToList();
        choose.Shuffle();
        
        while (tilesLeft.Count > 0)
        {
            var sel = choose[0];
            Debug.Log(sel.name);
            tilesLeft[0].SetTile(sel);
            tilesLeft[1].SetTile(sel);
            tilesLeft.RemoveAt(0);
            tilesLeft.RemoveAt(0);     
            choose.RemoveAt(0);
        }
    }

    public void TileClicked(TileScript tile)
    {
        // if there are two tiles revealed, hide them all
        var revealed = tiles.Where(t => t.isReveal).ToList();
        if (revealed.Count >= 2) return;

        if (tile.isReveal) return;
        // {
        //     tile.Hide();
        //     return;
        // }


        tile.Reveal();
        revealed = tiles.Where(t => t.isReveal).ToList();
        if (revealed.Count < 2) return;

        StartCoroutine(TileClickedAsync());
        
        
    }

    private IEnumerator TileClickedAsync()
    {
        var revealed = tiles.Where(t => t.isReveal).ToList();
        if (revealed.Count < 2) yield break;

        if (revealed[0].tileData.name == revealed[1].tileData.name)
        {
            yield return new WaitForSeconds(winWaitTime);
            revealed[0].Win();
            revealed[1].Win();
            tiles.Remove(revealed[0]);
            tiles.Remove(revealed[1]);

            if (tiles.Count == 0)
            {
                onWin.Invoke();
            }
        }
        else
        {
            yield return new WaitForSeconds(hideWaitTime);            
            foreach(var r in revealed) r.Hide();
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(restartScene);
    }

}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // UnityEngine.Random
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}

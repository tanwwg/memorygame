using EditorCools;
using UnityEngine;
using UnityEngine.Events;

public class TileScript : MonoBehaviour
{
    [SerializeField] [ReadOnly]
    public bool isReveal;

    public UnityEvent<TileScript> onClick;
    public UnityEvent onReveal;

    public UnityEvent onHide;

    public UnityEvent onWin;

    public SetMaterialTexture setTileTexture;
    public AudioSource audioSource;

    [SerializeField] [ReadOnly] public TileData tileData;

    public void HandleClick()
    {
        onClick.Invoke(this);
    }

    public void Reveal()
    {
        isReveal = true;
        onReveal.Invoke();
    }

    public void Hide()
    {
        isReveal = false;
        onHide.Invoke();
    }

    [EditorCools.Button]
    public void Win()
    {
        onWin.Invoke();
    }

    public void SetTile(TileData tile)
    {
        this.tileData = tile;
        setTileTexture.Invoke(tile.picture);
        audioSource.clip = tile.audio;
    }
}

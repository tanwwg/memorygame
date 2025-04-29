using System;
using UnityEngine;

[Serializable]
public class TileData
{
    public string name;
    public Texture2D picture;
    public AudioClip audio;
}

[CreateAssetMenu(fileName = "TileLibrary", menuName = "Scriptable Objects/TileLibrary")]
public class TileLibrary : ScriptableObject
{
    public TileData[] tiles;
}

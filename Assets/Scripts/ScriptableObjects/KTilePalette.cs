using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTM
{
    [CreateAssetMenu(
        fileName = "TilePalette", 
        menuName = "KTM/Tile Palette", 
        order = 120
    )]
    public class KTilePalette : ScriptableObject
    {
        public List<KTilePaletteLayer> Layers = new();

        public Vector2Int TileSize = new(32, 32);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTM
{
    [System.Serializable]
    public class KTile
    {
        public Vector3Int Position;

        public int PaletteLayer;
        public Vector2Int TileIndex;
    }

    [CreateAssetMenu(
        fileName = "Tilemap", 
        menuName = "KTM/Tilemap", 
        order = 121
    )]
    public class KTilemap : ScriptableObject
    {
        [SerializeField]
        public KTilePalette Palette;
            
        [SerializeField]
        public List<KTile> Tiles;
    }
}

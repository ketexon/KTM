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

    [System.Serializable]
    public class KTilemap
    {
        [SerializeField]
        KTilePalette palette;
            
        [SerializeField]
        List<KTile> tiles;
    }
}

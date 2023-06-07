using UnityEngine;

namespace KTM
{
    [System.Serializable]
    public class KTilemapReference
    {
        [SerializeField]
        KTilemap inline;

        [SerializeField]
        KTilemapStorage storage;

        public KTilemap Tilemap => storage != null ? storage.Tilemap : inline;
    }
}
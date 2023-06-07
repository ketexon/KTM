using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTM
{
    [CreateAssetMenu(
        fileName = "Tilemap Storage",
        menuName = "KTM/Tilemap Storage",
        order = 121
    )]
    public class KTilemapStorage : ScriptableObject
    {
        [SerializeField]
        public KTilemap Tilemap;
    }
}

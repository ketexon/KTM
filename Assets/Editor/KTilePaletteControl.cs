using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace KTM.Editor
{
    public class KTilePaletteControl : VisualElement
    {
        // https://docs.unity3d.com/ScriptReference/UIElements.VisualElement-generateVisualContent.html
        public new class UxmlFactory : UxmlFactory<KTilePaletteControl, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlIntAttributeDescription xIndex = new UxmlIntAttributeDescription { name = "x-index" };
            UxmlIntAttributeDescription yIndex = new UxmlIntAttributeDescription { name = "y-index" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var control = ve as KTilePaletteControl;

                control.Selected = new Vector2Int(xIndex.GetValueFromBag(bag, cc), yIndex.GetValueFromBag(bag, cc));
            }
        }

        public Vector2Int Selected { get; private set; }

        public KTilePaletteControl()
        {
            generateVisualContent += GenerateVisualContent;
        }

        ~KTilePaletteControl()
        {
            generateVisualContent -= GenerateVisualContent;
        }

        void GenerateVisualContent(MeshGenerationContext ctx)
        {
        }
    }
}
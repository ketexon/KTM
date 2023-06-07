using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace KTM.Editor
{
    [CustomEditor(typeof(KTilePalette))]
    public class KTilePaletteEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            root.Add(Util.ScriptField(this));

            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            return root;
        }
    }
}
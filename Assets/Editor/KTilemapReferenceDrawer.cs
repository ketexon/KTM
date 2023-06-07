using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using PlasticGui.WorkspaceWindow.QueryViews;

namespace KTM.Editor
{
    [CustomPropertyDrawer(typeof(KTilemapReference))]
    public class KTilemapReferenceDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            var storageProperty = property.FindPropertyRelative("storage");
            root.Add(new PropertyField(
                storageProperty,
                $"[{property.displayName}]"
            ));

            var storage = storageProperty.objectReferenceValue;
            if (storage == null)
            {
                var inlineProperty = property.FindPropertyRelative("inline");
                var inlinePropertyField = new PropertyField(inlineProperty, "Inline Storage");

                inlinePropertyField.style.paddingLeft = 7;

                root.Add(inlinePropertyField);
            }

            return root;
        }
    }
}

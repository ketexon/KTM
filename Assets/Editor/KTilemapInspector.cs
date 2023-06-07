using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace KTM.Editor
{
    public class KTilemapInspector : UnityEditor.EditorWindow
    {
        [SerializeField]
        Texture _icon;

        public Texture Icon => _icon;

        [MenuItem("Window/KTM/Inspector", false, 2050)]
        static void ContextMenu()
        {
            Get();
        }

        public static KTilemapInspector Get(bool focus = true)
        {
            var inspectorWindowType = System.Type.GetType("UnityEditor.InspectorWindow,UnityEditor.dll");
            var window = GetWindow<KTilemapInspector>(
                title: "",
                focus: focus,
                desiredDockNextTo: new System.Type[] { inspectorWindowType }
            );

            window.titleContent = new GUIContent(
                "KTM Inspector",
                window.Icon
            );

            window.Show();

            return window;
        }

        void OnEnable()
        {
            KTilemapTool.SelectedEvent += OnTilesSelected;
        }

        void OnDisable()
        {
            KTilemapTool.SelectedEvent -= OnTilesSelected;
        }

        void OnTilesSelected(KTilemapTool tool)
        {
        }

        void CreateGUI()
        {
            
        }
    }
}

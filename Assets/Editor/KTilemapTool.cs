using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace KTM.Editor {
    [EditorTool("KTM", typeof(KTilemap))]
    public class KTilemapTool : EditorTool
    {
        Color GridColor => new Color(1f, 0f, 0f, 0.2f);
        Color SelectColor => Color.white;

        Vector2Int selected = Vector2Int.zero;


        void DrawGrid(Vector3 min, Vector3 max)
        {
            Vector3Int minInt = Vector3Int.CeilToInt(min);
            Vector3Int maxInt = Vector3Int.FloorToInt(max);

            Handles.color = GridColor;
            for (int x = minInt.x; x <= maxInt.x; ++x)
            {
                Handles.DrawLine(
                    new Vector3(x, min.y),
                    new Vector3(x, max.y),
                    1
                );
            }

            for (int y = minInt.y; y <= maxInt.y; ++y)
            {
                Handles.DrawLine(
                    new Vector3(min.x, y),
                    new Vector3(max.x, y),
                    1
                );
            }
        }

        void DrawSelected()
        {
            Handles.DrawSolidRectangleWithOutline(
                new Rect((Vector2)selected, Vector2.one),
                Color.clear,
                SelectColor
            );
        }

        void Repaint(EditorWindow window)
        {
            EditorUtility.SetDirty(window);
        }

        public override void OnToolGUI(EditorWindow window)
        {
            HandleUtility.AddControl(0, 0);

            SceneView sceneView = window as SceneView;
            if (sceneView == null) return;

            var camera = sceneView.camera;
            var evt = Event.current;

            if (!sceneView.in2DMode) sceneView.in2DMode = true;

            var mousePos = evt.mousePosition;
            var mousePosWorld = (Vector2)HandleUtility.GUIPointToWorldRay(mousePos).origin;

            if (evt.type == EventType.Repaint)
            {
                EditorGUIUtility.AddCursorRect(
                    camera.pixelRect,
                    MouseCursor.Arrow
                );

                DrawSelected();

                var min = camera.ViewportToWorldPoint(Vector3.zero);
                var max = camera.ViewportToWorldPoint(Vector3.one);

                DrawGrid(min, max);
            }

            if (evt.type == EventType.MouseDown)
            {
                selected = Vector2Int.FloorToInt(mousePosWorld);
                // repaint
                Repaint(window);
            }

            if (evt.type == EventType.MouseDrag)
            {
                Debug.Log("DRAG");
            }

            if (evt.type == EventType.MouseMove)
            {
                Debug.Log("MOVE");
            }
        }

        [Shortcut("KTM", null, KeyCode.T, ShortcutModifiers.Shift)]
#pragma warning disable IDE0051 // Remove unused private members
        static void ToolShortcut()
        {
            if (Selection.GetFiltered<KTilemap>(SelectionMode.TopLevel).Length > 0)
            {
                ToolManager.SetActiveTool<KTilemapTool>();
            }
        }
#pragma warning restore IDE0051 // Remove unused private members
    }
}
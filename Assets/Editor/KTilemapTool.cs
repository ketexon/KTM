using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace KTM.Editor {
    [EditorTool("KTM", typeof(KTM))]
    public class KTilemapTool : EditorTool
    {
        public KTM KTM => Selection.GetFiltered<KTM>(SelectionMode.TopLevel)[0];
        public List<RectInt> Selected => selected;

        public static System.Action<KTilemapTool> SelectedEvent;

        Color GridColor => new Color(1f, 0f, 0f, 0.2f);
        Color SelectColor => Color.white;

        List<RectInt> selected = new List<RectInt>{
            new RectInt(Vector2Int.zero, Vector2Int.one)
        };

        Vector2Int? selectionStart = null;
        Vector2Int? selectionEnd = null;


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
            foreach(var rect in selected)
            {
                Handles.DrawSolidRectangleWithOutline(
                    new Rect(rect.x, rect.y, rect.width, rect.height),
                    Color.clear,
                    SelectColor
                );
            }
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

            Vector2 mousePos = evt.mousePosition;
            Vector2 mousePosWorld = (Vector2)HandleUtility.GUIPointToWorldRay(mousePos).origin;
            Vector2Int mousePosGrid = Vector2Int.FloorToInt(mousePosWorld);

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

            if (evt.type == EventType.MouseDrag)
            {

                if(mousePosGrid != selectionEnd)
                {
                    selected.Clear();
                    selectionEnd = mousePosGrid;
                    selected.Add(new RectInt(
                        selectionStart.Value
                        + new Vector2Int(
                            selectionStart.Value.x <= selectionEnd.Value.x ? 0 : 1,
                            selectionStart.Value.y <= selectionEnd.Value.y ? 0 : 1
                        ), 
                        selectionEnd.Value - selectionStart.Value 
                        + new Vector2Int(
                            selectionStart.Value.x <= selectionEnd.Value.x ? 1 : -1,
                            selectionStart.Value.y <= selectionEnd.Value.y ? 1 : -1
                        )
                    ));
                    Repaint(window);
                }
            }

            if (evt.type == EventType.MouseDown)
            {
                selected.Clear();
                selected.Add(new RectInt(mousePosGrid, Vector2Int.one));

                selectionStart = mousePosGrid;
                selectionEnd = mousePosGrid;

                // repaint
                Repaint(window);
            }

            if(evt.type == EventType.MouseUp)
            {
                OnSelect();
            }

            if (evt.type == EventType.MouseMove)
            {
            }
        }

        void OnSelect()
        {
            KTilemapInspector.Get(true);
            SelectedEvent?.Invoke(this);
        }

        [Shortcut("KTM", null, KeyCode.T, ShortcutModifiers.Shift)]
#pragma warning disable IDE0051 // Remove unused private members
        static void ToolShortcut()
        {
            if (Selection.GetFiltered<KTM>(SelectionMode.TopLevel).Length > 0)
            {
                ToolManager.SetActiveTool<KTilemapTool>();
            }
        }
#pragma warning restore IDE0051 // Remove unused private members
    }
}
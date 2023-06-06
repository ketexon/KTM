using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;

namespace KTM.Editor
{
    //[CustomEditor(typeof(KTilemap))]
    //public class KTilemapEditor : UnityEditor.Editor
    //{
    //    Color GridColor => new Color(1f, 0f, 0f, 0.2f);
    //    Color SelectColor => Color.white;

    //    Vector2Int selected = Vector2Int.zero;


    //    void DrawGrid(Vector3 min, Vector3 max)
    //    {
    //        Vector3Int minInt = Vector3Int.CeilToInt(min);
    //        Vector3Int maxInt = Vector3Int.FloorToInt(max);

    //        Handles.color = GridColor;
    //        for (int x = minInt.x; x <= maxInt.x; ++x)
    //        {
    //            Handles.DrawLine(
    //                new Vector3(x, min.y),
    //                new Vector3(x, max.y),
    //                1
    //            );
    //        }

    //        for (int y = minInt.y; y <= maxInt.y; ++y)
    //        {
    //            Handles.DrawLine(
    //                new Vector3(min.x, y),
    //                new Vector3(max.x, y),
    //                1
    //            );
    //        }
    //    }

    //    void DrawSelected()
    //    {
    //        Handles.DrawSolidRectangleWithOutline(
    //            new Rect((Vector2)selected, Vector2.one),
    //            Color.clear,
    //            SelectColor
    //        );
    //    }

    //    void OnSceneGUI()
    //    {
    //        var sceneView = SceneView.currentDrawingSceneView;

    //        var camera = sceneView.camera;
    //        var evt = Event.current;

    //        if (!sceneView.in2DMode) sceneView.in2DMode = true;

    //        DrawSelected();

    //        var min = camera.ViewportToWorldPoint(Vector3.zero);
    //        var max = camera.ViewportToWorldPoint(Vector3.one);

    //        DrawGrid(min, max);

    //        var mousePos = evt.mousePosition;
    //        var mousePosWorld = (Vector2)HandleUtility.GUIPointToWorldRay(mousePos).origin;

    //        if(evt.type == EventType.Repaint)
    //        {
    //            EditorGUIUtility.AddCursorRect(
    //                camera.pixelRect, 
    //                MouseCursor.Arrow
    //            );
    //        }

    //        if(evt.type == EventType.MouseDown)
    //        {
    //            selected = Vector2Int.FloorToInt(mousePosWorld);
    //        }
    //    }
    //}

}
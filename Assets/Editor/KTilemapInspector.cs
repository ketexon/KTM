using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace KTM.Editor
{
    public class KTilemapInspector : UnityEditor.EditorWindow
    {
        [SerializeField]
        Texture _icon;

        public Texture Icon => _icon;

        int lastSelectedLayer = 0;

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
            KTilemapTool.ActivatedEvent += OnToolActivated;
            KTilemapTool.DeactivatedEvent += OnToolDeactivated;
        }

        void OnDisable()
        {
            KTilemapTool.SelectedEvent -= OnTilesSelected;
            KTilemapTool.ActivatedEvent -= OnToolActivated;
            KTilemapTool.DeactivatedEvent -= OnToolDeactivated;
        }

        void OnTilesSelected(KTilemapTool tool)
        {
        }

        void OnToolActivated(KTilemapTool tool)
        {
            RecreateGUI();
        }

        void OnToolDeactivated(KTilemapTool tool)
        {
            RecreateGUI();
        }

        void RecreateGUI()
        {
            ClearGUI();
            CreateGUI();
        }

        void ClearGUI()
        {
            rootVisualElement.Clear();
        }

        void CreateGUI()
        {
            rootVisualElement.style.paddingTop = 4;
            rootVisualElement.style.paddingLeft = 15;

            var ktm = KTilemapTool.KTM;
            if(ktm == null)
            {
                rootVisualElement.Add(new Label("No KTM selected."));
                return;
            }

            var ktmSO = new SerializedObject(ktm);

            SerializedProperty tilemapProperty = ktmSO.FindProperty("tilemap");

            var tilemapInspector = new PropertyField(tilemapProperty, "Tilemap");
            rootVisualElement.Add(tilemapInspector);

            KTilemap tilemap = tilemapProperty.objectReferenceValue as KTilemap;
            if(tilemap != null)
            {
                rootVisualElement.Add(CreateTilemapGUI(tilemap));
            }

            rootVisualElement.Bind(ktmSO);
        }

        VisualElement CreateTilemapGUI(KTilemap tilemap)
        {
            var root = new VisualElement();

            var tilemapSO = new SerializedObject(tilemap);
            var paletteProperty = tilemapSO.FindProperty("Palette");

            var paletteField = new PropertyField(paletteProperty);
            root.Add(paletteField);

            var palette = paletteProperty.objectReferenceValue as KTilePalette;
            if(palette != null)
            {
               root.Add(CreatePaletteGUI(palette));
            }

            root.Bind(tilemapSO);

            return root;
        }

        VisualElement CreatePaletteGUI(KTilePalette palette)
        {
            var root = new VisualElement();

            var paletteSO = new SerializedObject(palette);

            SerializedProperty layersProperty = paletteSO.FindProperty("Layers");
            SerializedProperty layerNamesProperty = paletteSO.FindProperty("LayerNames");

            var layerNames = new List<string>(palette.LayerNames);

            var layerField = new DropdownField(
                layerNames,
                lastSelectedLayer
            );
            layerField.label = "Layer";

            var removeButton = new Button(RemoveButtonClicked);
            removeButton.text = "-";
            layerField.Add(removeButton);
            if(palette.Layers.Count == 0)
            {
                removeButton.SetEnabled(false);
            }

            var addButton = new Button(AddButtonClicked);
            addButton.text = "+";
            layerField.Add(addButton);

            layerField.RegisterValueChangedCallback(LayerChanged);

            root.Add(layerField);

            if(palette.Layers.Count >= 1) {
                if(layerField.index >= 0 && layerField.index < palette.Layers.Count)
                {
                    var layerGUI = CreateLayerGUI(palette, layerField.index);
                    layerGUI.style.paddingLeft = 15;
                    root.Add(layerGUI);
                }
            }

            return root;

            void LayerChanged(ChangeEvent<string> evt)
            {
                lastSelectedLayer = layerField.index;
                RepaintPaletteInspector();
            }

            void RemoveButtonClicked()
            {
                int idx = layerField.index;
                layersProperty.DeleteArrayElementAtIndex(idx);
                layerNamesProperty.DeleteArrayElementAtIndex(idx);

                paletteSO.ApplyModifiedProperties();

                if(lastSelectedLayer >= palette.Layers.Count)
                {
                    lastSelectedLayer--;
                }

                RepaintPaletteInspector();
            }

            void AddButtonClicked()
            {
                int idx = palette.Layers.Count;
                layersProperty.InsertArrayElementAtIndex(idx);
                SerializedProperty newLayer = layersProperty.GetArrayElementAtIndex(idx);

                newLayer.boxedValue = new KTilePaletteLayer();

                string targetLayerName = "New Layer";
                int i = 1;
                while (palette.LayerNames.Contains(targetLayerName))
                {
                    targetLayerName = $"New Layer ({i++})";
                }

                idx = palette.LayerNames.Count;
                layerNamesProperty.InsertArrayElementAtIndex(idx);
                SerializedProperty newLayerName = layerNamesProperty.GetArrayElementAtIndex(idx);

                newLayerName.stringValue = targetLayerName;

                paletteSO.ApplyModifiedProperties();
                paletteSO.Update();

                lastSelectedLayer = idx;

                RecreateGUI();
            }

            void RepaintPaletteInspector()
            {
                root.Clear();
                root.Add(CreatePaletteGUI(palette));
            }
        }

        VisualElement CreateLayerGUI(KTilePalette palette, int idx)
        {
            var container = new VisualElement();
            var root = new VisualElement();
            container.Add(root);

            var paletteSO = new SerializedObject(palette);
            var layerSP = paletteSO.FindProperty("Layers").GetArrayElementAtIndex(idx);

            var textureSP = paletteSO.FindProperty("Layers").GetArrayElementAtIndex(idx);
            textureSP.NextVisible(true); // first element in KTilePalette

            var textureField = new PropertyField(textureSP, "Texture");
            root.Add(textureField);

            textureField.TrackPropertyValue(textureSP, TextureFieldChanged);

            var texture = textureSP.objectReferenceValue as Texture;
            if(texture != null)
            {
                var image = new Image();
                image.style.paddingTop = 4;
                image.image = texture;

                root.Add(image);
            }

            root.Bind(paletteSO);

            return container;

            void RepaintLayerInspector()
            {
                root.Clear();
                root.Add(CreateLayerGUI(palette, idx));
            }

            void TextureFieldChanged(SerializedProperty _)
            {
                RepaintLayerInspector();
            }
        }
    }
}

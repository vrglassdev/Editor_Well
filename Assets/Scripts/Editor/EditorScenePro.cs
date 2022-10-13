using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditorScenePro : EditorWindow
{
    private Color primaryColor = new Color(0.098f, 0.098f, 0.098f, 1.00f);
    Texture2D icon;
    public GUISkin _editorSkin;

    static public GameObject model3D, tempModel, managerData;
    string nameModel;
    Texture2D iconModel;

    bool oneshotInstantiate;
    bool offInteration = false;

    [MenuItem("EditorScenePro/Editor Scene Manager")]
    public static void ShowWindow()
    {
        GetWindow<EditorScenePro>("Editor Scene Manager");
    }

    private void OnEnable()
    {
        InitTextures();
    }

    private void OnGUI()
    {
        DrawLayouts();
    }

    private void InitTextures()
    {
        icon = Resources.Load("icons/editorSceneIcon", typeof(Texture2D)) as Texture2D;
        _editorSkin = Resources.Load("UI/GUI Skins/editorsceneSkin", typeof(GUISkin)) as GUISkin;
    }
    private void DrawLayouts()
    {
        Rect r = EditorGUILayout.BeginVertical();
        EditorGUI.DrawRect(r, primaryColor);
        GUILayout.Label(icon, GUILayout.Height(60), GUILayout.Width(250));
        if (_editorSkin)
        {
            GUIStyle styleField1 = this._editorSkin.GetStyle("byName");
            GUILayout.Label("by Well Gomes", styleField1);
        }
        //GUILayout.FlexibleSpace();
        EditorGUILayout.EndVertical();

        if (FindObjectOfType<ManagerData>())
        {
            GameObject temObj = FindObjectOfType<ManagerData>().gameObject;
            EditorGUI.BeginDisabledGroup(offInteration == false);
            EditorGUILayout.BeginHorizontal();
            managerData = EditorGUILayout.ObjectField("Manager Data", temObj, typeof(GameObject), true) as GameObject;
            EditorGUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();
        }

        if (managerData)
        {
            //Model 3D
            EditorGUILayout.BeginHorizontal();
            model3D = EditorGUILayout.ObjectField("Model 3D", model3D, typeof(GameObject), true) as GameObject;
            EditorGUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();

            if (tempModel)
            {
                EditorGUI.BeginDisabledGroup(offInteration == false);
                EditorGUILayout.BeginHorizontal();
                tempModel = EditorGUILayout.ObjectField("Model 3D", tempModel, typeof(GameObject), true) as GameObject;
                EditorGUILayout.EndHorizontal();
                EditorGUI.EndDisabledGroup();
            }

            if (model3D)
            {
                oneshotInstantiate = true;
                if (oneshotInstantiate)
                {

                    if (iconModel == null)
                    {
                        tempModel = Instantiate(model3D);

                        if (tempModel)
                        {
                            Texture2D texture = UnityEditor.AssetPreview.GetAssetPreview(tempModel);
                            //var a = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            iconModel = texture;//new Texture2D(texture.width, texture.height);
                        }
                    }

                    if (iconModel)
                    {
                        Rect r1 = EditorGUILayout.BeginHorizontal();
                        EditorGUI.DrawRect(r1, primaryColor);
                        int iButtonWidth = 125;
                        GUILayout.Space(Screen.width / 2 - iButtonWidth / 2);
                        //GUILayout.BeginArea(new Rect((Screen.width / 2) - 50, (r1.height + 100), 100, 100));
                        GUILayout.Label(iconModel, GUILayout.Height(150), GUILayout.Width(150));
                        //GUILayout.EndArea();

                        EditorGUILayout.EndHorizontal();
                        this.Repaint();
                    }

                    //Name Model
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Name key model");
                    GUIStyle styleField = this._editorSkin.GetStyle("textfield");
                    nameModel = EditorGUILayout.TextField(nameModel, styleField);
                    EditorGUILayout.EndHorizontal();
                    this.Repaint();


                    GUIStyle style = this._editorSkin.GetStyle("button");
                    //Button
                    EditorGUILayout.BeginVertical();
                    if (GUILayout.Button("CREATE MODEL PREFAB", style))
                    {
                        CreateAssetMenu();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            else
            {
                if (tempModel)
                {
                    DestroyImmediate(tempModel);
                    //tempModel = null;
                }
                iconModel = null;
                oneshotInstantiate = false;
            }
        }
    }

    private const string previewPath = "Resources/Textures";
    void CreateAssetMenu()
    {
        // Texture2D t = iconModel;
        // AssetDatabase.CreateAsset(t, "Assets/Resources/Textures/MyMaterial.png");
        if (managerData)
        {
            ManagerData md = managerData.GetComponent<ManagerData>();
            //Create SO
            #region CreateSO
            ModuleSO asset = ScriptableObject.CreateInstance<ModuleSO>();

            string name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/DataSO/" + nameModel + ".asset");
            AssetDatabase.CreateAsset(asset, name);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            asset.nameModel = nameModel;
            asset.prefabModel = model3D;
            Selection.activeObject = asset;
            #endregion

            #region SetInfo
            md.moduleController.modules.Add(asset);
            md.moduleController.numList = md.moduleController.modules.Count - 1;
            #endregion

            #region InstantieBTN
            GameObject go = Instantiate(md.modelBTNPrefab);
            go.transform.SetParent(md.contentSpawn);
            go.GetComponent<ModelBTN>().num = md.moduleController.modules.Count - 1;
            #endregion

            #region createImage
            string path = Path.Combine(Application.dataPath, string.Format("{0}/{1}.png", previewPath, nameModel));

            byte[] _bytes = iconModel.EncodeToPNG();
            System.IO.File.WriteAllBytes(path, _bytes);

            var provider = asset.texture;//go.GetComponent<ModelBTN>().texture;
            provider = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/" + path, typeof(Texture2D));
            go.GetComponent<ModelBTN>().texture = Resources.Load("Textures/"+ nameModel, typeof(Texture2D)) as Texture2D;
            #endregion

            if (PrefabUtility.IsPartOfRegularPrefab(managerData))
            {
                EditorUtility.SetDirty(managerData);
                PrefabUtility.RecordPrefabInstancePropertyModifications(managerData.gameObject);

                var scene = SceneManager.GetActiveScene();
                EditorSceneManager.MarkSceneDirty(scene);
               // EditorSceneManager.SaveScene(scene);
            }

            model3D = null;
        }
    }
}

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

[CustomEditor(typeof(PeopleWalkPath))]
public class PeopleWalkPathEditor : Editor
{
    private PeopleWalkPath walkPathTarget;

    private SerializedProperty _ignorePeopleProperty;
    private SerializedProperty _ignoreCarProperty;
    private SerializedProperty _ignoreBicycleProperty;
    private SerializedProperty _overrideDefaultAnimationMultiplierProperty;
    private SerializedProperty _customWalkAnimationMultiplierProperty;
    private SerializedProperty _customRunAnimationMultiplierProperty;

    public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
    {
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }
        return assets;
    }
    public void OnEnable()
    {
        walkPathTarget = target as PeopleWalkPath;

        _ignorePeopleProperty = serializedObject.FindProperty("_ignorePeople");
        _ignoreCarProperty = serializedObject.FindProperty("_ignoreCar");
        _ignoreBicycleProperty = serializedObject.FindProperty("_ignoreBicycle");
        _overrideDefaultAnimationMultiplierProperty = serializedObject.FindProperty("_overrideDefaultAnimationMultiplier");
        _customWalkAnimationMultiplierProperty = serializedObject.FindProperty("_customWalkAnimationMultiplier");
        _customRunAnimationMultiplierProperty = serializedObject.FindProperty("_customRunAnimationMultiplier");
    }

    public List<DirectoryInfo> FindAllDirs(string path)
    {
        List<DirectoryInfo> ret = new List<DirectoryInfo>();
        DirectoryInfo f = new DirectoryInfo(path);
        if (f.GetDirectories().Length > 0)
        {
            foreach (var item in f.GetDirectories())
            {
                ret.Add(item);
                if (item.GetDirectories().Length > 0)
                {
                    ret.AddRange(FindAllDirs(item.FullName));
                }
            }
        }
        return ret;

    }

    public void OnSceneGUI()
    {
        if (walkPathTarget.newPointCreation || walkPathTarget.oldPointDeleting)
        {
            if (Event.current.type == EventType.MouseMove) SceneView.RepaintAll();
            RaycastHit hit;
            Vector2 mPos = Event.current.mousePosition;
            mPos.y = Screen.height - mPos.y - 40;
            Ray ray = Camera.current.ScreenPointToRay(mPos);

            if (Physics.Raycast(ray, out hit, 3000))
            {
                walkPathTarget.mousePosition = hit.point;

                if ((Event.current.type == EventType.MouseDown && Event.current.button == 0))
                {
                    // создаём новую точку
                    if (walkPathTarget.newPointCreation)
                    {
                        walkPathTarget.AddPoint();
                    }
                    // удаляем старую точку
                    if (walkPathTarget.oldPointDeleting)
                    {
                        walkPathTarget.DeletePoint();
                    }
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PeopleWalkPath walkPath = target as PeopleWalkPath;

        EditorGUILayout.Space();

        GUIStyle boxStyle = new GUIStyle("Box");

        EditorGUILayout.LabelField("People Walking Path", boxStyle, GUILayout.ExpandWidth(true));

        EditorGUILayout.Space();

        DrawDefaultInspector();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_overrideDefaultAnimationMultiplierProperty, new GUIContent("Override Animation Multiplier"));
        EditorGUILayout.PropertyField(_ignorePeopleProperty, new GUIContent("Ignore People"));
        EditorGUILayout.PropertyField(_ignoreCarProperty, new GUIContent("Ignore Car"));
        EditorGUILayout.PropertyField(_ignoreBicycleProperty, new GUIContent("Ignore Bicycle"));

        if (_overrideDefaultAnimationMultiplierProperty.boolValue)
        {
            EditorGUILayout.PropertyField(_customWalkAnimationMultiplierProperty, new GUIContent("Custom walk animation speed"));
            EditorGUILayout.PropertyField(_customRunAnimationMultiplierProperty, new GUIContent("Custom run animation speed"));
        }

        if (_overrideDefaultAnimationMultiplierProperty.boolValue) EditorGUILayout.Space();

        EditorGUILayout.Space();

        GUI.backgroundColor = Color.green;

        if (GUILayout.Button("Populate!"))
        {
            if (walkPath.par != null)
            {
                DestroyImmediate(walkPath.par);
            }

            if (walkPath.walkingPrefabs != null && walkPath.walkingPrefabs.Length > 0 && walkPath.walkingPrefabs[0] != null)
            {
                walkPath.SpawnPeople();
            }
        }

        GUI.backgroundColor = Color.white;

        if (GUILayout.Button("Remove prefabs"))
        {
            if (walkPath.par != null)
            {
                DestroyImmediate(walkPath.par);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        if (walkPath.walkingPrefabs == null || walkPath.walkingPrefabs.Length == 0 || walkPath.walkingPrefabs[0] == null)
            EditorGUILayout.HelpBox("To create a path must be at least 1 walking object prefab", MessageType.Warning);


        if ((walkPathTarget.oldPointDeleting ||
            walkPathTarget.newPointCreation) &&
            GUILayout.Button("Edit Points Finish"))
        {

            walkPathTarget.newPointCreation = false;
            walkPathTarget.oldPointDeleting = false;
            walkPathTarget.EditorLock(false);
        }

        if (!walkPathTarget.newPointCreation &&
            !walkPathTarget.oldPointDeleting)
        {

            if (GUILayout.Button("Add Points"))
            {
                walkPathTarget.newPointCreation = true;
                walkPathTarget.EditorLock(true);
            }

            if (GUILayout.Button("Delete Points"))
            {
                walkPathTarget.oldPointDeleting = true;
                walkPathTarget.EditorLock(true);
            }
            if (!walkPath.disableLineDraw)
            {
                if (GUILayout.Button("HIDE GRAPHICS"))
                {
                    walkPath.disableLineDraw = true;
                    walkPath.HideExistingIcons();
                    return;
                }
            }
            if (walkPath.disableLineDraw)
            {
                if (GUILayout.Button("SHOW GRAPHICS"))
                {
                    walkPath.disableLineDraw = false;
                    walkPath.ShowExistingIcons();
                }
            }
        }

        if (GUILayout.Button("Re-Build Points"))
        {
            Transform parentOfPoints = walkPath.transform.Find("points");

            Transform[] pointsTransform = parentOfPoints.GetComponentsInChildren<Transform>();

            walkPath.pathPoint.Clear();
            walkPath.pathPointTransform.Clear();

            for (int i = 1; i < pointsTransform.Length; i++)
            {
                walkPath.pathPoint.Add(pointsTransform[i].position);
                walkPath.pathPointTransform.Add(pointsTransform[i].gameObject);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}

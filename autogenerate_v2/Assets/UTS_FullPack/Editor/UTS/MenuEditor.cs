using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MenuEditor : MonoBehaviour
{
    [MenuItem("UTS PRO/Create/Vehicles")]
    private static void CreateVehiclePath()
    {
        CreatePath(PathType.VehiclePath);
    }

    [MenuItem("UTS PRO/Create/Bicycles\\Gyro")]
    private static void CreateBcyclesGyroPath()
    {
        CreatePath(PathType.BcyclesGyroPath);
    }

    [MenuItem("UTS PRO/Create/Population/Walking people")]
    private static void CreateWalkingPeople()
    {
        CreatePath(PathType.PeoplePath);
    }

    [MenuItem("UTS PRO/Create/Population/Audience Path")]
    private static void CreateAudiencePath()
    {
        CreatePath(PathType.AudiencePath);
    }

    [MenuItem("UTS PRO/Create/Population/Audience")]
    private static void CreateAudience()
    {
        var populationSystemManager = GetPopulationSystemManager();
        Selection.activeGameObject = populationSystemManager.gameObject;
        ActiveEditorTracker.sharedTracker.isLocked = true;
        populationSystemManager.isConcert = true;
    }

    [MenuItem("UTS PRO/Create/Population/Talking people")]
    private static void CreateTalkingPeople()
    {
        var populationSystemManager = GetPopulationSystemManager();
        Selection.activeGameObject = populationSystemManager.gameObject;
        ActiveEditorTracker.sharedTracker.isLocked = true;
        populationSystemManager.isStreet = true;
    }

    private static void CreatePath(PathType pathType)
    {
        GetPopulationSystemManager();

        GameObject newPath = new GameObject { name = "New path" };
        NewPath newPathComponent = newPath.AddComponent<NewPath>();
        newPathComponent.PathType = pathType;
        Selection.activeGameObject = newPath;
    }

    private static PopulationSystemManager GetPopulationSystemManager()
    {
        if (FindObjectOfType<PopulationSystemManager>() == null)
        {
            string[] managerPrefabs = AssetDatabase.FindAssets("Population System t:Prefab");
            if (managerPrefabs.Length > 0)
            {
                string managerPath = AssetDatabase.GUIDToAssetPath(managerPrefabs[0]);
                PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(managerPath));
            }
        }

        return FindObjectOfType<PopulationSystemManager>();
    }
}
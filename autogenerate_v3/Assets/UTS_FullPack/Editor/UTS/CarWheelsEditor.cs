using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CarWheels))]
public class CarWheelsEditor : Editor
{
    void OnSceneGUI()
    {
        CarWheels cw = target as CarWheels;
        Rigidbody rb = cw.GetComponent<Rigidbody>();
        Handles.color = Color.red;
        Handles.SphereHandleCap(0, cw.transform.TransformPoint(rb.centerOfMass), rb.gameObject.transform.rotation, 0.2f, EventType.Repaint);
    }
    public override void OnInspectorGUI()
    {
        GUI.skin = EditorGUIUtility.GetBuiltinSkin(UnityEditor.EditorSkin.Inspector);
        DrawDefaultInspector();
    }
}

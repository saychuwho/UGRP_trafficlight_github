using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Passersby))]
public class FieldOfViewEditor : Editor
{

   void OnSceneGUI()
    {
        Passersby fow = (Passersby)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position + fow.transform.up, Vector3.up, Vector3.forward, 360, fow.VIEW_RADIUS);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.VIEW_ANGLE / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.VIEW_ANGLE / 2, false);

        Handles.DrawLine(fow.transform.position + fow.transform.up, fow.transform.position + fow.transform.up + viewAngleA * fow.VIEW_RADIUS);
        Handles.DrawLine(fow.transform.position + fow.transform.up, fow.transform.position + fow.transform.up + viewAngleB * fow.VIEW_RADIUS);
    }

}
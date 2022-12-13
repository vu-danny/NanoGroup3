using UnityEngine;
using System.Collections;
using UnityEditor;
using Raph;

[CustomEditor(typeof(AutoAlignAsset))]
public class AutoAlignAssetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AutoAlignAsset myScript = (AutoAlignAsset)target;
        if(GUILayout.Button("Align Object To Ground"))
        {
            myScript.AligntoGroundNormal();
        }
    }
}
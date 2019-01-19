using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GameStructure))]
public class PlatformInspector : Editor
{
    private GameStructure _target;


    private void OnEnable()
    {
        _target = (GameStructure)target;
        if (_target.GetComponent<BoxCollider>())
            _target.GetComponent<BoxCollider>().hideFlags = HideFlags.HideInInspector;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (_target.GetComponent<Collider>() == null)
            return;

        if (_target.width != _target.GetComponent<BoxCollider>().size.x)
        {
            EditorGUILayout.LabelField("The platform size has changed. It need to be rebuilt");
        }

        bool buttonBuild = GUILayout.Button("Build", GUILayout.Height(2 * EditorGUIUtility.singleLineHeight));
        if (buttonBuild)
        {
            _target.Build();
        }
    }
}

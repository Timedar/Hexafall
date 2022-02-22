using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		GridManager gridManager = (GridManager)target;

		if (GUILayout.Button("Generate Map"))
		{
			gridManager.GenerateHexMap();
		}
	}
}

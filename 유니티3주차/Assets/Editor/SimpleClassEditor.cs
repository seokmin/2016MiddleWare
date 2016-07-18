using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

[CustomEditor(typeof(SimpleClass))]
public class SimpleClassEditor : Editor
{
	bool myListFold = false;
	public override void OnInspectorGUI()
	{
		SimpleClass simpleClass = target as SimpleClass;

		simpleClass.showButton = EditorGUILayout.Toggle("Show Toggle", simpleClass);
		if (GUILayout.Button("Show button") == true)
			Debug.Log("========== Show Button =========");

		SerializedProperty iter = serializedObject.FindProperty("level");
		EditorGUILayout.PropertyField(iter);
		serializedObject.ApplyModifiedProperties();

		if (GUILayout.Button("CreateFile"))
		{
			string dataPath = Application.dataPath;
			string fullPath = dataPath + "/test.txt";

			FileStream fs = new FileStream(fullPath, FileMode.Create);
			TextWriter textWriter = new StreamWriter(fs);

			textWriter.Write("width : 10" + "\n");

			textWriter.Close();

			//copy
			string copyPath = dataPath + "/test_copy.txt";
			FileUtil.CopyFileOrDirectory(fullPath, copyPath);
			AssetDatabase.Refresh();

		}


		simpleClass.weight = EditorGUILayout.FloatField("Weight", simpleClass.weight);
		simpleClass.nickName = EditorGUILayout.TextField("Nick name", simpleClass.nickName);

		simpleClass.heroType = (HEROTYPE)EditorGUILayout.EnumPopup("HeroType", simpleClass.heroType);

		simpleClass.mainCameraObject = (GameObject)EditorGUILayout.ObjectField("Camera Object", simpleClass.mainCameraObject, typeof(GameObject), false);
		simpleClass.myTransform = (Transform)EditorGUILayout.ObjectField("My Transform", simpleClass.myTransform, typeof(Transform), true);

		if (myListFold = EditorGUILayout.Foldout(myListFold, "My List"))
		{
			MyEditorUtility.DrawSeparator(Color.red);
			for (int i = 0; i < simpleClass.arrayVector3.Length; ++i)
				simpleClass.arrayVector3[i] = EditorGUILayout.Vector3Field("Vector 3", simpleClass.arrayVector3[i]);
			MyEditorUtility.DrawSeparator(Color.blue);
		}
	}
	public static void RecordObject(Object obj, string name)
	{
		if(obj != null)
		{
			Undo.RecordObject(obj, name);
			EditorUtility.SetDirty(obj);
		}
	}
}

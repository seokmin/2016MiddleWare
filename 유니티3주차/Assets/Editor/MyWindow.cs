using UnityEngine;
using System.Collections;
using UnityEditor;

public class MyWindow : EditorWindow
{
	[MenuItem("MyMenu/Create Window")]
	static void CreateWindow()
	{
		MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
	}

	void OnGUI()
	{
		if (GUILayout.Button("Show button") == true)
			Debug.Log("========== Show Button =========");
	}

	void OnEnable()
	{
		SceneView.onSceneGUIDelegate += SceneGUI;
	}
	void OnDisable()
	{
		SceneView.onSceneGUIDelegate -= SceneGUI;
	}
	enum UIMODE
	{
		MODE1,
		MODE2
	}
	private UIMODE uiMode = UIMODE.MODE1;
	Color arcColor = new Color(1, 1, 1, 0.1f);
	private bool isShow = false;

	void SceneGUI(SceneView sceneView)
	{
		Handles.BeginGUI();
		//		if (GUI.Button(new Rect(10, 10, 100, 50), "Show Window"))
		//		{
		//			MyWindow window =
		//			(MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
		//
		//			window.title = "Menu 1";
		//		}

		if (GUI.Button(new Rect(10, 70, 100, 50), "Dialog Window"))
		{
			if (EditorUtility.DisplayDialog("Warning Message",
						"This is my warning dialog.", "ok", "cancel") == true)
			{
				Debug.Log("Clicked  OK  button");
			}
			else
			{
				Debug.LogWarning("Clicked  Cancel  button");
			}
		}

		switch (uiMode)
		{
			case UIMODE.MODE1:
				if (GUI.Button(new Rect(10, 10, 100, 50), "to MODE2"))
					uiMode = UIMODE.MODE2;
				break;
			case UIMODE.MODE2:
				if (GUI.Button(new Rect(10, 10, 100, 50), "to MODE1"))
					uiMode = UIMODE.MODE1;
				if(GUI.Button(new Rect(170,10,200,50),"Show My Window"))
				{
					Debug.Log("Hello!!!");
					EditorApplication.Beep();
					isShow = !isShow;
				}
				if (isShow)
					GUILayout.Window(0, new Rect(10, 90, 300, 300),
						ShowMyWindow, "Unity Object");

				break;

			default:
				break;
		}
		Handles.EndGUI();

		//MySceneView mySceneView = target as MySceneView;
		//Transform myTransform = mySceneView.transform;

		//Handles.color = arcColor;
		//Handles.DrawSolidArc(myTransform.position, Vector3.up,
		//	myTransform.forward - myTransform.right, 90.0f, 5.0f);
		//Handles.color = Color.white;

		//Handles.color = Color.blue;
		//Handles.ArrowCap(0, myTransform.position, myTransform.rotation, 1.0f);
		//Handles.color = Color.white;


		//Event e = Event.current;
		//if (e.type == EventType.MouseDown)
		//{
		//	if (e.button == 1)
		//	{
		//		arcColor.r = Random.Range(0.0f, 1.0f);
		//		arcColor.g = Random.Range(0.0f, 1.0f);
		//		arcColor.b = Random.Range(0.0f, 1.0f);
		//	}
		//}
		//if (e.isKey)
		//{
		//	switch (e.character)
		//	{
		//		case '1':
		//			EditorApplication.Beep();
		//			break;
		//		default:
		//			break;
		//	}
		//}
	}


	void ShowMyWindow(int windowID)
	{
		GUILayout.Label("TestLabel====");

		GUILayout.BeginHorizontal();
		GUILayout.Button("Button 1");
		GUILayout.Button("Button 2");
		GUILayout.EndHorizontal();

		Rect rect = new Rect(10, 80, 100, 100);
		if (GUI.Button(rect, new Texture()))
			Debug.Log("Click Texture");

		rect.x += 110;
		if (GUI.Button(rect, "Text"))
			Debug.LogWarning("Click TextButton");
	}
}
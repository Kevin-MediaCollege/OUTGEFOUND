using UnityEngine;
using UnityEditor;

namespace Utils
{
	[CustomEditor(typeof(Transform))]
	[CanEditMultipleObjects]
	public class TransformEditor : Editor
	{
		private Transform[] transforms;

		private Transform transform;

		private Vector3 newPosition;
		private Vector3 newRotation;
		private Vector3 newScale;

		protected void OnEnable()
		{
			transforms = new Transform[serializedObject.targetObjects.Length];

			for(int i = 0; i < serializedObject.targetObjects.Length; i++)
			{
				transforms[i] = serializedObject.targetObjects[i] as Transform;
			}

			transform = transforms[0];
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.indentLevel = 0;
			EditorGUIUtility.labelWidth = Screen.width / 5;

			DrawPosition();
			DrawRotation();
			DrawScale();

			if(GUI.changed)
			{
				foreach(Transform transform in transforms)
				{
					UpdateTransform(transform);
				}
			}
		}

		private void UpdateTransform(Transform transform)
		{
			Undo.RecordObject(transform, "Update position");
			transform.localPosition = FixIfNaN(newPosition);

			Undo.RecordObject(transform, "Update rotation");
			transform.localEulerAngles = FixIfNaN(newRotation);

			Undo.RecordObject(transform, "Update scale");
			transform.localScale = FixIfNaN(newScale);
		}

		private void DrawPosition()
		{
			GUIContent resetGUIContent = new GUIContent("R", "Reset the local position of the object.");
			GUIContent positionGUIContent = new GUIContent("Position", "The local position of this Game Object relative to the parent.");

			EditorGUILayout.BeginHorizontal();

			if(GUILayout.Button(new GUIContent(resetGUIContent), GUILayout.Width(20)))
			{
				newPosition = Vector3.zero;
			}
			else
			{
				newPosition = EditorGUILayout.Vector3Field(positionGUIContent, transform.localPosition);
			}

			EditorGUILayout.EndHorizontal();
		}

		private void DrawRotation()
		{
			GUIContent resetGUIContent = new GUIContent("R", "Reset the local rotation of the object.");
			GUIContent rotationGUIContent = new GUIContent("Rotation", "The local rotation of this Game Object relative to the parent.");

			EditorGUILayout.BeginHorizontal();

			if(GUILayout.Button(new GUIContent(resetGUIContent), GUILayout.Width(20)))
			{
				newRotation = Quaternion.identity.eulerAngles;
			}
			else
			{
				newRotation = EditorGUILayout.Vector3Field(rotationGUIContent, transform.localEulerAngles);
			}

			EditorGUILayout.EndHorizontal();
		}

		private void DrawScale()
		{
			GUIContent resetGUIContent = new GUIContent("R", "Reset the local scale of the object.");
			GUIContent scaleGUIContent = new GUIContent("Scale", "The local scaling of this Game Object relative to the parent.");

			EditorGUILayout.BeginHorizontal();

			if(GUILayout.Button(new GUIContent(resetGUIContent), GUILayout.Width(20)))
			{
				newScale = Vector3.one;
			}
			else
			{
				newScale = EditorGUILayout.Vector3Field(scaleGUIContent, transform.localScale);
			}

			EditorGUILayout.EndHorizontal();
		}

		private Vector3 FixIfNaN(Vector3 target)
		{
			if(float.IsNaN(target.x))
			{
				target.x = 0;
			}

			if(float.IsNaN(target.y))
			{
				target.y = 0;
			}

			if(float.IsNaN(target.z))
			{
				target.z = 0;
			}

			return target;
		}
	}
}
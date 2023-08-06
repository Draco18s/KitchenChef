using Assets.draco18s.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.draco18s.Editor
{
	[CustomPropertyDrawer(typeof(SerializableTuple<,>))]
	public class TupleDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = EditorGUIUtility.singleLineHeight;
			position.width /= 2;
			EditorGUI.PropertyField(position, property.FindPropertyRelative("Item1"), GUIContent.none);
			position.x += position.width;
			EditorGUI.PropertyField(position, property.FindPropertyRelative("Item2"), GUIContent.none);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}
	}
}

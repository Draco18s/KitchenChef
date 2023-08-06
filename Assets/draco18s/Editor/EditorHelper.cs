using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class EditorHelper
{
	/// <summary>
	/// Gets the object the property represents.
	/// </summary>
	/// <param name="prop"></param>
	/// <returns></returns>
	public static object GetTargetObjectOfProperty(SerializedProperty prop)
	{
		if (prop == null) return null;

		var path = prop.propertyPath.Replace(".Array.data[", "[");
		object obj = prop.serializedObject.targetObject;
		var elements = path.Split('.');
		foreach (var element in elements)
		{
			if (element.Contains("["))
			{
				var elementName = element.Substring(0, element.IndexOf("["));
				var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
				obj = GetValue_Imp(obj, elementName, index);
			}
			else
			{
				obj = GetValue_Imp(obj, element);
			}
		}
		return obj;
	}

	public static object GetTargetObjectOfProperty(SerializedProperty prop, object targetObj)
	{
		var path = prop.propertyPath.Replace(".Array.data[", "[");
		var elements = path.Split('.');
		foreach (var element in elements)
		{
			if (element.Contains("["))
			{
				var elementName = element.Substring(0, element.IndexOf("["));
				var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
				targetObj = GetValue_Imp(targetObj, elementName, index);
			}
			else
			{
				targetObj = GetValue_Imp(targetObj, element);
			}
		}
		return targetObj;
	}

	private static object GetValue_Imp(object source, string name)
	{
		if (source == null)
			return null;
		var type = source.GetType();

		while (type != null)
		{
			var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			if (f != null)
				return f.GetValue(source);

			var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			if (p != null)
				return p.GetValue(source, null);

			type = type.BaseType;
		}
		return null;
	}

	private static object GetValue_Imp(object source, string name, int index)
	{
		var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
		if (enumerable == null) return null;
		var enm = enumerable.GetEnumerator();
		//while (index-- >= 0)
		//    enm.MoveNext();
		//return enm.Current;

		for (int i = 0; i <= index; i++)
		{
			if (!enm.MoveNext()) return null;
		}
		return enm.Current;
	}

	public static object DrawDefaultField(Rect position, GUIContent label, object obj, Type objType)
	{
		if (objType == null) return obj;
		
		if (objType == typeof(Vector3))
		{
			if (obj == null) obj = Vector3.zero;
			return EditorGUI.Vector3Field(position, label, (Vector3)obj);
		}
		if (objType == typeof(Vector2))
		{
			if (obj == null) obj = Vector2.zero;
			return EditorGUI.Vector2Field(position, label, (Vector2)obj);
		}
		return obj;
	}
}

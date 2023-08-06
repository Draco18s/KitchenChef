using System;
using System.Collections;
using System.Collections.Generic;
using Assets.draco18s.util;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
public class DictionaryEditor : PropertyDrawer
{
	private ReorderableList list;
	private SerializedProperty key;

	/// <summary>
	/// This is such a pile of hacky trash all because Unity can't figure out how to serialize dictionaries.
	/// I swear someone created a better property drawer for dictionaries than this before, might've even been me.
	/// But wherever that code is now, I have no idea. This works...well enough though.
	/// </summary>
	/// <param name="position"></param>
	/// <param name="property"></param>
	/// <param name="label"></param>
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		object o = EditorHelper.GetTargetObjectOfProperty(property);
		IDictionary dict = (IDictionary)o;
		if (list == null || key != property)
		{
			Type typeK = o.GetType().GetGenericArguments()[0];
			Type typeV = o.GetType().GetGenericArguments()[1];
			List<object> l = new List<object>();
			foreach (var j in dict.Keys)
			{
				l.Add(j);
			}

			list = new ReorderableList(l, typeK)
			{
				drawHeaderCallback = rect => { EditorGUI.LabelField(rect, label); },
				drawElementCallback = (rect, index, active, focused) =>
				{
					var k = l[index];
					var v = dict[k];
					rect.width /= 2;
					l[index] = EditorGUI.ObjectField(rect, GUIContent.none, k as UnityEngine.Object, typeK, false);

					if (l[index] != k)
					{
						dict.Remove(k);
						dict.Add(l[index], v);
						k = l[index];
					}

					rect.x += rect.width;
					dict[k] = EditorHelper.DrawDefaultField(rect, GUIContent.none, v, typeV);
				},
				onCanAddCallback = reorderableList => true,
				onAddCallback = reorderableList =>
				{
					ScriptableObject k = ScriptableObject.CreateInstance(typeK);
					dict.Add(k, Activator.CreateInstance(typeV));
				},
				onRemoveCallback = reorderableList =>
				{
					foreach (int index in reorderableList.selectedIndices)
					{
						var k = l[index];
						dict.Remove(k);
					}
				}
			};
		}
		list.DoList(position);
		property.serializedObject.ApplyModifiedProperties();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return list == null ? 0 : list.GetHeight();
	}
}

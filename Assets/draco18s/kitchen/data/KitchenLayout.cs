using System;
using System.Collections;
using System.Collections.Generic;
using Assets.draco18s.kitchen;
using Assets.draco18s.util;
using UnityEngine;

namespace Assets.draco18s.kitchen
{
	[CreateAssetMenu(fileName = "Layout", menuName = "Kitchen/Layout", order = 1)]
	public class KitchenLayout : ScriptableObject
	{
		public List<SerializableTuple<KitchenItem, Vector2>> KitchenObjects;

		public static Vector3 GetWorldPos(Vector2 placement)
		{
			return new Vector3(placement.y, 0, placement.x) * 2.5f;
		}
	}
}
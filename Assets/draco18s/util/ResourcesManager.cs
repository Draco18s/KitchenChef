using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.draco18s.util
{
	public class ResourcesManager : MonoBehaviour
	{
		public static ResourcesManager instance;

		public ScriptableObject[] ScriptableObjects;

		[UsedImplicitly]
		void Start()
		{
			instance = this;
		}

		public List<T> GetAssetsMatching<T>(Func<T,bool> predicate) where T : ScriptableObject
		{
			List<T> retObjs = new List<T>();
			foreach (ScriptableObject obj in ScriptableObjects)
			{
				if(obj is T o && predicate(o))
					retObjs.Add(o);
			}

			return retObjs;
		}
	}
}
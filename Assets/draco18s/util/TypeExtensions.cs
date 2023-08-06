using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.draco18s.util {
	public static class TypeExtensions {
		public static bool IsArrayOf<T>(this Type type) {
			return type == typeof(T[]);
		}

		public static IEnumerable<string> ChunksUpto(this string str, int maxChunkSize) {
			for(int i = 0; i < str.Length; i += maxChunkSize)
				yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
		}

		public static Transform Clear(this Transform transform) {
			foreach(Transform child in transform) {
				GameObject.Destroy(child.gameObject);
			}
			return transform;
		}

		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> predicate) {
			foreach(T t in enumerable) {
				predicate(t);
			}
		}
		
		public static T IfDefaultGiveMe<T>(this T value, T alternate)
		{
			if (value.Equals(default(T))) return alternate;
			return value;
		}

		public static Vector3 ReplaceX(this Vector3 v1, float nx)
		{
			return new Vector3(nx, v1.y, v1.z);
		}

		public static Vector3 ReplaceY(this Vector3 v1, float ny)
		{
			return new Vector3(v1.x, ny, v1.z);
		}

		public static Vector3 ReplaceZ(this Vector3 v1, float nz)
		{
			return new Vector3(v1.x, v1.y, nz);
		}

		/*public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = RandomExtensions.Shared.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }*/
	}
}

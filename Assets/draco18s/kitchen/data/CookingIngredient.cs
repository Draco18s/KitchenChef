using System;
using UnityEngine;

namespace Assets.draco18s.kitchen
{
	[CreateAssetMenu(fileName = "Item", menuName = "Kitchen/Ingredient", order = 1)]
	public class CookingIngredient : ScriptableObject
	{
		[Flags]
		public enum ProcessType
		{
			None,Chop,Cook
		}
		public GameObject prefab;
		public CookingIngredient resultWhenChopped;
		public CookingIngredient resultWhenCooked;
		public ProcessType processType;
		public float processingTime;
		public int scoreValue;
		public Sprite sprite;
	}
}
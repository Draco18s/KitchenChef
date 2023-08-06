using System.Collections.Generic;
using Assets.draco18s.util;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.draco18s.kitchen
{
	public class KitchenSetup : MonoBehaviour
	{
		public static KitchenSetup instance;
		public KitchenLayout Layout;
		public Transform container;
		public List<CookingIngredient> RawIngredients = new List<CookingIngredient>();
		public List<CookingIngredient> FinalIngredients = new List<CookingIngredient>();

		public int TotalScore = 0;

		[UsedImplicitly]
		void Start()
		{
			instance = this;
			foreach (SerializableTuple<KitchenItem, Vector2> kvp in Layout.KitchenObjects)
			{
				GameObject go = Instantiate(kvp.Item1.prefab, KitchenLayout.GetWorldPos(kvp.Item2), Quaternion.identity, container);
				KitchenSupply sup = go.GetComponent<KitchenSupply>();
				if (sup != null)
				{
					sup.RawIngredients.AddRange(RawIngredients);
				}
			}
		}

		public static RecipeResult GetCustomerDesire()
		{
			RecipeResult result = new RecipeResult();
			foreach (CookingIngredient ingred in instance.FinalIngredients)
			{
				result.recipe.Add(ingred);
			}

			while ((result.recipe.Count > 3 || Random.value > 0.5) && result.recipe.Count > 2)
			{
				result.recipe.RemoveAt(Mathf.FloorToInt(Random.value * result.recipe.Count));
			}
			return result;
		}
	}
}
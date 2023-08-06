using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.draco18s.kitchen
{
	public class RecipeResult : IReadOnlyCollection<CookingIngredient>
	{
		public List<CookingIngredient> recipe = new List<CookingIngredient>();

		public int Count => recipe.Count;

		public CookingIngredient this[int index] => recipe[index];
		public IEnumerator<CookingIngredient> GetEnumerator()
		{
			return recipe.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return recipe.GetEnumerator();
		}
	}
}

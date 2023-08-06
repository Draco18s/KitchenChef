using Assets.draco18s.kitchen;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.draco18s.kitchen
{
	public class DeliveryTimer : MonoBehaviour
	{
		public TextMeshProUGUI text;
		public Image[] sprite;

		public void SetRecipe(RecipeResult recipe)
		{
			int i;
			for (i = 0; recipe != null && i < recipe.Count; i++)
			{
				sprite[i].sprite = recipe[i].sprite;
				sprite[i].gameObject.SetActive(true);
			}

			for (; i < sprite.Length; i++)
			{
				sprite[i].gameObject.SetActive(false);
			}
		}
	}
}
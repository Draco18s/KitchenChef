using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.draco18s.kitchen
{
	public class KitchenSupply : MonoBehaviour, IInteractable
	{
		public List<CookingIngredient> RawIngredients = new List<CookingIngredient>();

		[SerializeField] private SpriteRenderer m_frame;
		public SpriteRenderer frame => m_frame;
		public SpriteRenderer icon;
		private int index = 0;
		private float interactDelayTime = 0.2f;
		private float interactDelay = 0f;

		[UsedImplicitly]
		void Start()
		{
			icon.sprite = RawIngredients[index].sprite;
		}

		[UsedImplicitly]
		void Update()
		{
			interactDelay -= Time.deltaTime;
		}

		public GameObject GetItem(CookingIngredient ingred)
		{
			GameObject go = Instantiate(ingred.prefab);
			CarryableIngredient ci = go.AddComponent<CarryableIngredient>();
			ci.ingredientType = ingred;
			return go;
		}

		public void Highlight(bool on)
		{
			frame.enabled = on;
		}

		public Transform PickupPlace(Transform carriedObject)
		{
			if(carriedObject != null) return carriedObject;
			return GetItem(RawIngredients[index]).transform;
		}

		public void Interact()
		{
			if(interactDelay > 0) return;
			interactDelay = interactDelayTime;
			index = (index + 1) % RawIngredients.Count;
			icon.sprite = RawIngredients[index].sprite;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.draco18s.kitchen.ui;
using Assets.draco18s.util;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.draco18s.kitchen
{
	public class DeliveryWindow : MonoBehaviour, IInteractable
	{
		[SerializeField] private SpriteRenderer m_frame;
		public DeliveryTimer timerUi;
		public SpriteRenderer frame => m_frame;
		public Light light_green;
		public Light light_yellow;
		public Light light_red;
		public Transform[] snapSpots;

		public RecipeResult customerDesire;

		private int score;
		private int index;
		private float waitTime;
		private float requestTime;
		private float requestTimeRand;

		[UsedImplicitly]
		void Start()
		{
			requestTimeRand = 0.0001f;
		}

		[UsedImplicitly]
		void Update()
		{
			if (ScoreKeeper.instance.state != ScoreKeeper.GameState.InGame)
			{
				if (customerDesire != null)
				{
					Clear();
				}
				return;
			}
			if (customerDesire == null)
			{
				timerUi.text.text = "";
				timerUi.SetRecipe(null);
				waitTime = 0;
				requestTime += Time.deltaTime;
				light_green.enabled = false;
				light_yellow.enabled = false;
				light_red.enabled = false;
				if (requestTime > requestTimeRand)
				{
					requestTimeRand = 5;
					customerDesire = KitchenSetup.GetCustomerDesire();
					timerUi.SetRecipe(customerDesire);
				}
				return;
			}

			requestTime = 0;
			waitTime += Time.deltaTime;
			timerUi.text.text = TimeWaiting();
			if (waitTime < 15)
			{
				light_green.enabled = true;
				light_yellow.enabled = false;
				light_red.enabled = false;
			}
			else if (waitTime < 30)
			{
				light_green.enabled = false;
				light_yellow.enabled = true;
				light_red.enabled = false;
			}
			else
			{
				light_green.enabled = false;
				light_yellow.enabled = false;
				light_red.enabled = true;
			}
		}

		void Clear()
		{
			foreach (Transform t in snapSpots)
			{
				t.Clear();
			}

			index = 0;
			customerDesire = null;
			timerUi.SetRecipe(null);
		}

		string TimeWaiting()
		{
			return customerDesire == null ? "" : Mathf.FloorToInt(waitTime).ToString();
		}

		public void Highlight(bool on)
		{
			frame.enabled = on;
		}

		public Transform PickupPlace(Transform carriedObject)
		{
			if (carriedObject == null) return carriedObject;

			CarryableIngredient ingred = carriedObject.GetComponent<CarryableIngredient>();
			CookingIngredient itm = customerDesire.FirstOrDefault(x => x == ingred.ingredientType);
			if (itm != null)
			{
				score += ingred.ingredientType.scoreValue;
				carriedObject.SetParent(snapSpots[index++]);
				carriedObject.localPosition = Vector3.zero;
				customerDesire.recipe.Remove(itm);
				timerUi.SetRecipe(customerDesire);
				if (customerDesire.Count == 0)
				{
					ScoreKeeper.instance.UpdateScore(score - Mathf.FloorToInt(waitTime));
					score = 0;
					Clear();
				}

				return null;
			}

			return carriedObject;
		}

		public void Interact()
		{

		}
	}
}

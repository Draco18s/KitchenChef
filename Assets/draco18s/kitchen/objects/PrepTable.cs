using UnityEngine;
using static Assets.draco18s.kitchen.CookingIngredient;

namespace Assets.draco18s.kitchen
{
	public class PrepTable : MonoBehaviour, IInteractable
	{
		public Transform snapPoint;
		[SerializeField] private SpriteRenderer m_frame;
		public SpriteRenderer frame => m_frame;

		private CarryableIngredient currentIngredient;
		private float chopTime = 0;
		
		public CarryableIngredient GetResult(CookingIngredient ingred)
		{
			if (ingred.processType != ProcessType.Chop) return currentIngredient;
			GameObject go = Instantiate(ingred.resultWhenChopped.prefab, snapPoint);
			go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
			CarryableIngredient ci = go.AddComponent<CarryableIngredient>();
			ci.ingredientType = ingred.resultWhenChopped;
			return ci;
		}

		public void Highlight(bool on)
		{
			frame.enabled = on;
		}

		public Transform PickupPlace(Transform carriedObject)
		{
			if (carriedObject != null && currentIngredient == null)
			{
				CarryableIngredient ingred = carriedObject.GetComponent<CarryableIngredient>();
				if (ingred.ingredientType.processType.HasFlag(CookingIngredient.ProcessType.Chop))
				{
					chopTime = ingred.ingredientType.processingTime;
					currentIngredient = ingred;
					currentIngredient.transform.SetParent(snapPoint);
					currentIngredient.transform.localPosition = Vector3.zero;
					return null;
				}
			}
			else if (currentIngredient != null)
			{
				Transform t = currentIngredient.transform;
				currentIngredient = null;
				return t;
			}

			return carriedObject;
		}

		public void Interact()
		{
			if (currentIngredient == null || chopTime < 0 || !currentIngredient.ingredientType.processType.HasFlag(ProcessType.Chop)) return;
			chopTime -= Time.deltaTime;
			if (chopTime <= 0)
			{
				GameObject c = currentIngredient.gameObject;
				currentIngredient = GetResult(currentIngredient.ingredientType);
				Destroy(c);
				chopTime = -1;
			}
		}
	}
}

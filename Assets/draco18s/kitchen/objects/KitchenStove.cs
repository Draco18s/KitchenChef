using Assets.draco18s.ui;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.draco18s.kitchen
{
	public class KitchenStove : MonoBehaviour, IInteractable
	{
		[SerializeField] private SpriteRenderer m_frame;
		public SpriteRenderer frame => m_frame;
		public Light stove_light;
		public ParticleSystem flames;
		public Transform snapPoint;
		public GameObject progressBarPrefab;
		private GameObject progressBarInstance;
		private PercentageBar progressBar;

		private CarryableIngredient currentIngredient;
		private float cookTime = 0;

		[UsedImplicitly]
		void Start()
		{
			Transform canvas = FindObjectOfType<Canvas>().transform;
			progressBarInstance = Instantiate(progressBarPrefab, canvas);
			progressBarInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up*40;
			progressBar = progressBarInstance.GetComponent<PercentageBar>();
			TurnOn(false);
		}

		[UsedImplicitly]
		void Update()
		{
			if (currentIngredient == null || cookTime < 0) return;
			cookTime -= Time.deltaTime;
			progressBar.current = (progressBar.total - cookTime);
			if (cookTime <= 0)
			{
				GameObject c = currentIngredient.gameObject;
				currentIngredient = GetResult(currentIngredient.ingredientType);
				currentIngredient.transform.localPosition = Vector3.zero;
				currentIngredient.transform.localRotation = Quaternion.identity;
				Destroy(c);
				cookTime = -1;
				TurnOn(false);
			}
		}

		private void TurnOn(bool on)
		{
			ParticleSystem.EmissionModule em = flames.emission;
			em.enabled = on;
			stove_light.enabled = on;
			progressBarInstance.SetActive(on);
		}

		public CarryableIngredient GetResult(CookingIngredient ingred)
		{
			GameObject go = Instantiate(ingred.resultWhenCooked.prefab, Vector3.zero, Quaternion.identity, snapPoint);
			CarryableIngredient ci = go.AddComponent<CarryableIngredient>();
			ci.ingredientType = ingred.resultWhenCooked;
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
				if (ingred.ingredientType.processType.HasFlag(CookingIngredient.ProcessType.Cook))
				{
					progressBar.total = cookTime = ingred.ingredientType.processingTime;
					currentIngredient = ingred;
					currentIngredient.transform.SetParent(snapPoint);
					currentIngredient.transform.localPosition = Vector3.zero;
					currentIngredient.transform.localRotation = Quaternion.identity;
					TurnOn(true);
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

		}
	}
}

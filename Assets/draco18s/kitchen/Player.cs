using JetBrains.Annotations;
using StarterAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.draco18s.kitchen.ui;
using UnityEngine;
using UnityEngine.Windows;

namespace Assets.draco18s.kitchen
{
	public class Player : MonoBehaviour
	{
		public Transform targetPoint;
		public Transform carriedObject;
		public IInteractable target;
		private StarterAssetsInputs _input;

		[UsedImplicitly]
		void Start()
		{
			_input = GetComponent<StarterAssetsInputs>();

		}

		private float interactDelayTime = 0.2f;
		private float interactDelay = 0f;

		[UsedImplicitly]
		void Update()
		{
			if (ScoreKeeper.instance.state != ScoreKeeper.GameState.InGame) return;
			interactDelay -= Time.deltaTime;
			if (_input.pickup && target != null && interactDelay <= 0)
			{
				interactDelay = interactDelayTime;
				carriedObject = target.PickupPlace(carriedObject);
				if (carriedObject == null) return;
				carriedObject.transform.SetParent(targetPoint);
				carriedObject.transform.localPosition = Vector3.zero;
			}

			if (_input.interact && target != null)
			{
				var thisIsDumb = (target == null).ToString();
				target.Interact();
			}
		}

		public void InteractionEnter(Collider other)
		{
			IInteractable t = other.GetComponent<IInteractable>();
			if (t != null)
			{
				target?.Highlight(false);
				target = t;
				target.Highlight(true);
			}
		}

		public void InteractionExit(Collider other)
		{
			IInteractable t = other.GetComponent<IInteractable>();
			if (t != null)
			{
				t.Highlight(false);
				target = null;
			}
		}

		public void Reset()
		{
			Destroy(carriedObject);
			transform.rotation = Quaternion.identity;
			transform.localPosition = new Vector3(10, 0, 3.15f);
		}
	}
}

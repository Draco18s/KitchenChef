using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.draco18s.kitchen.objects
{
	public class TrashCan : MonoBehaviour, IInteractable
	{
		[SerializeField] private SpriteRenderer m_frame;
		public SpriteRenderer frame => m_frame; 
		public void Highlight(bool on)
		{
			frame.enabled = on;
		}

		public Transform PickupPlace(Transform carriedObject)
		{
			if(carriedObject != null) Destroy(carriedObject.gameObject);
			return null;
		}

		public void Interact()
		{

		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.draco18s.kitchen
{
	public interface IInteractable
	{
		SpriteRenderer frame { get; }
		void Highlight(bool on);
		Transform PickupPlace(Transform carriedObject);
		void Interact();
	}
}

using UnityEngine;

namespace Assets.draco18s.kitchen
{
	[CreateAssetMenu(fileName = "Item", menuName = "Kitchen/Object", order = 1)]
	public class KitchenItem : ScriptableObject
	{
		public enum KitchenItemType
		{
			Unknown,Table,Stove,Refrigerator,Trash,
		}
		public GameObject prefab;
		public KitchenItemType type;
	}
}

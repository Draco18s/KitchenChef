using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.draco18s.util
{
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{

		[SerializeField] private List<TKey> keys = new List<TKey>();
		[SerializeField] private List<TValue> values = new List<TValue>();

		// save the dictionary to lists
		public void OnBeforeSerialize()
		{
			keys = new List<TKey>();
			values = new List<TValue>();
			foreach (KeyValuePair<TKey, TValue> pair in this)
			{
				keys.Add(pair.Key);
				values.Add(pair.Value);
			}
		}

		// load dictionary from lists
		public void OnAfterDeserialize()
		{
			this.Clear();

			//if (keys.Count != values.Count)
			//	throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));
			/*while (keys.Count != values.Count)
			{
				if(keys.Count > values.Count)
					values.Add(default(TValue));
				if (keys.Count < values.Count)
					keys.Add(default(TKey));
			}*/

			for (int i = 0; i < keys.Count; i++)
				this.Add(keys[i], values[i]);
		}

		public void CopyFrom(SerializableDictionary<TKey, TValue> other)
		{
			keys.Clear();
			values.Clear();
			this.Clear();
			foreach (KeyValuePair<TKey, TValue> pair in other)
			{
				keys.Add(pair.Key);
				values.Add(pair.Value);
				this.Add(pair.Key, pair.Value);
			}
		}

		public void Callback()
		{
			this.Add(default,default);
		}
	}
}
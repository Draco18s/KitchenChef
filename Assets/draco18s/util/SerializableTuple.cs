using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.draco18s.util
{
	[Serializable]
	public class SerializableTuple<T,U> : Tuple<T,U>
	{
		[SerializeField] public new T Item1;
		[SerializeField] public new U Item2;

		public SerializableTuple() : base(default, default)
		{

		}

		public SerializableTuple(T item1, U item2) : base(item1, item2)
		{

		}
	}
}

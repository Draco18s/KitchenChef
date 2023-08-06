using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.draco18s.util
{
	[Serializable]
	public class TriValue
	{
		[NotNull] public static readonly TriValue Default = new TriValue(0);
		[NotNull] public static readonly TriValue True = new TriValue(1);
		[NotNull] public static readonly TriValue False = new TriValue(-1);

		[SerializeField] private int v;

		public TriValue Clone()
		{
			return new TriValue(v);
		}

		private TriValue(int i)
		{
			v = i;
		}

		protected bool Equals(TriValue other)
		{
			return v == other?.v;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((TriValue)obj);
		}

		public override int GetHashCode()
		{
			return v;
		}

		public static implicit operator int(TriValue a) => a.v;

		public static implicit operator TriValue(int a)
		{
			if (a == -1) return False.Clone();
			if (a == 1) return True.Clone();
			if (a == 0) return Default.Clone();
			return new TriValue(a);
		}

		public static bool operator ==(TriValue a, TriValue b)
		{
			return a?.Equals(b) ?? (b?.Equals(null) ?? true);
		}

		public static bool operator !=(TriValue a, TriValue b)
		{
			if (a == null) return b != null;
			return !a.Equals(b);
		}

		public static bool operator ==(TriValue a, bool b) {
			return (a == True && b) || (a == False && !b);
		}

		public static bool operator !=(TriValue a, bool b)
		{
			return (a == True && !b) || (a == False && b);
		}

		public static bool operator true(TriValue x) => (x?.v ?? 0) > 0;
		public static bool operator false(TriValue x) => (x?.v ?? 0) < 0;

		public static TriValue operator |(TriValue a, TriValue b)
		{
			if (a == Default) return b.Clone();

			if (b == Default) return a.Clone();

			if (a == True && b == True) return True.Clone();
			if (a == False && b == False) return False.Clone();

			return Default.Clone();
		}

		public static TriValue operator &(TriValue a, TriValue b)
		{
			if (a == True && b == True) return True.Clone();
			if (a == False) return False.Clone();
			if (b == False) return False.Clone();

			return Default;
		}

		public override string ToString()
		{
			if (v == 1) return "True";
			if(v == -1) return "False";
			return "Default";
		}
	}
}
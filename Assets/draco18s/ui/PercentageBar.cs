using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.draco18s.ui
{
	[DisallowMultipleComponent]
	public class PercentageBar : MonoBehaviour
	{
		[SerializeField] private float m_current;
		[SerializeField] private float m_total;

		public float current
		{
			get => m_current;
			set
			{
				m_current = Mathf.Clamp(value, 0, total);
				UpdateFill();
			}
		}

		public float total
		{
			get => m_total;
			set
			{
				m_total = value;
				UpdateFill();
			}
		}

		public float percentage => total == 0 ? 0 : current / total;
		[SerializeField] private RectTransform fill;

		private RectTransform rectTransform;

		private void UpdateFill()
		{
			if (rectTransform == null) return;
			fill.sizeDelta = new Vector2(percentage * rectTransform.sizeDelta.x, 0);
		}

		[UsedImplicitly]
		private void Start()
		{
			rectTransform = (RectTransform)transform;
			Invoke("UpdateFill", 0.01f);
		}
	}
}

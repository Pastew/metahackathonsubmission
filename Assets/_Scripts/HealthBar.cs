using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
	public class HealthBar : MonoBehaviour
	{
		[SerializeField] private Slider slider;

		public void SetHealthSlider(float health)
		{
			slider.value = health;
		}
	}
}

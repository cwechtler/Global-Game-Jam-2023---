using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
	public PlayerHealth health;
	public Slider slider;

	void Start()
	{
		slider.maxValue = health.health;
	}

	// Update is called once per frame
	void Update()
    {
		slider.value = health.health;
    }
}

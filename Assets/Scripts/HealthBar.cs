﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Slider slider;
	
	public void SetMaxHealth (int health) {
		slider.value = health;
		slider.maxValue = health;
	}
	
    // Start is called before the first frame update
    public void SetHealth (int health) {
    	slider.value = health;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private IHealth _playerHealth;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _playerHealth = GameObject.Find("Player").GetComponentInChildren<IHealth>();
    }

    private void LateUpdate()
    {
        _image.fillAmount = (float)_playerHealth.GetHealth()/_playerHealth.GetMaxHealth();
    }
}

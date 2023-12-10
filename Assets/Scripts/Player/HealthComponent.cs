using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    #region EVENTS

    public event Action OnDecrease_Health;
    public event Action OnIncrease_Health;
    public event Action OnInsufficient_Health;
    public event Action OnHealthChanged;

    #endregion

    #region EXPOSED_FIELDS

    [field: SerializeField] public float _health { get; set; }
    [field: SerializeField] public float _maxHealth { get; set; }

    #endregion

    #region UNITY_CALLS

    private void OnEnable()
    {
        ResetFullHealth();
    }
    
    private void OnDisable()
    {
        _health = 0;
    }

    #endregion

    #region PUBLIC_METHODS

    public void ResetFullHealth()
    {
        _health = _maxHealth;
    }

    /// <summary>
    /// Decrease The Health Variable for the Characters
    /// </summary>
    /// <param name="harm_Value"></param>
    public void DecreaseHealth(float harm_Value)
    {
        _health -= harm_Value;
        OnDecrease_Health?.Invoke();
        CheckHealth();
    }

    /// <summary>
    /// Increase The Health Variable for the Characters
    /// </summary>
    public void IncreaseHealth(float health_Value)
    {
        _health += health_Value;

        if (_health > _maxHealth)
            _health = _maxHealth;
        OnIncrease_Health?.Invoke();
        CheckHealth();
    }

    /// <summary>
    /// Checks If The Character Health is Greater or Equals Than 0
    /// </summary>
    public void CheckHealth()
    {
        OnHealthChanged?.Invoke();
        if (_health <= 0)
            OnInsufficient_Health?.Invoke();
    }

    #endregion
}
/*  Filename:           HealthDisplay.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 18, 2022
 *  Description:        Display health in a slider, which can be used for enemies, towers and player.
 *  Revision History:   June 18, 2022 (Yuk Yee Wong): Initial script.
 */

/// <summary>
/// This is different from the PlayerHealthBarController.cs, 
/// as it does not need to demostrate the effect of damage by 
/// 1. clicking the key K and 
/// 2. changing the slider value manually 
/// </summary>
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    protected int currentHealthValue;
    protected int maxHealthValue;

    [SerializeField] private Slider hpSlider;

    public int CurrentHealthValue { get { return currentHealthValue; } }
    public int MaxHealthValue { get { return maxHealthValue; } }

    public void Init(int maxHealthValue)
    {
        this.maxHealthValue = maxHealthValue;
        ResetHealth();
    }

    public void SetHealthValue(int health)
    {
        currentHealthValue = health;
        UpdateHpBar();
        CheckZeroHealth();
    }

    public void ResetHealth()
    {
        hpSlider.maxValue = maxHealthValue;
        hpSlider.minValue = 0;
        currentHealthValue = maxHealthValue;
        UpdateHpBar();
    }

    public void UpdateHpBar()
    {
        hpSlider.value = currentHealthValue;
    }

    public void TakeDamage(int damage)
    {
        ReduceHealth(damage);
        CheckZeroHealth();
        UpdateHpBar();
    }

    private void CheckZeroHealth()
    {
        if (currentHealthValue <= 0)
        {
            currentHealthValue = 0;
            ReachZeroHealth();
        }
    }

    public virtual void ReduceHealth(int damage)
    {
        currentHealthValue -= damage;
    }

    public virtual void ReachZeroHealth() { }
}
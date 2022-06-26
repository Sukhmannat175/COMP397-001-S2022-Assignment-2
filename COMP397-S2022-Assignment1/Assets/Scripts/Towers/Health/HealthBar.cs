using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image fill;

    [SerializeField]
    private float positionOffset;

    private Health health;



    public void SetHealth(Health health)
    {
        this.health = health;
        health.OnHealthUpdated += UpdateHealth;
    }

    private void UpdateHealth(float fillAmount)
    {
        fill.fillAmount = fillAmount;
    }

    public void SetBuildingTime(Health health)
    {
        this.health = health;
        health.OnBuildingTimeUpdated += UpdateHealth;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(health.transform.position + Vector3.up * positionOffset);
    }

    private void OnDestroy()
    {
        //deregisters this so it no longer updates
        health.OnHealthUpdated -= UpdateHealth;
        health.OnBuildingTimeUpdated -= UpdateHealth;
    }



}

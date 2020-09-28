using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    GameObject health;

    private float currentHealth = 1f;
    
    void Start()
    {
        health.transform.localScale = new Vector3(currentHealth,1f);
    }

    public void takeDamage(float damage)
    {
        health.transform.localScale = new Vector3(currentHealth-damage,1f);
    }
}

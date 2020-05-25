using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth = 6;
    public IntReference currentHealth;
    public bool isInvulnerable = false;

    public UnityEvent AfterInitialised;
    public UnityEvent OnHeal;
    public UnityEvent OnDamage;
    public UnityEvent OnDeath;

    void Start()
    {
        currentHealth.Value = maxHealth;
        if (AfterInitialised != null)
            AfterInitialised.Invoke();
    }

    public void ChangeByAmount(int amount)
    {
        if (amount > 0 && currentHealth.Value != maxHealth && OnHeal != null)
            OnHeal.Invoke();
        if (amount < 0 && OnDamage != null)
        {
            if (isInvulnerable)
                return;

            OnDamage.Invoke();
        }

        currentHealth.Value = Mathf.Clamp(currentHealth.Value + amount, 0, maxHealth);

        if (currentHealth.Value == 0 && OnDeath != null)
            OnDeath.Invoke();
    }
}

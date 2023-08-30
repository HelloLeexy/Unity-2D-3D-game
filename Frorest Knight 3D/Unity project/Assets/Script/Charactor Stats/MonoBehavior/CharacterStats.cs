using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action<int, int> UpdateHealthBarOnAttcak;

    public Character_SO templateData;

    public Character_SO characterData;

    public AttackData_SO attackData;

   

    void Awake()
    {
        if(templateData != null)
        {
            characterData = Instantiate(templateData);
        }
    }

    public int MaxHealth 
    {
        get
        {
            if (characterData != null)
            {
                return characterData.maxHealth;
            }
            else
            {
                return 0;

            }
        }
        set
        {
            characterData.maxHealth = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            if (characterData != null)
            {
                return characterData.currentHealth;
            }
            else
            {
                return 0;

            }
        }
        set
        {
            characterData.currentHealth = value;
        }
    }

    public int BaseDefence
    {
        get
        {
            if (characterData != null)
            {
                return characterData.baseDefence;
            }
            else
            {
                return 0;

            }
        }
        set
        {
            characterData.baseDefence = value;
        }
    }
    public int CurrentDefence
    {
        get
        {
            if (characterData != null)
            {
                return characterData.currentDefence;
            }
            else
            {
                return 0;

            }
        }
        set
        {
            characterData.currentDefence = value;
        }
    }

    public void TakeDamage(CharacterStats attacker, CharacterStats defener)
    {
        int damage = Math.Abs(attacker.CurrentDamage() - defener.CurrentDefence);
        CurrentHealth = Math.Max(CurrentHealth - damage, 0);
        
        UpdateHealthBarOnAttcak?.Invoke(CurrentHealth, MaxHealth);


    }

    private int CurrentDamage()
    {
        float coreDamage = attackData.maxDamage;

        return (int)coreDamage;
    }
}

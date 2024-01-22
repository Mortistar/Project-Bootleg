using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public float health {get; private set;}
    public float healthMax {get; private set;}
    public float attack {get; private set;}
    public int bombs {get; private set;}

    public PlayerStats()
    {
        health = 100;
        healthMax = 100;
        attack = 10;
        bombs = 3;
    }
    public void GainHealth(float value)
    {
        health = Mathf.Clamp(health + value, 0, healthMax);
    }
    public void LoseHealth(float value)
    {
        health = Mathf.Clamp(health - value, 0, healthMax);
    }
    public void GainBomb()
    {
        bombs = Mathf.Clamp(bombs + 1, 0, 3);
    }
    public void UseBomb()
    {
        if (bombs > 0)
        {
            bombs--;
        }
    }
    public void GainAttack(float value)
    {
        attack += value;
    }
}

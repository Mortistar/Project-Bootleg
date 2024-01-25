using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStats
{
    public enum EnemyScore
    {
        weak,
        basic,
        strong,
        priest
    }
    public int enemiesKilled {get; private set;}
    private float timeElapsed;
    public int score {get; private set;}
    public int secretsFound {get; private set;}
    public int secretsTotal {get; private set;}

    public DungeonStats()
    {
        enemiesKilled = 0;
        timeElapsed = 0;
        score = 0;
        secretsTotal = 0;
        secretsFound = 0;
    }
    public DungeonStats(int secretAmount)
    {
        enemiesKilled = 0;
        timeElapsed = 0;
        score = 0;
        secretsFound = 0;
        secretsTotal = secretAmount;
    }
    public void KillEnemy(EnemyScore enemyType)
    {
        enemiesKilled++;

        switch(enemyType)
        {
            case EnemyScore.weak:
                score += 50;
                break;
            case EnemyScore.basic:
                score += 100;
                break;
            case EnemyScore.strong:
                score += 200;
                break;
            case EnemyScore.priest:
                score += 500;
                break;
        }
    }
    public void AddTime(float value)
    {
        timeElapsed += value;
    }
    public int GetTime()
    {
        return Mathf.RoundToInt(timeElapsed);
    }
    public string GetTimeString()
    {
        int minutes = TimeSpan.FromSeconds(timeElapsed).Minutes;
        int seconds = TimeSpan.FromSeconds(timeElapsed).Seconds;
        int milliseconds = TimeSpan.FromSeconds(timeElapsed).Milliseconds;
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
    public void FindSecret()
    {
        secretsFound++;
    }
}

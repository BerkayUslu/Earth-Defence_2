using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour, IExperience
{
    [SerializeField] int playerExperience = 0;
    [SerializeField] int playerLevel = 0;
    [SerializeField] int expBarrier = 0;

    public Action playerLeveledUp;

    private void Start()
    {
        CheckTheLevel();
    }

    public void GainExperience(int expAmount)
    {
        playerExperience += expAmount;
        CheckTheLevel();
    }

    private void CheckTheLevel()
    {
        if (playerExperience >= expBarrier)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerLevel++;
        CalculateNextLevelExpBarrier();
        playerLeveledUp.Invoke();
    }

    private void CalculateNextLevelExpBarrier()
    {
        expBarrier = 10  * (int)(Mathf.Pow(2, playerLevel));
    }

}

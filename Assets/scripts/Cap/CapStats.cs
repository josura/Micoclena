using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapStats
{
    public float speedStat = 0;
    public float strenghtStat = 0;
    public float toxicStat = 0;
    public float rangedStat = 0;
    public int healthStat = 0;
    public CapStats()
    {

    }
    public CapStats(float _speedStat, float _strenghtStat, float _toxicStat, int _healthStat, float _rangedStat)
    {
        speedStat = _speedStat;
        strenghtStat = _strenghtStat;
        toxicStat = _toxicStat;
        rangedStat = _rangedStat;
        healthStat = _healthStat;
    }
}

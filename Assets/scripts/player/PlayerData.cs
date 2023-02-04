using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerData{
    public int health;
    public int maxhealth;
    public float speed= 0;
    public float strenght= 0;
    public float toxic= 0;
    public float ranged= 0;
    public int currentCapLevel = 0;
    public float[] position;

    public playerData(int _health, int capLevel, float initialStrenght, float initialSpeed, float initialToxic, float initialRanged, Vector2 position)
    {
        health = (_health);
        currentCapLevel = capLevel;  // to change to mushrooms
        strenght = initialStrenght;
        speed = initialSpeed;
        toxic = initialToxic;
        ranged = initialRanged;
        //position = new float[3];
    }
}
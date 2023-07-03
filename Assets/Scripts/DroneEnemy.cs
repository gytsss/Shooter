using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;


public class DroneEnemy : MonoBehaviour
{
    private StaticEnemy staticEnemy;

    public void TakeBullet()
    {
        staticEnemy.TakeDamage();
    }

}

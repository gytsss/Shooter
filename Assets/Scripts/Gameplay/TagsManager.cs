using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagsManager : MonoBehaviour
{
    public static TagsManager instance;
    
    public string playerTag = "Player";
    public string enemyTag = "Enemy";
    public string bulletTag = "Bullet";
    public string enemyBulletTag = "EnemyBullet";
    public string gunTag = "Gun";
    public string staticEnemyTag = "StaticEnemy";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}

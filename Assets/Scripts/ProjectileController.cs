using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public static ProjectileController instance;

    [SerializeField] private ObjectPool swordProjectiles;



    private void Awake()
    {
        instance = this;
    }

    public static void SpawnSwordProjectile(Vector2 position)
    {
        GameObject obj = instance.swordProjectiles.GetNext();
        obj.transform.position = position;
        obj.SetActive(true);
    }
}
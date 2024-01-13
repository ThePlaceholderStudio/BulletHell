using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringEnemy : Enemy
{
    public float firingRate = 30; //Per minute
    private float fireTime;
    private float firingTimeCounter;

    public int projectileSpeed = 12;

    [SerializeField]
    public GameObject projectilePrefab;
    
    protected override void Init()
    {
        base.Init();

        fireTime = 60f / firingRate;
        firingTimeCounter = fireTime;

    }

    // Update is called once per frame
    void Update()
    {
        firingTimeCounter -= Time.deltaTime;

        if (firingTimeCounter <= 0)
        {
            firingTimeCounter = fireTime;

            Fire();
        }
    }

    private void Fire()
    {
        GameObject player = character.gameObject;
        if (player != null)
        {
            Vector3 directionToEnemy = (player.transform.position - transform.position).normalized;

            if (projectilePrefab != null)
            {
                Vector3 pos = transform.position;
                pos += transform.forward * 2f;
                Transform projectileTransform = Instantiate(projectilePrefab.transform, pos, Quaternion.identity);
                Projectile projectile = projectileTransform.GetComponent<Projectile>();
                projectile.IsFiredFromEnemy = true;
                if (projectile != null)
                {
                    projectile.ProjectilePhysics(directionToEnemy, projectileSpeed);
                }
                else
                {
                    Debug.LogError("No Projectile component attached to the bullet prefab.");
                }
            }
            else
            {
                Debug.LogError("No bullet prefab assigned.");
            }
        }
        else
        {
            Debug.Log("No enemies detected.");            
        }
    }
}

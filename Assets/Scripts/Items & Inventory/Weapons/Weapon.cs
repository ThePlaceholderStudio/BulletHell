using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float LaunchVelocity = 10f;

    private float RoundsPerMinute;
    [SerializeField] private Transform pfProjectile;
    [SerializeField] private EquippableItem weapon;
    [SerializeField]private WeaponType weaponType;

    [Header("Pattern Test")]
    private int numberOfProjectiles;
    private Vector3 startPoint;
    private float conicalAngle;

    private const float radius = 1f;

    private float reloadTime; // Reload time in seconds
    private int magazineSize; 
    private int currentAmmo; 
    private bool isReloading = false;

    private float fireTimer = 0.0f;

    private void Start()
    {
        if (!PauseControl.isPaused)
        {
            RoundsPerMinute = weapon.RPM;
            numberOfProjectiles = weapon.ProjectileCount;
            conicalAngle = weapon.ConicalAngle;
            reloadTime = weapon.ReloadTime;
            magazineSize = weapon.MagazineSize;
        }
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= 60f / RoundsPerMinute)
        {
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    // Fire logic for Pistol
                    StartCoroutine(LockProjectile());
                    break;
                case WeaponType.Shotgun:
                    // Fire logic for Shotgun
                    StartCoroutine(SpawnCircularProjectile());
                    break;
                case WeaponType.Rifle:
                    // Fire logic for Rifle
                    StartCoroutine(SpawnProjectile());
                    break;
            }
            fireTimer = 0f;
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
    }

    private IEnumerator LockProjectile()
    {
        if (isReloading)
            yield break;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            yield break;
        }

        yield return new WaitForSeconds(0.1f); // Add delay before firing

        currentAmmo--;

        // Detect nearest enemy
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            Vector3 directionToEnemy = (nearestEnemy.transform.position - transform.position).normalized;

            if (pfProjectile != null)
            {
                Transform projectileTransform = Instantiate(pfProjectile, transform.position, Quaternion.identity);
                Projectile projectile = projectileTransform.GetComponent<Projectile>();

                if (projectile != null)
                {
                    projectile.ProjectilePhysics(directionToEnemy);
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
            yield break;
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = (enemy.transform.position - transform.position).sqrMagnitude;

            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }


    private IEnumerator SpawnProjectile()
    {
        if (isReloading)
            yield break;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            yield break;
        }

        yield return new WaitForSeconds(0.1f); // Add delay before firing

        currentAmmo--;

        if (pfProjectile != null)
        {
            Transform projectileTransform = Instantiate(pfProjectile, transform.position, Quaternion.identity);
            Projectile projectile = projectileTransform.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.ProjectilePhysics(transform.forward);
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

    private IEnumerator SpawnCircularProjectile()
    {
        if (isReloading)
            yield break;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            yield break;
        }

        yield return new WaitForSeconds(0.1f); // Add delay before firing

        currentAmmo--;

        float angleStep = conicalAngle / (numberOfProjectiles - 1);
        float angle = -conicalAngle / 2; // Start at half the conical angle

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            // Direction calculations.
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirZPosition = startPoint.z + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            // Create vectors.
            Vector3 projectileVector = new Vector3(projectileDirXPosition, 0, projectileDirZPosition);

            // Rotate the vector by the player's rotation.
            projectileVector = transform.rotation * projectileVector;

            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * LaunchVelocity;

            // Create game objects.
            if (pfProjectile != null)
            {
                Transform projectileTransform = Instantiate(pfProjectile, transform.position, Quaternion.identity);
                Projectile projectile = projectileTransform.GetComponent<Projectile>();

                if (projectile != null)
                {
                    projectile.ProjectilePhysics(projectileMoveDirection);
                    Destroy(projectile.gameObject, 10F);
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

            angle += angleStep;
        }
    }

}

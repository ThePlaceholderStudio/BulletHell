using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Projectile
{
    public bool go;//Will Be Used To Change Direction Of Weapon

    public Vector3 locationInFrontOfPlayer;//Location In Front Of Player To Travel To

    public float rotationSpeed = 500f;
    public float movementSpeed = 40f;
    public float delay = 1.5f;

    public IEnumerator Boom()
    {
        go = true;
        yield return new WaitForSeconds(delay);//Any Amount Of Time You Want
        go = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(CalculatePlayerDamage());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0); //Rotate The Object

        if (go)
        {
            transform.position = Vector3.Lerp(transform.position, locationInFrontOfPlayer, Time.deltaTime * movementSpeed); //Change The Position To The Location In Front Of The Player            
        }

        if (!go)
        {
            Vector3 playerPosition = new Vector3(character.transform.position.x, character.transform.position.y + 1, character.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * movementSpeed); //Return To Player
        }

        if (!go && Vector3.Distance(character.transform.position, transform.position) < 1.5)
        {
            Destroy(gameObject);
        }
    }
}

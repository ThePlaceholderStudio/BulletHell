using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Character character;

    private void Update()
    {
        Vector3 currentSize = GetComponent<BoxCollider>().size;

        currentSize.x = character.PickUpRadius.Value * 100;
        currentSize.z = character.PickUpRadius.Value * 100;

        GetComponent<BoxCollider>().size = currentSize;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Experience>(out Experience experience))
        {
            experience.SetTarget(transform.parent.position);
        }
    }
}
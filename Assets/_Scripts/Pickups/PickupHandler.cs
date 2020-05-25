using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupHandler : MonoBehaviour
{
    public List<PickupEffectPair> handledPickups = new List<PickupEffectPair>();
    private bool shouldPickup = true;

    private void OnEnable()
    {
        shouldPickup = true;
    }

    private void OnDisable()
    {
        shouldPickup = false;
    }

    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (shouldPickup)
        {
            Pickup pickup = otherCollider.gameObject.GetComponent<Pickup>();
            if (pickup != null)
            {
                foreach (PickupEffectPair pep in handledPickups)
                {
                    if (pickup.identifier == pep.pickupIdentifier && pep.pickupEffect != null)
                    {
                        pep.pickupEffect.Invoke();
                        PoolObject pickupPool = pickup.gameObject.GetComponent<PoolObject>();
                        if (pickup.destroyOnPickup)
                        {
                            if (pickupPool != null)
                                pickupPool.ReturnToPool();
                            else
                                Destroy(pickup.gameObject);
                        }
                    }
                }
            }
        }
    }
}

[System.Serializable]
public struct PickupEffectPair
{
    public PickupIdentifier pickupIdentifier;
    public UnityEvent pickupEffect;
}

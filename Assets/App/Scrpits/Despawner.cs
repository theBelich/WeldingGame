using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    [SerializeField] private WieldCompleteEvent wield;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        wield.Finished();
    }
}

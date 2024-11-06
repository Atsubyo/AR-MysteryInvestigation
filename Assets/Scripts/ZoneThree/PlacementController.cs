using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    [SerializeField] private GameObject Barrier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlaceablePlank"))
        {
            Barrier.SetActive(false);
        }
    }
}

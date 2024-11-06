using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCollider : MonoBehaviour
{
    [SerializeField] private GameObject TorchEmber;
    [SerializeField] private GameObject WallTorch;

    void Start()
    {

    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(WallTorch))
        {
            Debug.Log(":::test2");
            TorchEmber.SetActive(true);
        }
    }
}

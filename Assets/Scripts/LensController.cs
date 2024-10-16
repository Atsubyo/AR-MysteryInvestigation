using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensController : MonoBehaviour
{
    private bool GhostHunting = false;
    [SerializeField] private GameObject ControlledObject;

    public void ToggleGhostHunting()
    {
        GhostHunting = !GhostHunting;
        ControlledObject.SetActive(GhostHunting);
    }
}

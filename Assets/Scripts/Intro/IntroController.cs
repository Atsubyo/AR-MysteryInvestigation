using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    [SerializeField] private Collider Intro2Zone;
    [SerializeField] private GameObject[] Intros;

    [SerializeField] private GameObject Polterdust;
    [SerializeField] private GameObject MainEntranceDoor;

    [SerializeField] private HotbarController hotbarController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Intro2Zone))
        {
            ToggleClue(1);
            // update y axis rotation to 90 for MainEntranceDoor
            MainEntranceDoor.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
        }
        if (other.gameObject.Equals(Polterdust))
        {
            // Disable the tapped object
            Polterdust.SetActive(false);
            ToggleClue(2);

            // Add the tapped object to the hotbar via HotbarController
            if (hotbarController != null)
            {
                hotbarController.AddToHotbar("Polterdust");
            }
        }

    }

    private void ToggleClue(int activeIdx)
    {
        for (int i = 0; i < Intros.Length; ++i)
        {
            if (i == activeIdx)
            {
                Intros[i].SetActive(true);
            }
            else
            {
                Intros[i].SetActive(false);
            }
        }
    }
}

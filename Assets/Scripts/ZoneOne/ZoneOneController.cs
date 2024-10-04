using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneOneController : MonoBehaviour
{
    [SerializeField] private GameObject TorchEmber;
    [SerializeField] private GameObject HandTorch;
    [SerializeField] private GameObject WallTorch;
    [SerializeField] private GameObject LowerGhost;
    [SerializeField] private Collider Zone;

    [SerializeField] private GameObject Clue;
    [SerializeField] private GameObject GhostDialogue;

    [SerializeField] private HotbarController hotbarController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Zone))
        {
            Clue.SetActive(true);
        }

        if (other.gameObject.Equals(HandTorch))
        {
            HandTorch.SetActive(false);

            if (hotbarController != null)
            {
                hotbarController.AddToHotbar("HandTorch");
            }
        }

        if (other.gameObject.Equals(WallTorch))
        {
            int slotIdx = hotbarController.SelectedSlot;
            if (hotbarController.HotbarText[slotIdx].text == "HandTorch")
            {
                TorchEmber.SetActive(true);
                LowerGhost.SetActive(true);
                GhostDialogue.SetActive(true);
            }
        }

        if (other.gameObject.Equals(LowerGhost))
        {
            int slotIdx = hotbarController.SelectedSlot;
            if (hotbarController.HotbarText[slotIdx].text == "Polterdust")
            {
                LowerGhost.SetActive(false);
            }
        }
    }
}

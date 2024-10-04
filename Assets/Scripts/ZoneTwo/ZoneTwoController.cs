using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTwoController : MonoBehaviour
{
    private bool gateIsClosed = false;
    private bool ghostDefeated = false;
    [SerializeField] GameObject Gate;
    [SerializeField] private GameObject Potion;
    [SerializeField] private GameObject LowerGhost;
    [SerializeField] private Collider Zone;

    [SerializeField] private GameObject Clue;
    [SerializeField] private GameObject GhostDialogue;

    [SerializeField] private HotbarController hotbarController;

    private int GhostStrength = 1;

    public void OpenClue()
    {
        Clue.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Zone) && !ghostDefeated)
        {
            if (!gateIsClosed)
            {
                Transform gatePos = Gate.transform;
                gatePos.position = new Vector3(gatePos.position.x, gatePos.position.y - 3, gatePos.position.z);
                gateIsClosed = true;
                LowerGhost.SetActive(true);
                GhostDialogue.SetActive(true);
            }
        }

        if (other.gameObject.Equals(Potion))
        {
            Potion.SetActive(false);

            if (hotbarController != null)
            {
                hotbarController.AddToHotbar("Potion");
            }
        }

        if (other.gameObject.Equals(LowerGhost))
        {
            int slotIdx = hotbarController.SelectedSlot;
            if (hotbarController.HotbarText[slotIdx].text == "Potion")
            {
                --GhostStrength;
            }
            if (hotbarController.HotbarText[slotIdx].text == "Polterdust")
            {
                if (GhostStrength < 1)
                {
                    LowerGhost.SetActive(false);
                    ghostDefeated = true;

                    Transform gatePos = Gate.transform;
                    gatePos.position = new Vector3(gatePos.position.x, gatePos.position.y + 3, gatePos.position.z);
                    gateIsClosed = false;
                }
            }
        }
    }
}

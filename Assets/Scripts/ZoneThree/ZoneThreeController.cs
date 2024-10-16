using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneThreeController : MonoBehaviour
{
    public bool ghostDefeated = false;
    [SerializeField] private GameObject LowerGhost;
    [SerializeField] private Collider Zone;
    [SerializeField] private GameObject[] Barriers;

    [SerializeField] private GameObject LoosePlank;
    [SerializeField] private GameObject PathPlanks;
    [SerializeField] private GameObject Void;

    [SerializeField] private GameObject Clue;
    [SerializeField] private GameObject GhostDialogue;
    [SerializeField] private GameObject FailDialogue;
    [SerializeField] private GameObject PlaneFinder;

    [SerializeField] private HotbarController hotbarController;

    public void OpenClue()
    {
        Clue.SetActive(true);
    }

    public void ResetScene()
    {
        LowerGhost.SetActive(false);
        GhostDialogue.SetActive(false);
        LoosePlank.SetActive(true);
        Void.SetActive(false);
        PathPlanks.SetActive(false);
        PlaneFinder.SetActive(false);
        ghostDefeated = false;
        foreach (GameObject barrier in Barriers)
        {
            barrier.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Zone) && !ghostDefeated)
        {
            PlaneFinder.SetActive(true);
            LowerGhost.SetActive(true);
            GhostDialogue.SetActive(true);
            LoosePlank.SetActive(false);
            Void.SetActive(true);
            PathPlanks.SetActive(true);
        }

        if (other.CompareTag("Plank"))
        {
            hotbarController.AddToHotbar("Plank");
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("BarrierZone"))
        {
            ResetScene();
            FailDialogue.SetActive(true);
        }

        if (other.gameObject.Equals(LowerGhost))
        {
            int slotIdx = hotbarController.SelectedSlot;
            if (hotbarController.HotbarText[slotIdx].text == "Polterdust")
            {
                PlaneFinder.SetActive(false);
                LowerGhost.SetActive(false);
                ghostDefeated = true;
                Void.SetActive(false);
                PathPlanks.SetActive(false);
            }
        }
    }
}

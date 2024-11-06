using System.Collections.Generic;
using UnityEngine;

using static NarrativeController;
using static HotbarItems;
using static AudioController;

public class ZoneOneController : MonoBehaviour
{
    private bool torchObtained = false;
    private bool torchLit = false;
    public bool ghostDefeated = false;
    public bool inZone = false;

    [SerializeField] private Collider RenderZone;
    [SerializeField] private Collider Zone;

    [SerializeField] private List<GameObject> Environment;
    [SerializeField] private GameObject TorchEmber;
    [SerializeField] private GameObject HandTorch;
    [SerializeField] private GameObject WallTorch;
    [SerializeField] private GameObject LowerGhost;
    [SerializeField] private GameObject Key;

    [SerializeField] private List<GameObject> NarrativePieces;

    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private AudioController audioController;

    void Start()
    {
        foreach (GameObject obj in Environment)
        {
            obj.SetActive(false);
        }
        HandTorch.SetActive(false);
        LowerGhost.SetActive(false);
        Key.SetActive(false);
    }

    public void OpenClue()
    {
        if (inZone)
        {
            audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
            IsolateNarrativePiece(ref NarrativePieces, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(RenderZone))
        {
            if (!torchObtained)
            {
                HandTorch.SetActive(true);
            }
            if (torchLit)
            {
                TorchEmber.SetActive(true);
                if (!ghostDefeated)
                {
                    LowerGhost.SetActive(true);
                }
            }
            foreach (GameObject obj in Environment)
            {
                obj.SetActive(true);
            }
        }

        if (other.Equals(Zone))
        {
            inZone = true;

            audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
            IsolateNarrativePiece(ref NarrativePieces, 0);
        }

        if (other.gameObject.Equals(HandTorch))
        {
            torchObtained = true;
            HandTorch.SetActive(false);
            if (hotbarController != null)
            {
                hotbarController.AddToHotbar(HAND_TORCH);
            }
        }

        if (other.gameObject.Equals(WallTorch))
        {
            int slotIdx = hotbarController.SelectedSlot;
            if (hotbarController.HotbarText[slotIdx].text == HAND_TORCH)
            {
                torchLit = true;
                TorchEmber.SetActive(true);

                audioController.PlayGlobalSound((int)GlobalAudio.GhostDialogue);
                LowerGhost.SetActive(true);

                audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                IsolateNarrativePiece(ref NarrativePieces, 2);

                hotbarController.DeleteFromHotbar(HAND_TORCH);
            }
        }

        if (other.gameObject.Equals(LowerGhost))
        {
            int slotIdx = hotbarController.SelectedSlot;
            if (hotbarController.HotbarText[slotIdx].text == POLTERDUST)
            {
                Key.SetActive(true);

                audioController.PlayGlobalSound((int)GlobalAudio.Polterdust);
                audioController.PlayGlobalSound((int)GlobalAudio.GhostDeath);
                LowerGhost.SetActive(false);
                ghostDefeated = true;

                audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                IsolateNarrativePiece(ref NarrativePieces, 3);
            }
        }

        if (other.gameObject.Equals(Key))
        {
            hotbarController.AddToHotbar(SILVER_KEY);

            audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
            IsolateNarrativePiece(ref NarrativePieces, 5);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.Equals(RenderZone))
        {
            HandTorch.SetActive(false);
            TorchEmber.SetActive(false);
            LowerGhost.SetActive(false);
            foreach (GameObject obj in Environment)
            {
                obj.SetActive(false);
            }
        }

        if (other.Equals(Zone))
        {
            inZone = false;
        }
    }
}

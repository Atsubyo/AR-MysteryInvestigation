using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

using static NarrativeController;
using static HotbarItems;
using static AudioController;

public class ZoneFourController : MonoBehaviour
{
    public bool ghostDefeated = false;

    public bool inZone = false;
    private int currentChest = 0;
    public bool ghostWeakened = false;

    [SerializeField] private List<GameObject> NarrativePieces;
    [SerializeField] private GameObject Chest1;
    [SerializeField] private GameObject Chest2;
    [SerializeField] private GameObject Chest3;
    [SerializeField] private GameObject MiddleGhost;
    [SerializeField] private Collider Zone;
    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private ZoneThreeController ghostProgressCheck;

    void Start()
    {
        MiddleGhost.SetActive(false);
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
        if (ghostProgressCheck.ghostDefeated)
        {
            if (other.Equals(Zone) && !ghostDefeated)
            {
                audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                IsolateNarrativePiece(ref NarrativePieces, 0);
                MiddleGhost.SetActive(true);
                inZone = true;
            }
            if (other.gameObject.Equals(Chest1))
            {
                int slotIdx = hotbarController.SelectedSlot;
                if (hotbarController.HotbarText[slotIdx].text == GOLD_KEY)
                {
                    if(currentChest == 1)
                    {
                        audioController.PlayZoneSound((int)ZoneAudio.UnlockLock);
                        hotbarController.DeleteFromHotbar(GOLD_KEY);
                        currentChest++;
                    }
                    else
                    {
                        audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                        IsolateNarrativePiece(ref NarrativePieces, 2);

                        audioController.PlayZoneSound((int)ZoneAudio.PuzzleFail);
                        ResetKeys();
                    }
                }
            }
            if (other.gameObject.Equals(Chest2))
            {
                int slotIdx = hotbarController.SelectedSlot;
                if (hotbarController.HotbarText[slotIdx].text == SILVER_KEY)
                {
                    if(currentChest == 0)
                    {
                        audioController.PlayZoneSound((int)ZoneAudio.UnlockLock);
                        hotbarController.DeleteFromHotbar(SILVER_KEY);
                        currentChest++;
                    }
                    else
                    {
                        audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                        IsolateNarrativePiece(ref NarrativePieces, 2);

                        audioController.PlayZoneSound((int)ZoneAudio.PuzzleFail);
                        ResetKeys();
                    }
                }
            }
            if (other.gameObject.Equals(Chest3))
            {
                int slotIdx = hotbarController.SelectedSlot;
                if (hotbarController.HotbarText[slotIdx].text == RUSTY_KEY)
                {
                    if(currentChest == 2)
                    {
                        audioController.PlayZoneSound((int)ZoneAudio.UnlockLock);
                        hotbarController.DeleteFromHotbar(RUSTY_KEY);

                        audioController.PlayZoneSound((int)ZoneAudio.OpenChest);
                        Chest1.transform.rotation = Quaternion.Euler(-90, -90, 0);
                        Chest2.transform.rotation = Quaternion.Euler(-90, -90, 0);
                        Chest3.transform.rotation = Quaternion.Euler(-90, -90, 0);

                        audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                        IsolateNarrativePiece(ref NarrativePieces, 5);

                        audioController.PlayGlobalSound((int)GlobalAudio.GhostWeaken);
                        ghostWeakened = true;
                    }
                    else
                    {
                        audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                        IsolateNarrativePiece(ref NarrativePieces, 2);

                        audioController.PlayZoneSound((int)ZoneAudio.PuzzleFail);
                        ResetKeys();
                    }
                }
            }
            if (other.gameObject.Equals(MiddleGhost))
            {
                int slotIdx = hotbarController.SelectedSlot;
                if (hotbarController.HotbarText[slotIdx].text == POLTERDUST)
                {
                    if (ghostWeakened)
                    {
                        audioController.PlayGlobalSound((int)GlobalAudio.Polterdust);
                        audioController.PlayGlobalSound((int)GlobalAudio.GhostDeath);
                        MiddleGhost.SetActive(false);
                        ghostDefeated = true;

                        audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                        IsolateNarrativePiece(ref NarrativePieces, 6);
                    }
                    else
                    {
                        audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                        IsolateNarrativePiece(ref NarrativePieces, 2);
                    }
                }
            }
        }
    }

    private void ResetKeys()
    {
        currentChest = 0;
        hotbarController.DeleteFromHotbar(GOLD_KEY);
        hotbarController.DeleteFromHotbar(SILVER_KEY);
        hotbarController.DeleteFromHotbar(RUSTY_KEY);
        hotbarController.AddToHotbar(GOLD_KEY);
        hotbarController.AddToHotbar(SILVER_KEY);
        hotbarController.AddToHotbar(RUSTY_KEY);
    }
}

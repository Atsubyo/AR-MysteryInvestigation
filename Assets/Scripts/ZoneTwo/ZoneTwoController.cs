using System.Collections.Generic;
using UnityEngine;

using static NarrativeController;
using static HotbarItems;
using static AudioController;

public class ZoneTwoController : MonoBehaviour
{
    public bool ghostDefeated = false;

    private bool gateIsClosed = false;
    public bool inZone = false;
    public bool potionObtained = false;
    public bool bottleObtained = false;
    public bool keyObtained = false;
    public int GhostStrength = 1;

    [SerializeField] private ZoneOneController prevZone;
    [SerializeField] private Collider RenderZone;
    [SerializeField] private Collider Zone;

    [SerializeField] private List<GameObject> Environment;
    [SerializeField] private GameObject startingDoor;
    [SerializeField] private List<GameObject> IncorrectPotions;
    [SerializeField] private GameObject EmptyFlask;
    [SerializeField] private GameObject PotionLiquid;
    [SerializeField] private GameObject LowerGhost;
    [SerializeField] private GameObject ProtectiveBarrier;
    [SerializeField] private GameObject Key;
    [SerializeField] private GameObject Gate;

    [SerializeField] private List<GameObject> NarrativePieces;

    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private AudioController audioController;

    void Start()
    {
        Gate.SetActive(false);
        foreach (GameObject obj in Environment)
        {
            obj.SetActive(false);
        }
        startingDoor.SetActive(true);
        Key.SetActive(false);
        ProtectiveBarrier.SetActive(false);
        LowerGhost.SetActive(false);
    }

    public void OpenClue()
    {
        if (inZone)
        {
            audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
            IsolateNarrativePiece(ref NarrativePieces, 3);
            audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.Equals(RenderZone))
        {
            foreach (GameObject obj in Environment)
            {
                obj.SetActive(true);
            }
            startingDoor.SetActive(true);
        }
        if (prevZone.ghostDefeated)
        {
            if (other.Equals(Zone))
            {
                inZone = true;
                if (!ghostDefeated && !gateIsClosed)
                {
                    Gate.SetActive(true);
                    gateIsClosed = true;
                    audioController.PlayGlobalSound((int)GlobalAudio.GateClosing);

                    audioController.PlayGlobalSound((int)GlobalAudio.GhostDialogue);
                    ProtectiveBarrier.SetActive(true);
                    LowerGhost.SetActive(true);

                    audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                    IsolateNarrativePiece(ref NarrativePieces, 0);
                }
            }

            foreach (GameObject incorrectPotion in IncorrectPotions)
            {
                if (other.gameObject.Equals(incorrectPotion))
                {
                    audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                    IsolateNarrativePiece(ref NarrativePieces, 4);
                }
            }

            if (other.gameObject.Equals(EmptyFlask))
            {
                if (hotbarController != null)
                {
                    bottleObtained = true;
                    EmptyFlask.SetActive(false);
                    audioController.PlayGlobalSound((int)GlobalAudio.ItemAdd);
                    hotbarController.AddToHotbar(EMPTY_FLASK);

                    audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                    IsolateNarrativePiece(ref NarrativePieces, 5);
                }
            }

            if (other.gameObject.Equals(PotionLiquid))
            {
                potionObtained = true;
                hotbarController.RenameHotbarItem(EMPTY_FLASK, FULL_FLASK);

                audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                IsolateNarrativePiece(ref NarrativePieces, 6);
            }

            if (other.gameObject.Equals(LowerGhost))
            {
                int slotIdx = hotbarController.SelectedSlot;
                if (hotbarController.HotbarText[slotIdx].text == FULL_FLASK)
                {
                    --GhostStrength;
                    ProtectiveBarrier.SetActive(false);
                    audioController.PlayZoneSound((int)ZoneAudio.PotionSplash);
                    hotbarController.DeleteFromHotbar(FULL_FLASK);

                    audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                    IsolateNarrativePiece(ref NarrativePieces, 7);
                }
                if (hotbarController.HotbarText[slotIdx].text == POLTERDUST)
                {
                    if (GhostStrength < 1)
                    {
                        Key.SetActive(true);

                        audioController.PlayGlobalSound((int)GlobalAudio.Polterdust);
                        audioController.PlayGlobalSound((int)GlobalAudio.GhostDeath);
                        LowerGhost.SetActive(false);
                        ghostDefeated = true;

                        Gate.SetActive(false);
                        gateIsClosed = false;

                        PotionLiquid.SetActive(false);

                        audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                        IsolateNarrativePiece(ref NarrativePieces, 8);
                    } else
                    {
                        audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                        IsolateNarrativePiece(ref NarrativePieces, 2);
                    }
                }
            }

            if (other.gameObject.Equals(Key))
            {
                keyObtained = true;
                Key.SetActive(false);
                hotbarController.AddToHotbar(GOLD_KEY);

                audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                IsolateNarrativePiece(ref NarrativePieces, 10);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.Equals(RenderZone))
        {
            foreach (GameObject obj in Environment)
            {
                obj.SetActive(false);
            }
            startingDoor.SetActive(false);
        }

        if (other.Equals(Zone))
        {
            inZone = false;
        }
    }
}

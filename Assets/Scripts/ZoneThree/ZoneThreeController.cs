using System.Collections.Generic;
using UnityEngine;
using Vuforia;

using static NarrativeController;
using static HotbarItems;
using static AudioController;

public class ZoneThreeController : MonoBehaviour
{
    public int planksObtained = 0;
    public bool ghostDefeated = false;
    public bool inZone = false;
    public bool mazeActive = false;
    public bool keyObtained = false;

    [SerializeField] private ZoneTwoController prevZone;
    [SerializeField] private Collider RenderZone;
    [SerializeField] private Collider Zone;

    [SerializeField] private List<GameObject> Environment;
    [SerializeField] private GameObject LowerGhost;
    [SerializeField] private GameObject Key;

    [SerializeField] private GameObject[] Barriers;
    [SerializeField] private GameObject[] PlankObjs;
    [SerializeField] private GameObject PlankPrefab;
    [SerializeField] private GameObject PlaneFinder;
    [SerializeField] private GameObject LoosePlank;
    [SerializeField] private GameObject PathPlanks;
    [SerializeField] private GameObject Void;
    private List<GameObject> Planks;

    [SerializeField] private List<GameObject> NarrativePieces;

    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private AudioController audioController;

    [SerializeField] private AudioSource ghostSource;
    [SerializeField] private AudioSource playerSource;
    [SerializeField] private AudioClip itemGet;
    [SerializeField] private AudioClip ghostDeath;
    private HitTestResult previousHit;

    void Start()
    {
        foreach (GameObject obj in Environment)
        {
            obj.SetActive(false);
        }
        foreach (GameObject plank in PlankObjs)
        {
            plank.SetActive(false);
        }
        Planks = new List<GameObject>();
        PathPlanks.SetActive(false);
        Key.SetActive(false);
        LowerGhost.SetActive(false);
    }

    public void OpenClue()
    {
        if (inZone)
        {
            audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
            IsolateNarrativePiece(ref NarrativePieces, 2);
        }
    }

    public void ResetScene()
    {
        planksObtained = 0;
        foreach (GameObject plank in Planks)
        {
            plank.SetActive(false);
        }

        hotbarController.DeleteFromHotbar(PLANK);
        hotbarController.DeleteFromHotbar(PLANK);
        hotbarController.DeleteFromHotbar(PLANK);
        Planks.Clear();

        LoosePlank.SetActive(true);
        Void.SetActive(false);
        PathPlanks.SetActive(false);
        PlaneFinder.SetActive(false);

        LowerGhost.SetActive(false);
        ghostDefeated = false;

        foreach (GameObject plank in PlankObjs)
        {
            plank.SetActive(true);
        }

        foreach (GameObject barrier in Barriers)
        {
            barrier.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(RenderZone))
        {
            foreach (GameObject obj in Environment)
            {
                obj.SetActive(true);
            }
            foreach (GameObject plank in PlankObjs)
            {
                plank.SetActive(true);
            }
        }
        if (prevZone.ghostDefeated)
        {
            if (other.Equals(Zone) && !ghostDefeated)
            {
                mazeActive = true;
                inZone = true;
                PlaneFinder.SetActive(true);

                audioController.PlayGlobalSound((int)GlobalAudio.GhostDialogue);
                LowerGhost.SetActive(true);

                audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                IsolateNarrativePiece(ref NarrativePieces, 1);

                LoosePlank.SetActive(false);
                Void.SetActive(true);
                PathPlanks.SetActive(true);
            }

            if (other.CompareTag(PLANK))
            {
                Debug.Log("That's a plank");
                planksObtained++;
                hotbarController.AddToHotbar(PLANK);
                other.gameObject.SetActive(false);
            }

            if (other.CompareTag("BarrierZone") && mazeActive)
            {
                audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                IsolateNarrativePiece(ref NarrativePieces, 3);

                audioController.PlayZoneSound((int)ZoneAudio.PuzzleFail);
                ResetScene();
            }

            if (other.gameObject.Equals(LowerGhost))
            {
                int slotIdx = hotbarController.SelectedSlot;
                if (hotbarController.HotbarText[slotIdx].text == POLTERDUST)
                {
                    Key.SetActive(true);
                    mazeActive = false;

                    audioController.PlayGlobalSound((int)GlobalAudio.Polterdust);
                    audioController.PlayGlobalSound((int)GlobalAudio.GhostDeath);
                    LowerGhost.SetActive(false);
                    ghostDefeated = true;

                    PlaneFinder.SetActive(false);
                    Void.SetActive(false);
                    PathPlanks.SetActive(false);

                    audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                    IsolateNarrativePiece(ref NarrativePieces, 4);
                }
            }

            if (other.gameObject.Equals(Key))
            {
                keyObtained = true;
                Key.SetActive(false);
                hotbarController.AddToHotbar(RUSTY_KEY);

                audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                IsolateNarrativePiece(ref NarrativePieces, 6);
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
        }

        if (other.Equals(Zone))
        {
            inZone = false;
        }
    }

    public void IntersectionLocation(HitTestResult intersection)
    {
        if (intersection != null)
        {
            previousHit = intersection;
        }
    }

    public void PlankPlace()
    {
        if (hotbarController.CheckSelectedHotbarSlot("Plank"))
        {
            GameObject newPlank = Instantiate(PlankPrefab, previousHit.Position, previousHit.Rotation);
            Planks.Add(newPlank);
            hotbarController.RemoveFromHotbar("Plank");
        }
    }
}

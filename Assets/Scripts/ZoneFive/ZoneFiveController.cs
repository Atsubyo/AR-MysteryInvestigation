using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

using static NarrativeController;
using static HotbarItems;
using static AudioController;

public class ZoneFiveController : MonoBehaviour
{
    private bool mazeActive = false;
    public bool ghostDefeated = false;
    public bool puzzleSolving = false;
    public bool puzzleSolved = false;
    public bool inZone = false;

    private Vector3 OriginalSkullPosition;
    private HitTestResult previousHit;
    [SerializeField] GameObject Gate1;
    [SerializeField] GameObject Gate2;
    [SerializeField] private Collider PuzzleZone;
    [SerializeField] private Collider Finish;
    [SerializeField] private GameObject LowerGhost;
    [SerializeField] private Collider Zone;
    [SerializeField] private GameObject Maze1;
    [SerializeField] private GameObject Maze2;
    [SerializeField] private GameObject Maze3;
    [SerializeField] private Camera cam;
    [SerializeField] private Collider RenderZone;
    [SerializeField] private List<GameObject> Environment;
    float initialCamFOV;

    [SerializeField] private List<GameObject> NarrativePieces;

    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private ZoneFourController ghostProgressCheck;
    [SerializeField] private GameObject zoneFinder;
    [SerializeField] private GameObject endDoor;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in Environment)
        {
            obj.SetActive(false);
        }
        Gate1.SetActive(false);
        Gate2.SetActive(false);
        zoneFinder.SetActive(false);
        LowerGhost.SetActive(false);
        Maze1.SetActive(false);
        Maze2.SetActive(false);
        Maze3.SetActive(false);
        initialCamFOV = cam.fieldOfView;
        Debug.Log(initialCamFOV);
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleSolved)
        {
            endDoor.SetActive(false);
        }
    }

    public void OpenClue()
    {
        if (inZone)
        {
            //audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
            //IsolateNarrativePiece(ref NarrativePieces, 1);
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
        }
        if(ghostDefeated)
        {
            if (other.Equals(PuzzleZone))
            {
                zoneFinder.SetActive(true);
                puzzleSolving = true;
            }
        }
        if (ghostProgressCheck.ghostDefeated)
        {
            if (other.gameObject.Equals(Maze1) || other.gameObject.Equals(Maze2) || other.gameObject.Equals(Maze3) || (other.gameObject.Equals(Gate1)&&mazeActive)|| (other.gameObject.Equals(Gate2)&&mazeActive))
            {
                audioController.PlayZoneSound((int)ZoneAudio.PuzzleFail);
                Maze1.SetActive(false);
                Maze2.SetActive(false);
                Maze3.SetActive(false);
                cam.fieldOfView = initialCamFOV;
                mazeActive = false;
                Gate1.SetActive(false);
                Gate2.SetActive(false);
            }
            if (other.Equals(Zone) && !ghostDefeated)
            {
                inZone = true;
                if (!mazeActive)
                {
                    IsolateNarrativePiece(ref NarrativePieces, 0);
                    Maze1.SetActive(true);
                    Maze2.SetActive(true);
                    Maze3.SetActive(true);

                    audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                    NarrativePieces[0].SetActive(true);
                    cam.fieldOfView = initialCamFOV / 2;

                    audioController.PlayGlobalSound((int)GlobalAudio.GateClosing);
                    Gate1.SetActive(true);
                    Gate2.SetActive(true);
                    mazeActive = true;
                }
            }

            if (other.Equals(Finish) && mazeActive)
            {
                audioController.PlayGlobalSound((int)GlobalAudio.GhostDialogue);
                LowerGhost.SetActive(true);

            }

            if (other.gameObject.Equals(LowerGhost))
            {
                int slotIdx = hotbarController.SelectedSlot;
                if (hotbarController.HotbarText[slotIdx].text == POLTERDUST)
                {
                    audioController.PlayGlobalSound((int)GlobalAudio.Polterdust);
                    audioController.PlayGlobalSound((int)GlobalAudio.GhostDeath);
                    IsolateNarrativePiece(ref NarrativePieces, 1);
                    LowerGhost.SetActive(false);
                    ghostDefeated = true;

                    mazeActive = false;
                    cam.fieldOfView = initialCamFOV;

                    Maze1.SetActive(false);
                    Maze2.SetActive(false);
                    Maze3.SetActive(false);
                    Gate1.SetActive(false);
                    Gate2.SetActive(false);
                    inZone = false;
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.Equals(RenderZone))
        {
            foreach (GameObject obj in Environment)
            {
                obj.SetActive(false);
            }
        }
        if(other.Equals(PuzzleZone))
        {
            zoneFinder.SetActive(false);
            puzzleSolving = false;
        }
    }
}

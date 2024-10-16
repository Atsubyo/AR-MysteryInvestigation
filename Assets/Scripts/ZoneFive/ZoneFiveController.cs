using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ZoneFiveController : MonoBehaviour
{
    private bool mazeActive = false;
    private bool ghostDefeated = false;
    private bool puzzleSolving = false;
    public bool puzzleSolved = false;
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
    [SerializeField] private GameObject Maze4;
    [SerializeField] private GameObject puzzlePiece;

    [SerializeField] private GameObject Clue;
    [SerializeField] private GameObject GhostDialogue;

    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private ZoneThreeController ghostProgressCheck;
    [SerializeField] private GameObject zoneFinder;
    [SerializeField] private GameObject endDoor;

    // Start is called before the first frame update
    void Start()
    {
        OriginalSkullPosition = new Vector3(puzzlePiece.transform.position.x, puzzlePiece.transform.position.y, puzzlePiece.transform.position.z);
        zoneFinder.SetActive(false);
        LowerGhost.SetActive(false);
        Maze1.SetActive(false);
        Maze2.SetActive(false);
        Maze3.SetActive(false);
        Maze4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleSolved)
        {
            endDoor.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ghostProgressCheck.ghostDefeated || true)
        {
            if (other.Equals(PuzzleZone))
            {
                zoneFinder.SetActive(true);
                puzzleSolving = true;
            }
            if (other.gameObject.Equals(Maze1) || other.gameObject.Equals(Maze2) || other.gameObject.Equals(Maze3) || other.gameObject.Equals(Maze4))
            {
                Maze1.SetActive(false);
                Maze2.SetActive(false);
                Maze3.SetActive(false);
                Maze4.SetActive(false);
                mazeActive = false;
                Transform gate1Pos = Gate1.transform;
                Transform gate2Pos = Gate2.transform;
                gate1Pos.position = new Vector3(gate1Pos.position.x, gate1Pos.position.y + 3, gate1Pos.position.z);
                gate2Pos.position = new Vector3(gate2Pos.position.x, gate2Pos.position.y + 3, gate2Pos.position.z);
            }
            if (other.Equals(Zone) && !ghostDefeated)
            {
            
                if (!mazeActive)
                {
                    Maze1.SetActive(true);
                    Maze2.SetActive(true);
                    Maze3.SetActive(true);
                    Maze4.SetActive(true);
                    GhostDialogue.SetActive(true);
                    Transform gate1Pos = Gate1.transform;
                    Transform gate2Pos = Gate2.transform;
                    gate1Pos.position = new Vector3(gate1Pos.position.x, gate1Pos.position.y - 3, gate1Pos.position.z);
                    gate2Pos.position = new Vector3(gate2Pos.position.x, gate2Pos.position.y - 3, gate2Pos.position.z);
                    mazeActive = true;
                }
            }

            if (other.Equals(Finish) && mazeActive)
            {
                LowerGhost.SetActive(true);
                mazeActive = false;

            }

            if (other.gameObject.Equals(LowerGhost))
            {
                int slotIdx = hotbarController.SelectedSlot;
                if (hotbarController.HotbarText[slotIdx].text == "Polterdust")
                {
                    LowerGhost.SetActive(false);
                    ghostDefeated = true;
                    mazeActive = false;

                    Transform gate1Pos = Gate1.transform;
                    Transform gate2Pos = Gate2.transform;
                    gate1Pos.position = new Vector3(gate1Pos.position.x, gate1Pos.position.y + 3, gate1Pos.position.z);
                    gate2Pos.position = new Vector3(gate2Pos.position.x, gate2Pos.position.y + 3, gate2Pos.position.z);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.Equals(PuzzleZone))
        {
            zoneFinder.SetActive(false);
            puzzleSolving = false;
        }
    }

    public void intersectionLocation(HitTestResult intersection)
    {
        if (intersection != null)
        {
            previousHit = intersection;
        }
    }
    public void puzzlePlace()
    {
        if(puzzleSolving)
        {
            puzzlePiece.transform.position = previousHit.Position;
        }
    }

    public void resetPuzzlePiece()
    {
        if (puzzlePiece.transform.position == OriginalSkullPosition)
        {
            Debug.Log("These are the same");
        }
        puzzlePiece.transform.position = OriginalSkullPosition;
    }
}

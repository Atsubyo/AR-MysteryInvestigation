using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static NarrativeController;
using static HotbarItems;
using static AudioController;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public bool bossDefeated = false;
    public bool bossEnabled = true;
    public bool ghostWeakened = false;
    public float timer = 120.0f;
    private int numTorchesLit = 0;
    private bool timerStarted = false;
    private bool slidePuzzleCounted = false;
    [SerializeField] private List<GameObject> Environment;
    [SerializeField] private PuzzleController prevZone;
    [SerializeField] private Collider RenderZone;
    [SerializeField] private GameObject KingBoo;
    [SerializeField] private GameObject slidePuzzle;
    [SerializeField] private GameObject TorchEmber1;
    [SerializeField] private GameObject HandTorch1;
    [SerializeField] private GameObject WallTorch1;
    [SerializeField] private GameObject TorchEmber2;
    [SerializeField] private GameObject HandTorch2;
    [SerializeField] private GameObject WallTorch2;
    [SerializeField] private GameObject timerCanvas;
    [SerializeField] private GameObject door;
    [SerializeField] private List<GameObject> MazeTiles;
    [SerializeField] private List<GameObject> MazeTilesWrong1;
    [SerializeField] private List<GameObject> MazeTilesWrong2;
    [SerializeField] private List<GameObject> MazeColliders;
    [SerializeField] private List<GameObject> MazeCollidersWrong1;
    [SerializeField] private List<GameObject> MazeCollidersWrong2;
    [SerializeField] private ST_PuzzleDisplay slidePuzzleController;
    private int currentCollider = 0;
    public int puzzlesCompleted = 0;

    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private AudioController audioController;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in Environment)
        {
            obj.SetActive(false);
        }
        foreach (GameObject maz in MazeColliders)
        {
            maz.SetActive(false);
        }
        foreach (GameObject maz in MazeCollidersWrong1)
        {
            maz.SetActive(false);
        }
        foreach (GameObject maz in MazeCollidersWrong2)
        {
            maz.SetActive(false);
        }
        foreach (GameObject maz in MazeTiles)
        {
            maz.SetActive(false);
        }
        foreach (GameObject maz in MazeTilesWrong1)
        {
            maz.SetActive(false);
        }
        foreach (GameObject maz in MazeTilesWrong2)
        {
            maz.SetActive(false);
        }
        HandTorch1.SetActive(false);
        HandTorch2.SetActive(false);
        KingBoo.SetActive(false);
    }

    void Update()
    {
        if (prevZone.completed)
        {
            bossEnabled = true;
        }
        if (slidePuzzleController.Complete && !slidePuzzleCounted)
        {
            slidePuzzle.SetActive(false);
            puzzlesCompleted++;
            slidePuzzleCounted = true;
        }
        if (timerStarted)
        {
            timer -= Time.deltaTime;
            timerCanvas.GetComponent<TMPro.TextMeshProUGUI>().text = ((int)timer).ToString();

        }

        if (timer <= 0.0f)
        {
            timerEnded();
        }

        if(puzzlesCompleted == 3)
        {
            ghostWeakened = true;
        }
    }

    void timerEnded()
    {
        //do your stuff here.
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(RenderZone))
        {
            foreach (GameObject obj in Environment)
            {
                obj.SetActive(true);
            }
        }
        if (bossEnabled && other.Equals(RenderZone))
        {
            foreach (GameObject maz in MazeTiles)
            {
                maz.SetActive(true);
            }
            foreach (GameObject maz in MazeTilesWrong1)
            {
                maz.SetActive(true);
            }
            foreach (GameObject maz in MazeTilesWrong2)
            {
                maz.SetActive(true);
            }
            foreach (GameObject maz in MazeColliders)
            {
                maz.SetActive(true);
            }
            foreach (GameObject maz in MazeCollidersWrong1)
            {
                maz.SetActive(true);
            }
            foreach (GameObject maz in MazeCollidersWrong2)
            {
                maz.SetActive(true);
            }
            HandTorch1.SetActive(true);
            HandTorch2.SetActive(true);
            KingBoo.SetActive(true);
            slidePuzzle.SetActive(true);
            door.SetActive(true);
            timerStarted = true;
        }
        if (other.gameObject.Equals(HandTorch1))
        {
            HandTorch1.SetActive(false);
            if (hotbarController != null)
            {
                hotbarController.AddToHotbar(HAND_TORCH);
            }
        }
        if (other.gameObject.Equals(HandTorch2))
        {
            HandTorch2.SetActive(false);
            if (hotbarController != null)
            {
                hotbarController.AddToHotbar(HAND_TORCH);
            }
        }

        if (other.gameObject.Equals(WallTorch1))
        {
            int slotIdx = hotbarController.SelectedSlot;
            if (hotbarController.HotbarText[slotIdx].text == HAND_TORCH)
            {
                TorchEmber1.SetActive(true);
                numTorchesLit++;
                if (numTorchesLit == 2)
                {
                    puzzlesCompleted++;
                }
                hotbarController.DeleteFromHotbar(HAND_TORCH);
            }
        }
        if (other.gameObject.Equals(WallTorch2))
        {
            int slotIdx = hotbarController.SelectedSlot;
            if (hotbarController.HotbarText[slotIdx].text == HAND_TORCH)
            {
                TorchEmber2.SetActive(true);
                numTorchesLit++;
                if(numTorchesLit==2)
                {
                    puzzlesCompleted++;
                }
                hotbarController.DeleteFromHotbar(HAND_TORCH);
            }
        }
        if (other.gameObject.Equals(KingBoo))
        {
            Debug.Log("Collided with King Boo");
            int slotIdx = hotbarController.SelectedSlot;
            if (hotbarController.HotbarText[slotIdx].text == POLTERDUST)
            {
                if (ghostWeakened)
                {
                    audioController.PlayGlobalSound((int)GlobalAudio.Polterdust);
                    audioController.PlayGlobalSound((int)GlobalAudio.GhostDeath);
                    KingBoo.SetActive(false);
                    bossDefeated = true;

                    audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                    //IsolateNarrativePiece(ref NarrativePieces, 6);
                }
                else
                {
                    audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
                    //IsolateNarrativePiece(ref NarrativePieces, 2);
                }
            }
        }
        if (other.CompareTag("CorrectMaze"))
        {
            if (other.gameObject.Equals(MazeColliders[currentCollider])) {
                MazeColliders[currentCollider].SetActive(false);
                MazeCollidersWrong1[currentCollider].SetActive(false);
                MazeCollidersWrong2[currentCollider].SetActive(false);
                MazeTiles[currentCollider].SetActive(false);
                MazeTilesWrong1[currentCollider].SetActive(false);
                MazeTilesWrong2[currentCollider].SetActive(false);
                currentCollider++;
                if(currentCollider == 5)
                {
                    puzzlesCompleted++;
                }
            }
        }
        if (other.CompareTag("WrongMaze"))
        {
            foreach (GameObject maz in MazeColliders)
            {
                maz.SetActive(true);
            }
            foreach (GameObject maz in MazeCollidersWrong1)
            {
                maz.SetActive(true);
            }
            foreach (GameObject maz in MazeCollidersWrong2)
            {
                maz.SetActive(true);
            }
            foreach (GameObject maz in MazeTiles)
            {
                maz.SetActive(true);
            }
            foreach (GameObject maz in MazeTilesWrong1)
            {
                maz.SetActive(true);
            }
            foreach (GameObject maz in MazeTilesWrong2)
            {
                maz.SetActive(true);
            }
            currentCollider = 0;
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
            foreach (GameObject maz in MazeColliders)
            {
                maz.SetActive(false);
            }
            foreach (GameObject maz in MazeCollidersWrong1)
            {
                maz.SetActive(false);
            }
            foreach (GameObject maz in MazeCollidersWrong2)
            {
                maz.SetActive(false);
            }
            foreach (GameObject maz in MazeTiles)
            {
                maz.SetActive(false);
            }
            foreach (GameObject maz in MazeTilesWrong1)
            {
                maz.SetActive(false);
            }
            foreach (GameObject maz in MazeTilesWrong2)
            {
                maz.SetActive(false);
            }
        }
    }
}

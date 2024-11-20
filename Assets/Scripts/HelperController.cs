using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

using static NarrativeController;
using static HotbarItems;
using static AudioController;
public class HelperController : MonoBehaviour
{
    [SerializeField] private List<GameObject> IntroClues;
    [SerializeField] private List<GameObject> TutorialClues;
    [SerializeField] private List<GameObject> Zone1Clues;
    [SerializeField] private List<GameObject> Zone2Clues;
    [SerializeField] private List<GameObject> Zone3Clues;
    [SerializeField] private List<GameObject> Zone4Clues;
    [SerializeField] private List<GameObject> Zone5Clues;
    [SerializeField] private List<AudioClip> IntroCluesAudio;
    [SerializeField] private List<AudioClip> TutorialCluesAudio;
    [SerializeField] private List<AudioClip> Zone1CluesAudio;
    [SerializeField] private List<AudioClip> Zone2CluesAudio;
    [SerializeField] private List<AudioClip> Zone3CluesAudio;
    [SerializeField] private List<AudioClip> Zone4CluesAudio;
    [SerializeField] private List<AudioClip> Zone5CluesAudio;
    [SerializeField] private IntroController intro;
    [SerializeField] private ZoneOneController zone1;
    [SerializeField] private ZoneTwoController zone2;
    [SerializeField] private ZoneThreeController zone3;
    [SerializeField] private ZoneFourController zone4;
    [SerializeField] private ZoneFiveController zone5;
    [SerializeField] private PuzzleController puzzle;
    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private AudioSource pochita;
    [SerializeField] private GameObject tutorialButton;
    private bool isLoaded = false;

    private bool[] finishedTutorials = new bool[8];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoaded && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            ClueDisplay();
        }
#if UNITY_EDITOR
        if (isLoaded && Input.GetMouseButtonDown(0))
        {
            ClueDisplay();
        }
#endif
    }
    public void SetLoaded()
    {
        isLoaded = true;
    }

    public void SetNotLoaded()
    {
        ResetDialogue();

        isLoaded = false;
    }
    private void ResetDialogue()
    {
        foreach (GameObject obj in IntroClues)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in TutorialClues)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in Zone1Clues)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in Zone2Clues)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in Zone3Clues)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in Zone4Clues)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in Zone5Clues)
        {
            obj.SetActive(false);
        }
    }
    public void ClueDisplay()
    {
        Debug.Log("Clue display was called");
        if (puzzle.completed)
        {
            //Go to boss room (NYI)
        }
        else if (zone5.ghostDefeated)
        {
            //Go to puzzle
        }
        else if (zone5.mazeFinished)
        {
            ResetDialogue();
            Zone5Clues[2].SetActive(true);
            pochita.PlayOneShot(Zone5CluesAudio[2]);
            //Capture the ghost at the end of the maze
        }
        else if (zone5.inZone)
        {
            ResetDialogue();
            Zone5Clues[1].SetActive(true);
            pochita.PlayOneShot(Zone5CluesAudio[1]);
            //Walk through maze without touching the walls, capture the ghost at the end
        }
        else if (zone4.ghostDefeated)
        {
            ResetDialogue();
            Zone5Clues[0].SetActive(true);
            pochita.PlayOneShot(Zone5CluesAudio[0]);
            //Go towards the exit
        }
        else if (zone4.ghostWeakened)
        {
            ResetDialogue();
            Zone4Clues[2].SetActive(true);
            pochita.PlayOneShot(Zone4CluesAudio[2]);
            //Capture the ghost
        }
        else if (false)
        {
            ResetDialogue();
            Zone4Clues[1].SetActive(true);
            pochita.PlayOneShot(Zone4CluesAudio[1]);
            //Use the objects to weaken the ghost
        }
        else if (zone4.inZone)
        {
            ResetDialogue();
            Zone4Clues[0].SetActive(true);
            pochita.PlayOneShot(Zone4CluesAudio[0]);
            //Open up the chests with keys from previous zones
        }
        else if (zone3.keyObtained)
        {
            //Go to zone 4
        }
        else if (zone3.ghostDefeated)
        {
            ResetDialogue();
            Zone3Clues[4].SetActive(true);
            pochita.PlayOneShot(Zone3CluesAudio[4]);
            //Get the key in zone 3
        }
        else if (false)
        {
            ResetDialogue();
            Zone3Clues[3].SetActive(true);
            pochita.PlayOneShot(Zone3CluesAudio[3]);
            //Capture ghost after finishing plank puzzle
        }
        else if(zone3.mazeActive && zone3.planksObtained>0)
        {
            ResetDialogue();
            Zone3Clues[2].SetActive(true);
            pochita.PlayOneShot(Zone3CluesAudio[2]);
            //Cross the pit using the planks
        }
        else if (zone3.mazeActive)
        {
            ResetDialogue();
            Zone3Clues[1].SetActive(true);
            pochita.PlayOneShot(Zone3CluesAudio[1]);
            //Get the planks
        }
        else if(zone3.inZone)
        {
            ResetDialogue();
            Zone3Clues[0].SetActive(true);
            pochita.PlayOneShot(Zone3CluesAudio[0]);
            //Start the pit section
        }
        else if (zone2.keyObtained)
        {
            //Go to zone 3
        }
        else if(zone2.ghostDefeated)
        {
            ResetDialogue();
            Zone2Clues[4].SetActive(true);
            pochita.PlayOneShot(Zone2CluesAudio[4]);
            //Get the key in zone 2
        }
        else if(zone2.inZone && zone2.GhostStrength==0)
        {
            ResetDialogue();
            Zone2Clues[3].SetActive(true);
            pochita.PlayOneShot(Zone2CluesAudio[3]);
            //Capture the ghost
        }
        else if(zone2.inZone && zone2.GhostStrength==1 && zone2.potionObtained)
        {
            ResetDialogue();
            Zone2Clues[2].SetActive(true);
            pochita.PlayOneShot(Zone2CluesAudio[2]);
            //Use potion on the ghost
        }
        else if (zone2.inZone && zone2.GhostStrength == 1 && zone2.bottleObtained)
        {
            ResetDialogue();
            Zone2Clues[1].SetActive(true);
            pochita.PlayOneShot(Zone2CluesAudio[1]);
            //Fill up bottle with potion
        }
        else if (zone2.inZone && zone2.GhostStrength == 1)
        {
            ResetDialogue();
            Zone2Clues[0].SetActive(true);
            pochita.PlayOneShot(Zone2CluesAudio[0]);
            //Get the correct bottle
        }
        else if(zone1.keyObtained)
        {
            //Go to zone 2
        }
        else if(zone1.ghostDefeated)
        {
            ResetDialogue();
            Zone1Clues[3].SetActive(true);
            pochita.PlayOneShot(Zone1CluesAudio[3]);
            //Get key
        }
        else if(zone1.torchLit)
        {
            ResetDialogue();
            Zone1Clues[2].SetActive(true);
            pochita.PlayOneShot(Zone1CluesAudio[2]);
            //Capture ghost
        }
        else if(zone1.torchObtained)
        {
            ResetDialogue();
            Zone1Clues[1].SetActive(true);
            pochita.PlayOneShot(Zone1CluesAudio[1]);
            //Light torch
        }
        else if(zone1.inZone)
        {
            ResetDialogue();
            Zone1Clues[0].SetActive(true);
            pochita.PlayOneShot(Zone1CluesAudio[0]);
            //Grab torch from ground
        }
        else if(intro.polterdustObtained)
        {
            //Go to zone 1
        }
        else if(intro.isDoorClosed)
        {
            if (!finishedTutorials[7] && !finishedTutorials[0])
            {
                ResetDialogue();
                finishedTutorials[0] = true;
                TutorialClues[0].SetActive(true);
                pochita.PlayOneShot(TutorialCluesAudio[0]);
                tutorialButton.SetActive(true);
            }
            else if (!finishedTutorials[7]) {

            } else
            {
                ResetDialogue();
                IntroClues[1].SetActive(true);
                pochita.PlayOneShot(IntroCluesAudio[1]);
                //Get the polterdust

            }
        }
        else
        {
            ResetDialogue();
            IntroClues[0].SetActive(true);
            pochita.PlayOneShot(IntroCluesAudio[0]);
            //Enter the building
        }
    }

    public void RunTutorial()
    {
        int tutorialNum = 0;
        while (finishedTutorials[tutorialNum])
        {
            TutorialClues[tutorialNum++].SetActive(false);
        }
        finishedTutorials[tutorialNum] = true;
        TutorialClues[tutorialNum].SetActive(true);
        pochita.PlayOneShot(TutorialCluesAudio[tutorialNum]);

        if (tutorialNum == 6)
        {
            finishedTutorials[7] = true;
            tutorialButton.SetActive(false);
        }
        // Play tutorial
    }
}

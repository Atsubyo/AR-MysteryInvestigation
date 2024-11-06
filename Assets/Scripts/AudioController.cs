using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public enum GlobalAudio
    {
        Background,
        GhostActive,
        Walking,
        CluePopup,
        DoorClosing,
        GateClosing,
        GhostDeath,
        GhostDialogue,
        GhostWeaken,
        ItemAdd,
        Polterdust,
    }

    public enum ZoneAudio
    {
        TorchBurning,
        Cauldron,
        PotionSplash,
        PlankPlace,
        OpenChest,
        PuzzleFail,
        UnlockLock
    }

    private Vector3 prevPlayerPos;

    [SerializeField] private GameObject player;
    [SerializeField] private ZoneOneController zoneOne;
    [SerializeField] private ZoneTwoController zoneTwo;
    [SerializeField] private ZoneThreeController zoneThree;
    [SerializeField] private ZoneFourController zoneFour;
    [SerializeField] private ZoneFiveController zoneFive;

    [SerializeField] private List<AudioSource> globalSounds;
    [SerializeField] private List<AudioSource> zoneSounds;

    // Use this for initialization
    void Start()
    {
        prevPlayerPos = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        PlayPlayerWalking();
        PlayGhostActive();
    }

    private void PlayPlayerWalking()
    {
        Debug.Log("player.transform.position: " + player.transform.position);
        Debug.Log("prevPlayerPos: " + prevPlayerPos);
        if (player.transform.position != prevPlayerPos)
        {
            Debug.Log("Hello1");
            globalSounds[(int)GlobalAudio.Walking].mute = false;
        }
        else
        {
            Debug.Log("Hello2");
            globalSounds[(int)GlobalAudio.Walking].mute = true;
        }
        prevPlayerPos = player.transform.position;
    }

    private void PlayGhostActive()
    {
        if (zoneOne.inZone || zoneTwo.inZone || zoneThree.inZone || zoneFour.inZone || zoneFive.inZone)
        {
            globalSounds[(int)GlobalAudio.Background].mute = true;
            globalSounds[(int)GlobalAudio.GhostActive].mute = false;
        } else
        {
            globalSounds[(int)GlobalAudio.GhostActive].mute = true;
            globalSounds[(int)GlobalAudio.Background].mute = false;
        }
    }

    public void PlayGlobalSound(int select)
    {
        globalSounds[select].Play();
    }

    public void PlayZoneSound(int select)
    {
        zoneSounds[select].Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class ClueController : MonoBehaviour
{
    [SerializeField] private Transform luigi;
    [SerializeField] private Transform boo;
    [SerializeField] private Transform poltergust;
    [SerializeField] private Transform ghost;
    [SerializeField] private GameObject ZoneOneClue;
    [SerializeField] private GameObject ZoneTwoClue;
    [SerializeField] private GameObject ZoneFiveClue;
    [SerializeField] private GameObject GhostHuntingLens;
    [SerializeField] private TextMeshProUGUI LensToggleText;

    private bool GhostHunting = false;
    private bool showingClue = false;

    private bool luigiFoundPoltergust = false;
    private bool luigiFoundGhost = false; // luigi must find poltergust first
    private bool luigiFoundBoo = false; // luigi must find ghost first

    private ObserverBehaviour LuigiObserver;
    private ObserverBehaviour BooObserver;
    private ObserverBehaviour PoltergustObserver;
    private ObserverBehaviour GhostObserver;

    void Start()
    {
        ZoneOneClue.SetActive(showingClue);
        ZoneTwoClue.SetActive(showingClue);
        ZoneFiveClue.SetActive(showingClue);

        LuigiObserver = luigi.GetComponentInParent<ObserverBehaviour>();
        BooObserver = boo.GetComponentInParent<ObserverBehaviour>();
        PoltergustObserver = poltergust.GetComponentInParent<ObserverBehaviour>();
        GhostObserver = ghost.GetComponentInParent<ObserverBehaviour>();

        CreateObserver(LuigiObserver);
        CreateObserver(BooObserver);
        CreateObserver(PoltergustObserver);
        CreateObserver(GhostObserver);
    }

    private void Update()
    {
        
        if (!ZoneOneClue.activeSelf && !ZoneTwoClue.activeSelf && !ZoneFiveClue.activeSelf)
        {
            showingClue = false;
        }


        if (GhostHunting)
        {
            if (IsTargetTracked(LuigiObserver) && IsTargetTracked(BooObserver))
            {
                if (luigiFoundPoltergust && luigiFoundGhost)
                {
                    if (!showingClue)
                    {
                        OpenLetter(ref ZoneFiveClue, ref showingClue);
                        luigiFoundBoo = true;
                        Debug.Log("Luigi found king boo!");
                    }
                    else
                    {
                        Debug.Log("Close other clues first.");
                    }
                }
                else
                {
                    Debug.Log("Luigi needs to find his poltergust and ghost first!");
                }
            }
            else if (IsTargetTracked(LuigiObserver) && IsTargetTracked(GhostObserver))
            {
                if (luigiFoundPoltergust)
                {
                    if (!showingClue)
                    {
                        OpenLetter(ref ZoneTwoClue, ref showingClue);
                        luigiFoundGhost = true;
                        Debug.Log("Luigi found a ghost!");
                    }
                    else
                    {
                        Debug.Log("Close other clues first.");
                    }
                }
                else
                {
                    Debug.Log("Luigi needs to find his poltergust first!");
                }
            }
            else if (IsTargetTracked(LuigiObserver) && IsTargetTracked(PoltergustObserver))
            {
                if (!showingClue)
                {
                    OpenLetter(ref ZoneOneClue, ref showingClue);
                    luigiFoundPoltergust = true;
                    Debug.Log("Luigi found his poltergust!");
                }
                else
                {
                    Debug.Log("Close other clues first.");
                }
            }
        }
        
    }

    void OnDestroy()
    {
        DestroyObserver(LuigiObserver);
        DestroyObserver(BooObserver);
        DestroyObserver(PoltergustObserver);
        DestroyObserver(GhostObserver);
    }

    private void OpenLetter(ref GameObject clue, ref bool clueState)
    {
        clueState = true;
        clue.SetActive(clueState);
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus) {}

    private bool IsTargetTracked(ObserverBehaviour observer)
    {
        return observer != null &&
               (observer.TargetStatus.Status == Status.TRACKED);
    }

    private void CreateObserver(ObserverBehaviour observer)
    {
        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void DestroyObserver(ObserverBehaviour observer)
    {
        if (observer != null)
        {
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
        }

    }

    public void ToggleGhostHunting()
    {
        GhostHunting = !GhostHunting;
        GhostHuntingLens.SetActive(GhostHunting);
        if (GhostHunting)
        {
            LensToggleText.text = "Lens On";
        } else
        {
            LensToggleText.text = "Lens Off";
        }
    }
}

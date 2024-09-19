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
    [SerializeField] private GameObject Clue1;
    [SerializeField] private GameObject Clue2;
    [SerializeField] private GameObject Clue3;
    [SerializeField] private GameObject GhostHuntingLens;
    [SerializeField] private TextMeshProUGUI LensToggleText;

    private bool GhostHunting = false;
    private bool showClue1 = false;
    private bool showClue2 = false;
    private bool showClue3 = false;

    private bool luigiFoundPoltergust = false;
    private bool luigiFoundGhost = false; // luigi must find poltergust first
    private bool luigiFoundBoo = false; // luigi must find ghost first

    private ObserverBehaviour LuigiObserver;
    private ObserverBehaviour BooObserver;
    private ObserverBehaviour PoltergustObserver;
    private ObserverBehaviour GhostObserver;

    void Start()
    {
        Clue1.SetActive(showClue1);
        Clue2.SetActive(showClue2);
        Clue3.SetActive(showClue3);

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
        if (!Clue1.activeSelf)
        {
            showClue1 = false;
        }
        if (!Clue2.activeSelf)
        {
            showClue2 = false;
        }
        if (!Clue3.activeSelf)
        {
            showClue3 = false;
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

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (GhostHunting)
        {
            if (IsTargetTracked(LuigiObserver) && IsTargetTracked(BooObserver))
            {
                if (luigiFoundPoltergust && luigiFoundGhost)
                {
                    OpenLetter(ref Clue3, ref showClue3);
                    luigiFoundBoo = true;
                    Debug.Log("Luigi found king boo!");
                }
                else
                {
                    Debug.Log("Luigi needs to find his poltergust and ghost first!");
                }
            } else if (IsTargetTracked(LuigiObserver) && IsTargetTracked(GhostObserver))
            {
                if (luigiFoundPoltergust)
                {
                    if (!showClue1)
                    {
                        OpenLetter(ref Clue2, ref showClue2);
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
            } else if (IsTargetTracked(LuigiObserver) && IsTargetTracked(PoltergustObserver))
            {
                if (!showClue2)
                {
                    OpenLetter(ref Clue1, ref showClue1);
                    luigiFoundPoltergust = true;
                    Debug.Log("Luigi found his poltergust!");
                } else
                {
                    Debug.Log("Close other clues first.");
                }
            }
        }
    }

    private bool IsTargetTracked(ObserverBehaviour observer)
    {
        return observer != null &&
               (observer.TargetStatus.Status == Status.TRACKED ||
                observer.TargetStatus.Status == Status.EXTENDED_TRACKED);
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

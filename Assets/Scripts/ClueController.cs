using UnityEngine;
using Vuforia;

public class ClueController : MonoBehaviour
{
    [SerializeField] private Transform luigi;
    [SerializeField] private Transform boo;
    [SerializeField] private Transform poltergust;
    [SerializeField] private Transform ghost;
    [SerializeField] private GameObject GhostHuntingLens;

    private bool GhostHunting = false;

    private ObserverBehaviour LuigiObserver;
    private ObserverBehaviour BooObserver;
    private ObserverBehaviour PoltergustObserver;
    private ObserverBehaviour GhostObserver;

    void Start()
    {
        LuigiObserver = luigi.GetComponentInParent<ObserverBehaviour>();
        BooObserver = boo.GetComponentInParent<ObserverBehaviour>();
        PoltergustObserver = poltergust.GetComponentInParent<ObserverBehaviour>();
        GhostObserver = ghost.GetComponentInParent<ObserverBehaviour>();

        CreateObserver(LuigiObserver);
        CreateObserver(BooObserver);
        CreateObserver(PoltergustObserver);
        CreateObserver(GhostObserver);
    }

    void OnDestroy()
    {
        DestroyObserver(LuigiObserver);
        DestroyObserver(BooObserver);
        DestroyObserver(PoltergustObserver);
        DestroyObserver(GhostObserver);
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus) {}

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
    }
}

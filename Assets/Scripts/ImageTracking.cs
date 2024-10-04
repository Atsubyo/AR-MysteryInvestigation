using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTracking : DefaultObserverEventHandler
{
    private bool GhostHunting = false;
    private bool isTracked = false;

    protected override void OnTrackingFound()
    {
        isTracked = true;
        if (mObserverBehaviour && GhostHunting)
            SetComponentsEnabled(true);

        OnTargetFound?.Invoke();
    }

    protected override void OnTrackingLost()
    {
        isTracked = false;
        if (mObserverBehaviour)
            SetComponentsEnabled(false);

        OnTargetLost?.Invoke();
    }

    public void ToggleGhostHunting()
    {
        GhostHunting = !GhostHunting;
        if(isTracked)
        {
            SetComponentsEnabled(GhostHunting);
        }
    }

    void SetComponentsEnabled(bool enable)
    {
        var components = VuforiaRuntimeUtilities.GetComponentsInChildrenExcluding<Component, DefaultObserverEventHandler>(gameObject);
        foreach (var component in components)
        {
            switch (component)
            {
                case Renderer rendererComponent:
                    rendererComponent.enabled = enable;
                    break;
                case Collider colliderComponent:
                    colliderComponent.enabled = enable;
                    break;
                case Canvas canvasComponent:
                    canvasComponent.enabled = enable;
                    break;
                case RuntimeMeshRenderingBehaviour runtimeMeshComponent:
                    runtimeMeshComponent.enabled = enable;
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

using static HotbarItems;

public class DebugToggle : MonoBehaviour
{
    [SerializeField] private List<GameObject> menus = null;
    [SerializeField] private MeshRenderer mesh = null;
    [SerializeField] private GameObject mainCamera = null;
    [SerializeField] private GameObject environmentScene = null;

    //[SerializeField] private List<Transform> zoneStartPositions;

    [SerializeField] private ZoneOneController zoneOne;
    [SerializeField] private ZoneTwoController zoneTwo;
    [SerializeField] private ZoneThreeController zoneThree;
    [SerializeField] private ZoneFourController zoneFour;
    [SerializeField] private ZoneFiveController zoneFive;
    [SerializeField] private HotbarController hotbarController = null;

    public void ToggleDebugMenu(GameObject debugMenu)
    {
        debugMenu.SetActive(!debugMenu.activeSelf);
    }

    public void ToggleMenu(GameObject toggleMenu)
    {
        foreach (GameObject menu in menus)
        {
            if (menu.Equals(toggleMenu))
            {
                menu.SetActive(!menu.activeSelf);
            } else
            {
                menu.SetActive(false);
            }
        }
    }

    public void ToggleRendering()
    {
        mesh.enabled = !mesh.enabled;
    }

    public void MoveSpawn()
    {
        environmentScene.transform.position = new Vector3(13.7299995f, 6.5f, 13.0699997f);
    }

    public void MoveZone1()
    {
        //Vector3 moveToPos = zoneStartPositions[0].position;
        //Quaternion rotationPos = Quaternion.identity;
        //MoveCamera(moveToPos, rotationPos);
        environmentScene.transform.position = new Vector3(11f, 6.5f, 7f);
    }

    public void MoveZone2()
    {
        //Vector3 moveToPos = zoneStartPositions[1].position;
        //Quaternion rotationPos = Quaternion.identity;
        //MoveCamera(moveToPos, rotationPos);
        environmentScene.transform.position = new Vector3(12f, 6.5f, -0.5f);
    }

    public void MoveZone3()
    {
        //Vector3 moveToPos = zoneStartPositions[2].position;
        //Quaternion rotationPos = Quaternion.identity;
        //MoveCamera(moveToPos, rotationPos);
        environmentScene.transform.position = new Vector3(0.5f, 6.5f, 13f);
    }

    public void MoveZone4()
    {
        //Vector3 moveToPos = zoneStartPositions[3].position;
        //Quaternion rotationPos = Quaternion.identity;
        //MoveCamera(moveToPos, rotationPos);
        environmentScene.transform.position = new Vector3(-0.25f, 6.5f, -2f);
    }

    public void MoveZone5()
    {
        //Vector3 moveToPos = zoneStartPositions[4].position;
        //Quaternion rotationPos = Quaternion.identity;
        //MoveCamera(moveToPos, rotationPos);
        environmentScene.transform.position = new Vector3(0.2f, 6.5f, 11.3f);
    }

    public void AddItemToHotbar(string itemName)
    {
        if (itemName == FULL_FLASK)
        {
            hotbarController.DeleteFromHotbar(EMPTY_FLASK);
        } else if (itemName == EMPTY_FLASK)
        {
            hotbarController.DeleteFromHotbar(FULL_FLASK);
        }
        if (itemName == PLANK || hotbarController.FindHotbarItem(itemName) == -1)
        {
            hotbarController.AddToHotbar(itemName);
        }
    }

    public void ClearHotbar()
    {
        hotbarController.ClearHotbar();
    }

    public void CompleteZone(int zoneNum)
    {
        switch (zoneNum)
        {
            case 1:
                zoneOne.ghostDefeated = true;
                break;
            case 2:
                zoneTwo.ghostDefeated = true;
                break;
            case 3:
                zoneThree.ghostDefeated = true;
                break;
            case 4:
                zoneFour.ghostDefeated = true;
                break;
            case 5:
                zoneFive.ghostDefeated = true;
                break;
            default:
                break;
        }
    }

    public void MoveCamera(Vector3 pos, Quaternion rotation)
    {
        Vuforia.VuforiaBehaviour.Instance.DevicePoseBehaviour.enabled = false;
        //move AR camera position
        mainCamera.transform.position = pos;
        //set AR camera rotation
        Vuforia.VuforiaBehaviour.Instance.DevicePoseBehaviour.RecenterPose();
        Vuforia.VuforiaBehaviour.Instance.DevicePoseBehaviour.enabled = true;
    }
}

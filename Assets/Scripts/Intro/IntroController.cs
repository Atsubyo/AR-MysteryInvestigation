using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static NarrativeController;
using static HotbarItems;
using static AudioController;

public class IntroController : MonoBehaviour
{
    public bool isDoorClosed = false;
    public bool polterdustObtained = false;

    [SerializeField] private Collider IntroTwoZone;
    [SerializeField] private List<GameObject> NarrativePieces;

    [SerializeField] private GameObject Polterdust;
    [SerializeField] private GameObject MainEntranceDoor;

    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private AudioController audioController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(IntroTwoZone) && !isDoorClosed)
        {
            audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
            IsolateNarrativePiece(ref NarrativePieces, 1);

            // update y axis rotation to 90 for MainEntranceDoor
            audioController.PlayGlobalSound((int)GlobalAudio.DoorClosing);
            MainEntranceDoor.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
            isDoorClosed = true;
        }
        if (other.gameObject.Equals(Polterdust))
        {
            // Disable the tapped object
            Polterdust.SetActive(false);

            audioController.PlayGlobalSound((int)GlobalAudio.CluePopup);
            IsolateNarrativePiece(ref NarrativePieces, 2);

            // Add the tapped object to the hotbar via HotbarController
            if (hotbarController != null)
            {
                hotbarController.AddToHotbar(POLTERDUST);
            }
        }

    }
}

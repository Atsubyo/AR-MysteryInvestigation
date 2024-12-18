using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static AudioController;
using static HotbarItems;

public class HotbarController : MonoBehaviour
{
    [SerializeField] private AudioController audioController;

    [SerializeField] private GameObject[] hotbar = new GameObject[5];
    [SerializeField] private TextMeshProUGUI[] hotbarText = new TextMeshProUGUI[5];
    [SerializeField] private TextMeshProUGUI[] hotbarCounter = new TextMeshProUGUI[5];
    [SerializeField] private List<RawImage> InventoryIconImages;
    [SerializeField] private List<Texture> InventoryItemTextures;
    [SerializeField] private GameObject Tooltip;
    private int selectedSlot = 0;

    private Coroutine hideTooltipCoroutine;

    public TextMeshProUGUI[] HotbarText => hotbarText;
    public int SelectedSlot => selectedSlot;

    public void SelectSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < hotbar.Length)
        {
            selectedSlot = slotIndex;
            Tooltip.SetActive(true);
            TextMeshProUGUI tooltipText = Tooltip.GetComponentInChildren<TextMeshProUGUI>();
            tooltipText.text = hotbarText[selectedSlot].text;
            UpdateHotbarUI();

            if (hideTooltipCoroutine != null)
                StopCoroutine(hideTooltipCoroutine);

            hideTooltipCoroutine = StartCoroutine(HideTooltipAfterDelay(1f));
        }
    }

    private IEnumerator HideTooltipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Tooltip.SetActive(false);
    }

    private void UpdateHotbarUI()
    {
        for (int i = 0; i < hotbar.Length; i++)
        {
            Image slotImage = hotbar[i].GetComponent<Image>();
            if (slotImage != null)
            {
                if (i == selectedSlot)
                {
                    slotImage.color = Color.gray;
                }
                else
                {
                    slotImage.color = Color.white;
                }
            }

            TextMeshProUGUI slotText = hotbarText[i];
            if (slotText != null)
            {
                if (i == selectedSlot)
                {
                    slotText.color = Color.white;
                    slotText.faceColor = Color.white;
                }
                else
                {
                    slotText.color = Color.gray;
                    slotText.faceColor = Color.gray;
                }
            }

            TextMeshProUGUI counterText = hotbarCounter[i];
            if (counterText != null)
            {
                if (i == selectedSlot)
                {
                    counterText.color = Color.white;
                    counterText.faceColor = Color.white;
                }
                else
                {
                    counterText.color = Color.gray;
                    counterText.faceColor = Color.gray;
                }
            }
        }

    }

    public void AddToHotbar(string itemName)
    {
        bool isItemPresent = false;
        for (int i = 0; i < hotbar.Length; ++i)
        {
            TextMeshProUGUI slotText = hotbar[i].GetComponentInChildren<TextMeshProUGUI>();
            if (slotText != null && slotText.text == itemName)
            {
                isItemPresent = true;
                break;
            }
        }
        for (int i = 0; i < hotbar.Length; ++i)
        {
            TextMeshProUGUI[] slotTexts = hotbar[i].GetComponentsInChildren<TextMeshProUGUI>();
            if (isItemPresent)
            {
                if (slotTexts[0] != null && slotTexts[0].text == itemName)
                {
                    int.TryParse(slotTexts[1].text, out int counter);
                    slotTexts[1].text = (++counter).ToString();

                    audioController.PlayGlobalSound((int)GlobalAudio.ItemAdd);
                    break;
                }
            }
            else
            {
                if (slotTexts[0] != null && slotTexts[0].text == "N/A")
                {
                    slotTexts[0].text = itemName;
                    slotTexts[1].text = "1";

                    audioController.PlayGlobalSound((int)GlobalAudio.ItemAdd);

                    int itemIdx = SelectInventoryItem(itemName);
                    InventoryIconImages[i].texture = InventoryItemTextures[itemIdx];
                    break;
                }
            }
        }
    }

    public void SetHotbarItemCount(string itemName, int total)
    {
        int itemIdx = FindHotbarItem(itemName);
        int.TryParse(hotbarCounter[itemIdx].text, out int counter);
        if (counter <= total)
        {
            hotbarCounter[itemIdx].text = total.ToString();
        }
    }

    // Deletes all of [itemName] from hotbar
    public void DeleteFromHotbar(string itemName)
    {
        for (int i = 0; i < hotbar.Length; ++i)
        {
            TextMeshProUGUI[] slotTexts = hotbar[i].GetComponentsInChildren<TextMeshProUGUI>();
            if (slotTexts[0] != null && slotTexts[0].text == itemName)
            {
                slotTexts[0].text = "N/A";
                slotTexts[1].text = "0";

                int itemIdx = SelectInventoryItem("N/A");
                InventoryIconImages[i].texture = InventoryItemTextures[itemIdx];
                break;
            }
        }
    }

    // Removes just one of [itemName] from hotbar
    public void RemoveFromHotbar(string itemName)
    {
        for (int i = 0; i < hotbar.Length; ++i)
        {
            TextMeshProUGUI[] slotTexts = hotbar[i].GetComponentsInChildren<TextMeshProUGUI>();
            if (slotTexts[0] != null && slotTexts[0].text == itemName)
            {
                int.TryParse(slotTexts[1].text, out int counter);
                slotTexts[1].text = (--counter).ToString();
                if (counter == 0)
                {
                    slotTexts[0].text = "N/A";

                    int itemIdx = SelectInventoryItem("N/A");
                    InventoryIconImages[i].texture = InventoryItemTextures[itemIdx];
                }
                break;
            }
        }
    }

    public void ClearHotbar()
    {
        for (int i = 0; i < hotbar.Length; ++i)
        {
            TextMeshProUGUI[] slotTexts = hotbar[i].GetComponentsInChildren<TextMeshProUGUI>();
            slotTexts[0].text = "N/A";
            slotTexts[1].text = "0";

            int itemIdx = SelectInventoryItem("N/A");
            InventoryIconImages[i].texture = InventoryItemTextures[itemIdx];
        }
    }

    public void RenameHotbarItem(string oldName, string newName)
    {
        for (int i = 0; i < hotbar.Length; ++i)
        {
            TextMeshProUGUI[] slotTexts = hotbar[i].GetComponentsInChildren<TextMeshProUGUI>();
            if (slotTexts[0] != null && slotTexts[0].text == oldName)
            {
                slotTexts[0].text = newName;
            }
        }
    }

    public bool CheckSelectedHotbarSlot(string itemName)
    {
        TextMeshProUGUI[] slotTexts = hotbar[selectedSlot].GetComponentsInChildren<TextMeshProUGUI>();
        if (slotTexts[0] != null && slotTexts[0].text == itemName)
        {
            return true;
        }
        return false;
    }

    public int FindHotbarItem(string itemName)
    {
        for (int i = 0; i < hotbar.Length; ++i)
        {
            TextMeshProUGUI[] slotTexts = hotbar[i].GetComponentsInChildren<TextMeshProUGUI>();
            if (slotTexts[0] != null && slotTexts[0].text == itemName)
            {
                return i;
            }
        }
        return -1;
    }

    public int SelectInventoryItem(string itemName)
    {
        switch (itemName)
        {
            case POLTERDUST:
                return (int)InventoryIcons.Polterdust;
            case HAND_TORCH:
                return (int)InventoryIcons.HandTorch;
            case SILVER_KEY:
                return (int)InventoryIcons.SilverKey;
            case EMPTY_FLASK:
                return (int)InventoryIcons.EmptyFlask;
            case FULL_FLASK:
                return (int)InventoryIcons.FullFlask;
            case GOLD_KEY:
                return (int)InventoryIcons.GoldKey;
            case PLANK:
                return (int)InventoryIcons.Plank;
            case RUSTY_KEY:
                return (int)InventoryIcons.RustyKey;
            default:
                return (int)InventoryIcons.EmptySlot;
        }
    }
}

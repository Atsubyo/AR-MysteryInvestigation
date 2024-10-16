using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarController : MonoBehaviour
{
    [SerializeField] private GameObject[] hotbar = new GameObject[5];
    [SerializeField] private TextMeshProUGUI[] hotbarText = new TextMeshProUGUI[5];
    [SerializeField] private TextMeshProUGUI[] hotbarCounter = new TextMeshProUGUI[5];
    private int selectedSlot = 0;

    public TextMeshProUGUI[] HotbarText => hotbarText;
    public int SelectedSlot => selectedSlot;

    public void SelectSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < hotbar.Length)
        {
            selectedSlot = slotIndex;
            UpdateHotbarUI();
        }
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
                    slotImage.color = Color.black;
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
                    slotText.color = Color.black;
                    slotText.faceColor = Color.black;
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
                    counterText.color = Color.black;
                    counterText.faceColor = Color.black;
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
                    break;
                }
            }
            else
            {
                if (slotTexts[0] != null && slotTexts[0].text == "N/A")
                {
                    slotTexts[0].text = itemName;
                    slotTexts[1].text = "1";
                    break;
                }
            }
        }
    }

    public void DeleteFromHotbar(string itemName)
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
                }
                break;
            }
        }
    }
}

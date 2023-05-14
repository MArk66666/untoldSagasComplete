using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCharacterSetup : MonoBehaviour
{
    [SerializeField] private GameObject dialogueSection;
    [SerializeField] private Image characterIcon;
    [SerializeField] private Text characterName;

    public void SetupCharacter(Character character)
    {
        SetCharacterIcon(character.Icon);
        SetCharacterName(character.Name);
    }

    public void ToggleDialogueVisibility(bool value)
    {
        dialogueSection.SetActive(value);    
    }

    private void SetCharacterIcon(Sprite icon)
    {
        characterIcon.sprite = icon;
    }

    private void SetCharacterName(string name)
    {
        characterName.text = name;
    }
}

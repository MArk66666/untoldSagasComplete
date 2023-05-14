using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBackgroundInteractable
{
    void SetBackground(Sprite image);
}

[RequireComponent(typeof(LocalizationManager))]
[RequireComponent(typeof(TextVisualizer))]
[RequireComponent(typeof(DialogueCharacterSetup))]
[RequireComponent(typeof(AudioController))]
public class SceneComponentsHolder : MonoBehaviour, IBackgroundInteractable
{
    [SerializeField] private Image background;
    [SerializeField] private Text title;

    [SerializeField] private AudioClip defaultClickSound;

    private EventsManager _eventsManager;
    private DialogueCharacterSetup _dialogueManager;
    private TextVisualizer _textVisualizer;
    private AudioController _audioController;

    private void Awake()
    {
        _eventsManager = GetComponent<EventsManager>();
        _textVisualizer = GetComponent<TextVisualizer>();
        _dialogueManager = GetComponent<DialogueCharacterSetup>();
        _audioController = GetComponent<AudioController>();

        _textVisualizer.SetTitleField(title);
    }

    public void SetBackground(Sprite image)
    {
        if (background.sprite != image)
        {
            StartCoroutine(ChangeBackground(image));
        }
    }

    private IEnumerator ChangeBackground(Sprite newImage)
    {
        float transitionSpeed = 1f;


        while (background.color.a > 0f)
        {
            float alpha = background.color.a - (transitionSpeed * Time.deltaTime);
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
            yield return null;
        }

        background.sprite = newImage;

        while (background.color.a < 1f)
        {
            float alpha = background.color.a + (transitionSpeed * Time.deltaTime);
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
            yield return null;
        }
    }

    private void CheckForDialogue(GameEvent currentEvent)
    {
        if (currentEvent.Dialogue)
        {
            Character character = currentEvent.DialogueCharacter;
            _dialogueManager.SetupCharacter(character);
        }

        _dialogueManager.ToggleDialogueVisibility(currentEvent.Dialogue);
    }

    public void SetUpScene(int currentEventID)
    {
        Chapter currentChapter = _eventsManager.GetCurrentChapter();
        GameEvent currentEvent = currentChapter.GetEvent(currentEventID);

        CheckForDialogue(currentEvent);
        SetBackground(currentEvent.Background);

        _audioController.SetAmbience(currentEvent.Ambience);
        _textVisualizer.WriteTitle(currentEvent.Title);
    }

    public AudioClip GetDefaultClickSound()
    {
        return defaultClickSound;
    }
}
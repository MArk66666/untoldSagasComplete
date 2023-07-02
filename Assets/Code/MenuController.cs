using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Text continueButton;
    [SerializeField] private Dropdown localizaitionDropDown;

    [Header("Localization files")]
    [SerializeField] private TextAsset russian;
    [SerializeField] private TextAsset english;

    private void Start()
    {
        if (DataPersistenceManager.DataPersistence.HasSavedData())
        {
            continueButton.color = Color.white;
        }
        else
        {
            continueButton.color = Color.gray;
        }

        CheckLocalizationIndex();
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);  
    }

    public void TogglePanel(GameObject panel)
    {
        bool active = panel.activeSelf;
        active = !active;
        panel.SetActive(active);
    }

    public void CheckLocalizationIndex()
    {
        switch (localizaitionDropDown.value)
        {
            case 0:
                DataPersistenceManager.DataPersistence.SetLocalizationFile(russian);
                break;
            case 1:
                DataPersistenceManager.DataPersistence.SetLocalizationFile(english);
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

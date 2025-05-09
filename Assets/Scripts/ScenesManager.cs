using UnityEditor.Timeline.Actions;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject mainMenu;
    //public GameObject characterSelect;
    public GameObject fightingRing;
    public GameObject optionsMenu;

    public GameObject previous = null;

    void Start()
    {
        ShowTitleScreen();
    }

    public void ShowTitleScreen()
    {
        SetActivePanel(titleScreen);
    }

    public void ShowMainMenu()
    {
        MainMenu.MenuEntry entry = MainMenu.MenuEntry.Title; // default

        if (previous == optionsMenu)
        {
            entry = MainMenu.MenuEntry.Options;
        }
        else if (previous == titleScreen)
        {
            entry = MainMenu.MenuEntry.Title;
        }
        else if (previous == fightingRing)
        {
            entry = MainMenu.MenuEntry.Versus;
        }

        MainMenu menuScript = mainMenu.GetComponent<MainMenu>();
        if (menuScript != null)
        {
            menuScript.SetMenuEntry(entry);
        }
        SetActivePanel(mainMenu);
    }

    /*
    public void ShowCharacterSelect()
    {
        SetActivePanel(characterSelect);
    }
    */
    public void ShowFightingRing()
    {
        SetActivePanel(fightingRing);
    }
    public void ShowOptionsMenu()
    {
        SetActivePanel(optionsMenu);
    }

    private void SetActivePanel(GameObject activePanel)
    {
        // Disable all panels first
        titleScreen.SetActive(false);
        mainMenu.SetActive(false);
        //characterSelect.SetActive(false);
        fightingRing.SetActive(false);
        optionsMenu.SetActive(false);

        // Enable the one we want
        activePanel.SetActive(true);

        previous = activePanel;
    }
}

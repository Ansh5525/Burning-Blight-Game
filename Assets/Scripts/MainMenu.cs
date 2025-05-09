using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    public enum MenuEntry
    {
        Title,
        Options,
        Versus
    }

    public void SetMenuEntry(MenuEntry source)
    {
        menuEntry = source;
    }

    public MenuEntry menuEntry;

    public UnityEvent onSelectOptions;


    public Animator versusIcon;
    public Animator optionsIcon;
    public Animator exitIcon;

    public Animator versusShade;
    public Animator optionsShade;
    public Animator exitShade;

    public Animator versusFire;
    public Animator optionsFire;
    public Animator exitFire;

    public Animator fireRise;
    public Animator fire;

    public GameObject shade;



    int MenuSelect;
    const int MaxMenu = 3;
    const int MinMenu = 1;

    const int shadeSpeed = 50;

    bool moveableSelection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (versusIcon != null) versusIcon.gameObject.SetActive(false);
        if (optionsIcon != null) optionsIcon.gameObject.SetActive(false);
        if (exitIcon != null) exitIcon.gameObject.SetActive(false);

        if (versusShade != null) versusShade.gameObject.SetActive(false);
        if (optionsShade != null) optionsShade.gameObject.SetActive(false);
        if (exitShade != null) exitShade.gameObject.SetActive(false);

        if (versusFire != null) versusFire.gameObject.SetActive(false);
        if (optionsFire != null) optionsFire.gameObject.SetActive(false);
        if (exitFire != null) exitFire.gameObject.SetActive(false);

        if (fireRise != null) fireRise.gameObject.SetActive(false);
        if (fire != null) fire.gameObject.SetActive(true);

        if (shade != null)
        {
            shade.SetActive(false);
            shade.transform.position = new Vector3(-0.03f, 8.55f, -5f);
        }

        moveableSelection = false;
        if (menuEntry == MenuEntry.Title)
        {
            StartCoroutine(FromTitleOnLoadAnimation());
        }
        else if(menuEntry == MenuEntry.Options)
        {
            StartCoroutine(FromOptionsOnLoadAnimation());
        }
    }

    IEnumerator FromTitleOnLoadAnimation()
    {
        versusShade.gameObject.SetActive(true);
        optionsShade.gameObject.SetActive(true);
        exitShade.gameObject.SetActive(true);
        yield return new WaitUntil(() => versusShade.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        yield return new WaitUntil(() => versusShade.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);
        versusShade.SetTrigger("LoopStart");
        optionsShade.SetTrigger("LoopStart");
        exitShade.SetTrigger("LoopStart");
        yield return new WaitForSeconds(0.5f);
        SetAllIconFireOn();
        versusFire.SetBool("FireOut", false);
        optionsFire.SetBool("FireOut", false);
        exitFire.SetBool("FireOut", false);
        versusIcon.gameObject.SetActive(true);
        optionsIcon.gameObject.SetActive(true);
        exitIcon.gameObject.SetActive(true);
        versusIcon.SetBool("Bright",false);
        optionsIcon.SetBool("Bright", false);
        exitIcon.SetBool("Bright", false);
        yield return new WaitForSeconds(0.75f);
        versusFire.SetBool("FireOut", true);
        optionsFire.SetBool("FireOut", true);
        exitFire.SetBool("FireOut", true);

        SetAllIconBright();
        yield return new WaitUntil(() => versusFire.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        yield return new WaitUntil(() => versusFire.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);
        SetAllIconFireOff();

        versusFire.Play("versusFireLoop", 0, 0f);
        optionsFire.Play("optionsFireLoop", 0, 0f);
        exitFire.Play("exitLoop", 0, 0f);

        yield return new WaitForSeconds(1f);
        MenuSelect = 1;

        moveableSelection = false;
        SelectMenuItem(versusIcon, versusFire, "versusFireLoop");
    }

    IEnumerator FromOptionsOnLoadAnimation()
    {
        yield return null;
        versusShade.gameObject.SetActive(true);
        optionsShade.gameObject.SetActive(true);
        exitShade.gameObject.SetActive(true);
        versusShade.SetTrigger("LoopStart");
        optionsShade.SetTrigger("LoopStart");
        exitShade.SetTrigger("LoopStart");
        versusIcon.gameObject.SetActive(true);
        optionsIcon.gameObject.SetActive(true);
        exitIcon.gameObject.SetActive(true);
        SelectMenuItem(optionsIcon, optionsFire, "optionsFireLoop");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && MenuSelect < MaxMenu && moveableSelection)
        {
            MenuSelect++;
            Debug.Log("Menu: " + MenuSelect);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && MenuSelect > MinMenu && moveableSelection)
        {
            MenuSelect--;
            Debug.Log("Menu: " + MenuSelect);
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Return)) && moveableSelection)
        {
            moveableSelection = false;
            switch (MenuSelect)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        StartCoroutine(EnterMenuItem(versusFire));
                    }
                    else
                    {
                        SelectMenuItem(versusIcon, versusFire, "versusFireLoop");
                    }
                    break;

                case 2:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        StartCoroutine(EnterMenuItem(optionsFire));
                    }
                    else
                    {
                        SelectMenuItem(optionsIcon, optionsFire, "optionsFireLoop");
                    }
                    break;

                case 3:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        StartCoroutine(EnterMenuItem(exitFire));   
                    }
                    else
                    {
                        SelectMenuItem(exitIcon, exitFire, "exitFireLoop");
                    }
                    break;
            }
        }
    }

    void VersusMenuTransition()
    {
        fireRise.gameObject.SetActive(true);
        //this function is to be made an IEnumerator type to wait for sometime before the scene is to be changed to versus menu
    }

    IEnumerator OptionsMenuTransition()
    {
        shade.SetActive(true);
        Vector3 targetPosition = new Vector3(
        shade.transform.position.x,
        -5f,
        shade.transform.position.z
        );

        while (shade.transform.position.y > -5f)
        {
            shade.transform.position = Vector3.MoveTowards(
                shade.transform.position,
                targetPosition,
                shadeSpeed * Time.deltaTime
            );

            yield return null;
        }

        onSelectOptions.Invoke();
    }

    IEnumerator EnterMenuItem(Animator fire)
    {
        fire.SetBool("FireOut", true);
        yield return new WaitUntil(() => fire.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        yield return new WaitUntil(() => fire.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);
        fire.gameObject.SetActive(false);
        if(fire == versusFire)
        {
            VersusMenuTransition();
        }
        else if(fire == optionsFire)
        {
            StartCoroutine(OptionsMenuTransition());
        }
        else if(fire == exitFire)
        {
            Application.Quit();

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }

    void SelectMenuItem(Animator icon, Animator fire, string fireAnimation)
    {
        SetAllIconFireOff();
        SetAllIconBright();

        fire.gameObject.SetActive(true);
        icon.SetBool("Bright", false);

        moveableSelection = true;
    }

    void SetAllIconFireOff()
    {
        versusFire.gameObject.SetActive(false);
        optionsFire.gameObject.SetActive(false);
        exitFire.gameObject.SetActive(false);
    }

    void SetAllIconFireOn()
    {
        versusFire.gameObject.SetActive(true);
        optionsFire.gameObject.SetActive(true);
        exitFire.gameObject.SetActive(true);
    }

    void SetAllIconBright()
    {
        versusIcon.SetBool("Bright", true);
        optionsIcon.SetBool("Bright", true);
        exitIcon.SetBool("Bright", true);
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Options : MonoBehaviour
{
    public GameObject shade;

    public UnityEvent PressEscape;


    const int shadeSpeed = 50;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            StartCoroutine(OptionsMenuTransition());
        }
    }

    void OnEnable()
    {
        shade.transform.position = new Vector3(shade.transform.position.x, -7.42f, shade.transform.position.z);
    }

    IEnumerator OptionsMenuTransition()
    {
        //shade.SetActive(true);
        Vector3 targetPosition = new Vector3(
        shade.transform.position.x,
        10f,
        shade.transform.position.z
        );

        while (shade.transform.position.y < 10f)
        {
            shade.transform.position = Vector3.MoveTowards(
                shade.transform.position,
                targetPosition,
                shadeSpeed * Time.deltaTime
            );

            yield return null;
        }

        PressEscape.Invoke();
    }
}

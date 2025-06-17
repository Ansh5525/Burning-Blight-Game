using System.Collections;
using UnityEngine;

public class CharSelection : MonoBehaviour
{
    public CharData charData;

    [SerializeField] public bool available;
    public bool isP1Here;
    public bool isP2Here;

    [SerializeField] public SpriteRenderer crossIcon;
    [SerializeField] public SpriteRenderer p1Marker;
    [SerializeField] public SpriteRenderer p2Marker;

    [SerializeField] SpriteRenderer iconRenderer;

    private Coroutine alternateRoutine;

    private void OnEnable()
    {
        setIcon();
    }

    void setIcon()
    {
        if (charData != null && iconRenderer != null)
        {
            iconRenderer.sprite = charData.icon;
            iconRenderer.enabled = true;
        }
    }

    private void Update()
    {
        if (isP1Here && isP2Here)
        {
            if (alternateRoutine == null)
                alternateRoutine = StartCoroutine(AlternateMarkers());
        }
        else
        {
            if (alternateRoutine != null)
            {
                StopCoroutine(alternateRoutine);
                alternateRoutine = null;

                // Reset both to proper visibility
                if (p1Marker != null) p1Marker.enabled = isP1Here;
                if (p2Marker != null) p2Marker.enabled = isP2Here;
            }
        }
    }

    IEnumerator AlternateMarkers()
    {
        while (true)
        {
            if (p1Marker != null) p1Marker.enabled = true;
            if (p2Marker != null) p2Marker.enabled = false;
            yield return new WaitForSeconds(0.5f);

            if (p1Marker != null) p1Marker.enabled = false;
            if (p2Marker != null) p2Marker.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UpdateVisual()
    {
        if (crossIcon != null) crossIcon.enabled = !available;

        //if (portraitNeutral != null) portraitNeutral.enabled = !unavailable;
        //if (portraitSelected != null) portraitSelected.enabled = false;

        if (p1Marker != null) p1Marker.enabled = isP1Here;
        if (p2Marker != null) p2Marker.enabled = isP2Here;
    }

    public void SetMarkers(bool p1, bool p2)
    {
        isP1Here = p1;
        isP2Here = p2;

        if (p1Marker != null) p1Marker.enabled = p1;
        if (p2Marker != null) p2Marker.enabled = p2;
    }
}

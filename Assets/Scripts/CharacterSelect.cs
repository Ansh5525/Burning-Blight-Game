using System.Collections;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject CharSelect;
    public GameObject BGFire;

    public Animator readyP1;
    public Animator readyP2;

    private SpriteRenderer bgFireRenderer;

    public float CharSelectSpeed = 20f;
    void OnEnable()
    {
        if(readyP1 != null) readyP1.gameObject.SetActive(false);
        if(readyP2 != null) readyP2.gameObject.SetActive(false);
        if(BGFire != null) { 
            BGFire.SetActive(true);
            bgFireRenderer = BGFire.GetComponent<SpriteRenderer>();
            if (bgFireRenderer != null)
            {
                StartCoroutine(DimBGFire());
            }
        }
        if (CharSelect != null) {
            CharSelect.transform.position = new Vector3(CharSelect.transform.position.x, -5.2242368f, CharSelect.transform.position.z);
        }
        StartCoroutine(OnLoadAnimation());
    }

    IEnumerator OnLoadAnimation()
    {
        Vector3 targetPosition = new Vector3(
        CharSelect.transform.position.x,
        -1.4986212f,
        CharSelect.transform.position.z
        );

        while (CharSelect.transform.position.y < -1.4986212f)
        {
            CharSelect.transform.position = Vector3.MoveTowards(
                CharSelect.transform.position,
                targetPosition,
                CharSelectSpeed * Time.deltaTime
            );

            yield return null;
        }
    }

    IEnumerator DimBGFire()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        Color startColor = bgFireRenderer.color;
        Color targetColor = new Color(startColor.r * 0.5f, startColor.g * 0.5f, startColor.b * 0.5f, startColor.a); // 50% dimmer

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            bgFireRenderer.color = Color.Lerp(startColor, targetColor, t);

            yield return null;
        }

        bgFireRenderer.color = targetColor;
    }

    IEnumerator setReadyP1()
    {
        readyP1.gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.05f);
        yield return new WaitUntil(() => readyP1.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        yield return new WaitUntil(() => readyP1.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            //StartCoroutine(setReadyP1());
            readyP1.gameObject.SetActive(true);
        }
    }
}

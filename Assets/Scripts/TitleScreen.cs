using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TitleScreen : MonoBehaviour
{
    public UnityEvent onAnyKeyPressed; // Assign this event in the Inspector
    //public UnityEvent TitleScreenToMenu;
    public Animator fireAnim;
    public Animator titleAnim;
    public float titleDuration;

    private bool flag = true;

    void Update()
    {
        if (Input.anyKeyDown && flag) // Detect any key press
        {
            StartCoroutine(TitleScreenAnim());
            flag = false;
        }
    }

    IEnumerator TitleScreenAnim()
    {
        if(titleAnim != null)
        {
            titleAnim.SetTrigger("out");
            yield return new WaitUntil(() => titleAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
            yield return new WaitUntil(() => titleAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);
            titleAnim.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.07f);
        if(fireAnim != null)
        {
            fireAnim.SetTrigger("fireOut");
            yield return new WaitUntil(() => fireAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
            yield return new WaitUntil(() => fireAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);
            fireAnim.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(titleDuration);
        onAnyKeyPressed.Invoke(); // Trigger the event
    }
}

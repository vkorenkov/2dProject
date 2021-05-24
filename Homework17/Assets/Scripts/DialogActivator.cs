using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    Output output;
    [SerializeField, Multiline] string phrases;
    [SerializeField] float displayTime;
    [SerializeField] bool destroy = true;

    private void Awake()
    {
        output = GameObject.Find("Hud").GetComponent<Output>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            output.timer = displayTime;
            output.goTimer = true;
            output.OutputDialog(phrases);
            if(destroy) Destroy(gameObject);
        }
    }

    #region variant
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        animationActivator.AnimationPlayback(output.dialogText, false);
    //        Destroy(gameObject);
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out Phrases dialogs))
    //    {
    //        output.OutputDialog(dialogs.dialogs.FirstOrDefault());
    //        animationActivator.AnimationPlayback(output.dialogText, true);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out Phrases dialogs))
    //    {
    //        animationActivator.AnimationPlayback(output.dialogText, false);
    //        dialogs.dialogs.Remove(dialogs.dialogs.FirstOrDefault());
    //        Destroy(gameObject);
    //    }
    //}
    #endregion
}

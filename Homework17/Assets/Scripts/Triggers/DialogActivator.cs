using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    /// <summary>
    /// Объект вывода информации на экран
    /// </summary>
    Output output;
    /// <summary>
    /// Текущая фраза персонажа
    /// </summary>
    [SerializeField, Multiline] string phrases;
    /// <summary>
    /// Время отображения фразы на экране
    /// </summary>
    [SerializeField] float displayTime;
    /// <summary>
    /// Указание необходимости уничтожения активатора фразы
    /// </summary>
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

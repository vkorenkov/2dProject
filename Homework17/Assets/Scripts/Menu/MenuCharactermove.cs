using UnityEngine;

public class MenuCharactermove : MonoBehaviour
{
    /// <summary>
    /// RigitBody ��
    /// </summary>
    [SerializeField] Rigidbody2D characterRb;

    private void Start()
    {
        GetComponentInChildren<Animator>().SetBool("Run", true); // ������ �������� ����
    }
}

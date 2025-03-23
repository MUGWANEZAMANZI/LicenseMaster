using TMPro;
using UnityEngine;
using System.Collections;

public class ViolationDialogWarning : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI text;
    [SerializeField] public float flashSpeed = .6f;

    private bool flashViolationTextCoroutine = false;

    void Update()
    {
        if (!flashViolationTextCoroutine) StartCoroutine(flashViolationText());
    }

    private IEnumerator flashViolationText()
    {
        flashViolationTextCoroutine = true;
        text.gameObject.SetActive(false); 
        yield return new WaitForSeconds(flashSpeed/2);
        text.gameObject.SetActive(true); 
        yield return new WaitForSeconds(flashSpeed/2);
        flashViolationTextCoroutine = false;
    }

}
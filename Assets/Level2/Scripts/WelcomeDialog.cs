using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WelcomeDialog : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public float speed = 0.05f;
    public GameObject[] carChoice;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Welcome(new string[]
        {
            "Welcome to the License Master",
            "This is a series of realistic traffic Exams",
            "All intended to test your driving skills",
            "Are you ready to start the first exam?"
        }
        ) );   
    }


    //Coroutine definition
    public IEnumerator Welcome(string[] lines)
    {
        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(speed);
        }
    }

    public IEnumerator TypeLine(string line)
    {
        dialogText.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(speed);
        }
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            text.SetActive(false);
            for (int i = 0; i < carChoice.Length-1; i++)
            {
                //Selecing an individul object to enable it.
                carChoice[i].SetActive(true);
            }

        }
    }

}

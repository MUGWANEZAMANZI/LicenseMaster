using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialDialog : MonoBehaviour
{
        [Header("References")]
        [SerializeField] public TextMeshProUGUI textComponent;

        [Header("Wwise")]
        [SerializeField] public AK.Wwise.Event MenuText;
        [SerializeField] public AK.Wwise.Event RadioStart;

        
        public string[] lines;
        public float textSpeed;
        public int index;

        void Start()
        {
            textComponent.text = string.Empty;
            StartDialog();
        }

        void Update() 
        {
            if (Input.anyKeyDown)
            {
                if (textComponent.text == lines[index]) {
                    NextLine();
                    return;
                }

                StopAllCoroutines();
                textComponent.text = lines[index];
            }

        }

        void StartDialog()
        {
            index = 0;
            StartCoroutine(TypeLine());

        }

        private IEnumerator TypeLine()
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                MenuText.Post(gameObject);
                yield return new WaitForSeconds(textSpeed);
            }
        }

        private void NextLine()
        {
            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;;
                StartCoroutine(TypeLine());
            } 
            else {
                //Check if AutoLevelStart has already started the level before calling the Start level function.
                RadioStart.Post(gameObject);
                gameObject.SetActive(false);
            }

        }

}
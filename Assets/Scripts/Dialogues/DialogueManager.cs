using System.Collections.Generic;
using Basics;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField]
        private List<Dialogue> dialogues;

        [SerializeField]
        private TextMeshProUGUI textField;

        private int index;

        private void Awake()
        {
            var doNotDestroy = FindObjectOfType<DoNotDestroy>();
            if (doNotDestroy != null)
            {
                Destroy(doNotDestroy.gameObject);
            }
            
            if (dialogues.Count <= 0)
            {
                return;
            }

            textField.text = dialogues[0].Text;
            index = 1;
        }

        private void Update()
        {
            if (!Input.GetButtonDown("Fire1"))
            {
                return;
            }

            if (dialogues.Count <= index)
            {
                SceneHelper.LoadNextOrFirstScene();
                return;
            }
            
            textField.text = dialogues[index].Text;
            index++;
        }
    }
}

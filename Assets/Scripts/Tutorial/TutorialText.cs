using UnityEngine;

namespace Tutorial
{
    public class TutorialText : MonoBehaviour
    {
        [SerializeField]
        [TextArea]
        private string tutorialText;

        public string Text => tutorialText;
    }
}
using UnityEngine;

namespace Dialogues
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField]
        [TextArea]
        private string text;

        public string Text => text;
    }
}
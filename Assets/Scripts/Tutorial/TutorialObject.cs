using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tutorial
{
    public class TutorialObject : MonoBehaviour
    {
        [SerializeField]
        private List<TutorialText> tutorialTexts;

        [SerializeField]
        private TextMeshProUGUI textField;

        [SerializeField]
        private GameObject contentContainer;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private Camera mainCamera;

        public bool IsChildActive => contentContainer.activeSelf;
        
        private int index;
        private bool isTutorialCompleted;

        private void Awake()
        {
            mainCamera = FindObjectOfType<Camera>();
            textField.text = tutorialTexts[index].Text;
        }

        private void Update()
        {
            if (IsTutorialObjectVisible() && !isTutorialCompleted)
            {
                contentContainer.SetActive(true);
            }

            if (!IsChildActive || !Input.GetButtonDown("Fire1") || isTutorialCompleted)
            {
                return;
            }

            index++;
            if (index >= tutorialTexts.Count)
            {
                contentContainer.SetActive(false);
                isTutorialCompleted = true;
                return;
            }

            textField.text = tutorialTexts[index].Text;
        }

        private bool IsTutorialObjectVisible()
        {
            var viewportPoint = mainCamera.WorldToViewportPoint(spriteRenderer.transform.position);
            return viewportPoint.x is >= 0 and <= 1 && viewportPoint.y is >= 0 and <= 1;
        }
    }
}

using UnityEngine;
using World.Models;

namespace World
{
    public class WorldState : MonoBehaviour
    {
        private WorldModel worldModel;
        private Camera mainCamera;

        private Color startColor;

        private void Awake()
        {
            worldModel = FindObjectOfType<WorldModel>();
            mainCamera = FindObjectOfType<Camera>();
            startColor = mainCamera.backgroundColor;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!Input.GetButtonDown("Dimension"))
            {
                return;
            }
            
            worldModel.SwitchWorld();
            mainCamera.backgroundColor =  worldModel.isShadowWorld ? Color.black : startColor;
        }
        
    }
}
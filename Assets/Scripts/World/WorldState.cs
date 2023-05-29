using UnityEngine;
using World.Models;

namespace World
{
    public class WorldState : MonoBehaviour
    {
        [SerializeField]
        private GameObject filterUiEffect;
        
        private WorldModel worldModel;

        private void Awake()
        {
            worldModel = FindObjectOfType<WorldModel>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!Input.GetButtonDown("Dimension"))
            {
                return;
            }
            
            worldModel.SwitchWorld();
            filterUiEffect.SetActive(worldModel.isShadowWorld);
        }
        
    }
}
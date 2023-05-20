using UnityEngine;

namespace World.Models
{
    public class WorldModel : MonoBehaviour
    {
        public bool isShadowWorld { get; protected set; }
        
        protected internal void SwitchWorld()
        {
            isShadowWorld = !isShadowWorld;
        }
    }
}
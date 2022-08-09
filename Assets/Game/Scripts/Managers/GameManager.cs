using Game.Scripts.Behaviours;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        public Vector3 dragObjectOffsetValue= new Vector3(0,1f,0);
        public float slotSizeMultiplier = 0.5f;
    
        private void Awake()
        {
        }
    }
}

using Core.Modules;
using UnityEngine;
using Weapons;

namespace Systems.UI.Interact
{
    public class InteractModule : MonoBehaviour, IModule, IAfterInitModule
    {
        private ModuleOwner _owner;
        private Weapon _weapon;
        [SerializeField] private GameObject visual;

        private bool _canInteract = true;
        
        public void Initialize(ModuleOwner owner)
        {
            _owner = owner;
            
            if (owner is Weapon weapon)
                _weapon = weapon;
            
        }
        public void AfterInitialize()
        {
            visual.SetActive(false); 
        }

        private void Update()
        {
            _canInteract = !_weapon.IsInHandler;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_canInteract) return;
            
            visual.SetActive(true);
            
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_canInteract) return;
            
            visual.SetActive(false);
        }

    }
}
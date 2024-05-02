using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using Photon.Realtime;

namespace Gun
{
    public class BreakableObject : MonoBehaviour, IObstacle, IBreakable
    {

        #region 프로퍼티
        [SerializeField] private BreakableSO _breakableSO;
        [SerializeField] private GameObject _visual;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private int _health = 0;
        #endregion

        public BreakableSO BreakableSO => _breakableSO;

        public int Health { get => _health; set => _health = value; }

        protected virtual void Awake()
        {
            _health = _breakableSO.MaxHealth;
        }

        #region IBreakable
        public void OnBreakWithMeleeWeapon(int damage)
        {
            _health -= (int)(damage * _breakableSO.MeleeWeaponMultiplier);
            CheckBreak();
        }

        public void OnBreakWithRangeWeapon(int damage)
        {
            _health -= (int)(damage * _breakableSO.RangeWeaponMultiplier);
            CheckBreak();
        }

        public void OnBreakWithPickaxe(int damage)
        {
            _health -= (int)(damage * _breakableSO.PickaxeDamageMultiplier);
            CheckBreak();
        }

        public void OnBreakWithAxe(int damage)
        {
            _health -= (int)(damage * _breakableSO.AxeDamageMultiplier);
            CheckBreak();
        }

        public void OnBreakWithShovel(int damage)
        {
            _health -= (int)(damage * _breakableSO.ShovelDamageMultiplier);
            CheckBreak();
        }

        public void OnBreakWithOthers(int damage)
        {
            _health -= (int)(damage * _breakableSO.OtherDamageMultiplier);
            CheckBreak();
        }
        public void CheckBreak()
        {
            if(_health <= 0) {
                Debug.Log("Broke!");
                _visual.SetActive(false);
                _particleSystem.Play();

                Destroy(gameObject, 2f);
            }
        }

        
        #endregion
    }
}


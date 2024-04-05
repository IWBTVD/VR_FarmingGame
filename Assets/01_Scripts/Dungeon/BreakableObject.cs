using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Gun
{
    public class BreakableObject : MonoBehaviour, IObstacle, IBreakable
    {

        #region 프로퍼티
        [SerializeField] private BreakableSO _breakableSO;

        [SerializeField] private float health = 100;
        #endregion

        public BreakableSO BreakableSO;
        public float Health { get => health; set => health = value; }

        #region IBreakable
        public void OnBreakWithMeleeWeapon(int damage)
        {

        }

        public void OnBreakWithRangeWeapon(int damage)
        {

        }

        public void OnBreakWithPickaxe(int damage)
        {
            health -= damage * _breakableSO.PickaxeDamageMultiplier;
            CheckBreak();
        }

        public void OnBreakWithAxe(int damage)
        {
            health -= damage * _breakableSO.AxeDamageMultiplier;
            CheckBreak();
        }

        public void OnBreakWithShovel(int damage)
        {
            health -= damage * _breakableSO.ShovelDamageMultiplier;
            CheckBreak();
        }

        public void OnBreakWithOthers(int damage)
        {
            health -= damage * _breakableSO.OtherDamageMultiplier;
            CheckBreak();
        }
        public void CheckBreak()
        {

        }

        
        #endregion
    }
}


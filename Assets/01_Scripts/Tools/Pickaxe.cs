using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

namespace Gun
{
    public class Pickaxe : MonoBehaviour
    {
        private int _damage = 20;
        public int Damage => _damage;
        [SerializeField] private CollisionDetector collisionDetector;

        private void Awake()
        {
            collisionDetector.OnEnter.AddListener(EnterCollision);
        }
        private void EnterCollision(Collision collision)
        {
            //일정 가속도 이상에서만 호출
            if(collision.relativeVelocity.magnitude > 2f)
            {
                if (collision.gameObject.TryGetComponent(out IBreakable breakable))
                {
                    breakable.OnBreakWithPickaxe(_damage);
                }
            }
        }
    } 
}
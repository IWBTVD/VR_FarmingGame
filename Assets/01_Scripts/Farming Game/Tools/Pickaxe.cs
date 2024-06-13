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

        private void OnCollisionEnter(Collision collision) {
            //일정 가속도 이상에서만 호출
            Debug.Log("PickaxeCollided " + collision.relativeVelocity.magnitude);
            if(collision.relativeVelocity.magnitude >= 1f)
            {
                if(collision.gameObject.tag == "Obstacle") {
                    var breakable = collision.gameObject.GetComponentInParent<IBreakable>();

                    if(breakable != null) {
                        Debug.Log("Mining Performed");
                        breakable.OnBreakWithPickaxe(_damage);
                    }
                }
            }
        }
    } 
}
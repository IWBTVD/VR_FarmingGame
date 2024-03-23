using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

namespace Jun
{
    public class WateringCan : MonoBehaviourPunCallbacks, IPunObservable
    {
        public float maxWaterAmount = 100f;
        public float currentWaterAmount;

        // Add other necessary variables, like particle system, angles, etc.
        public bool playAura = false;
        private ParticleSystem particleObject;

        private void Start()
        {
            currentWaterAmount = maxWaterAmount;
            particleObject = GetComponent<ParticleSystem>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Well"))
            {
                photonView.RPC("RefillWater", RpcTarget.AllBuffered);
            }
        }

        [PunRPC]
        private void RefillWater()
        {
            currentWaterAmount = maxWaterAmount;
        }

        private void Update()
        {
            //float currentXAngle = transform.rotation.eulerAngles.x;
            //Debug.Log(currentXAngle);
            if (photonView.IsMine)
            {
                // Implement logic for lowering the watering can and decreasing water amount
                float currentXAngle = transform.rotation.eulerAngles.x;

                if ((currentXAngle >= 35f && currentXAngle <= 100f) && currentWaterAmount >= 0)
                {
                    playAura = true;
                    currentWaterAmount -= 0.1f;
                }
                else
                {
                    playAura = false;
                }

                if (!playAura)
                {
                    particleObject.Stop();
                }
                else
                {
                    particleObject.Play();
                }

            }
        }

        // 내가 닿은 오브젝트에 대한 정보를 출력하는 함수(테스트용)
        // void OnParticleCollision(GameObject other)
        // {
        //     // Print the name of the object that the particle collided with
        //     Debug.Log(other.name);
        // }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(currentWaterAmount);
                stream.SendNext(playAura);
            }
            else
            {
                currentWaterAmount = (float)stream.ReceiveNext();
                playAura = (bool)stream.ReceiveNext();
            }
        }
    }

}
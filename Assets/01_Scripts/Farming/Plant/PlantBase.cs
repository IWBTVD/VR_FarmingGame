using Jun.Ground.Crops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun 
{
    public class PlantBase : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> sproutVisualList;
        [SerializeField] protected GameObject matureVisual;

        protected CropPoint _cropPoint;
        public CropPoint CropPoint => _cropPoint;
        protected CultivationField _cultivationField;
        public CultivationField CultivationField
        {
            get
            {
                //if(_cultivationField == null)
                return _cultivationField;
            }
        }



        protected virtual void Grow()
        {

        }
    }
    
}

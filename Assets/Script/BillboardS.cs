using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class BillboardS : MonoBehaviour
    {
        public Transform cam;
        // Start is called before the first frame update
        private void LateUpdate()
        {
            transform.LookAt(transform.position +  cam.forward);
        }
    }
}

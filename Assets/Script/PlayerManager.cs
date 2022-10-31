using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class PlayerManager : MonoBehaviour
    {
        #region Sigleton
        public static PlayerManager instance;

        private void Awake()
        {
            instance = this;
        }

        #endregion

        public GameObject player;

    }
}

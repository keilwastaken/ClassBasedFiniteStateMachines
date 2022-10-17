using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI.Base
{
    public abstract class BaseFSM
    {

        protected readonly GameObject gameObject;
        
        protected readonly Transform transform;
        
        public BaseFSM(GameObject _GameObject)
        {
            gameObject = _GameObject;
            transform = _GameObject.transform;
        }
        
        public abstract Type tick();
    }
}

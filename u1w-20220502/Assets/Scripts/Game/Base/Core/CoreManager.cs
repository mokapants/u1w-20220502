using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Base.Core
{
    public class CoreManager : MonoBehaviour
    {
        private Transform cameraTransform;

        protected virtual void Start()
        {
            cameraTransform = Camera.main.transform;
            
            var tmp = cameraTransform.position;
            tmp.y = 35f;
            cameraTransform.position = tmp;

            PlayLoadedSceneAnimation();
        }

        protected void PlayLoadAnySceneAnimation()
        {
            cameraTransform.DOMoveY(-15f, 2f);
        }

        protected void PlayLoadedSceneAnimation()
        {
            cameraTransform.DOMoveY(10f, 2f);
        }
    }
}
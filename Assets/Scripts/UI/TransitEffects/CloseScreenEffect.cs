﻿using AppCoreModule.Scripts.Extensions;
using AppCoreModule.Scripts.UI.TransitEffects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Services;
using UnityEngine;
using Zenject;

namespace UI.TransitEffects
{
    public class CloseScreenEffect : ITransitEffect
    {
        private const float Time = 0.3f;

        public async UniTask RunAsync(GameObject targetGameObject)
        {
            var audioService = ProjectContext.Instance.Container.Resolve<AudioService>();
            audioService.PlayWoosh();
            var rectTransform = targetGameObject.GetComponent<RectTransform>();
            rectTransform.pivot = rectTransform.anchorMax.SetNew(y: -0.5f);
            rectTransform.DOScale(1.2f, Time);
            await rectTransform.DOLocalRotate(Vector3.forward * -90f, Time).AsyncWaitForCompletion();
        }
    }
}
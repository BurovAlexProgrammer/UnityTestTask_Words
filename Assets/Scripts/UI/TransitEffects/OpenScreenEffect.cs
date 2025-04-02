using AppCoreModule.Scripts.Extensions;
using AppCoreModule.Scripts.UI.TransitEffects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI.TransitEffects
{
    public class OpenScreenEffect : ITransitEffect
    {
        private const float Time = 0.5f;

        public async UniTask RunAsync(GameObject targetGameObject)
        {
            var rectTransform = targetGameObject.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.pivot = rectTransform.anchorMax.SetNew(y: -0.5f);
            rectTransform.DOScale(1f, Time);
            rectTransform.eulerAngles = rectTransform.eulerAngles.SetNew(z: 90f);
            await rectTransform.DOLocalRotate(Vector3.forward * 0f, Time).AsyncWaitForCompletion();
        }
    }
}
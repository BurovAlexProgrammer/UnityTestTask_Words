using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MaskController : MonoBehaviour
    {
        [SerializeField] private MaskableGraphic[] _maskables;

        public void SetEnableMasks(bool isEnabled)
        {
            for (var i = 0; i < _maskables.Length; i++)
            {
                _maskables[i].maskable = isEnabled;

                if (_maskables[i] is TextMeshProUGUI textMeshProUGUI)
                {
                    textMeshProUGUI.RecalculateMasking();
                    _maskables[i]
                        .GetComponentsInChildren<TMP_SubMeshUI>(true)
                        .ForEach(x => x.maskable = isEnabled);
                }
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Models;
using Services;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace GameCore.LevelControl.Views
{
    public class ClusterView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Inject] private AudioService _audioService;
        [Inject] private ScreenService _screenService;

        [SerializeField] private TextMeshProUGUI _textView;
        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private MaskController _maskController;

        private const string ClustersPanel = "ClustersPanel";

        private ClusterPlaceholderView _prevClusterPlaceholder;
        private RectTransform _rectTransform;
        private Transform _originalParent;
        private int _originalSiblingIndex;

        /// <summary>
        /// Return this cluster and hovered objects on drag end
        /// </summary>
        public event Action<ClusterView, ClusterPlaceholderView> DroppedToPlaceholder;

        public event Action<ClusterView, ClusterPlaceholderView> DroppedToPanel;

        public Cluster Cluster { get; private set; }

        public ClusterPlaceholderView PrevClusterPlaceholder => _prevClusterPlaceholder;

        public void Init(Cluster cluster)
        {
            Cluster = cluster;
            _textView.text = cluster.Letters;
            _layoutElement.ignoreLayout = false;
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalParent = _rectTransform.parent;
            _originalSiblingIndex = transform.GetSiblingIndex();
            transform.SetParent(_screenService.DragDropCanvas.transform);
            _maskController.SetEnableMasks(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var placeholder = eventData.pointerEnter.GetComponent<ClusterPlaceholderView>();

            if (placeholder != null && placeholder.HasCluster == false)
            {
                _audioService.PlayBoolk();
                DroppedToPlaceholder?.Invoke(this, placeholder);
                _prevClusterPlaceholder = placeholder;
                Debug.Log($"DroppedToPlaceholder: cluster[{gameObject.name}] to placeholder[{placeholder.gameObject.name}]");
                return;
            }

            if (eventData.hovered.Any(x => x.CompareTag(ClustersPanel)) && _originalParent.CompareTag(ClustersPanel) == false)
            {
                var parent = _originalParent.GetComponent<ClusterPlaceholderView>();
                _audioService.PlayBoolk();
                DroppedToPanel?.Invoke(this, parent);
                _prevClusterPlaceholder = null;
                Debug.Log($"DroppedToPanel: cluster[{gameObject.name}] to panel");
                return;
            }

            ReturnBack();
        }

        public void PlaceTo(Transform placeTransform)
        {
            transform.SetParent(placeTransform);
        }

        private void ReturnBack()
        {
            transform.SetParent(_originalParent);
            transform.SetSiblingIndex(_originalSiblingIndex);
        }
    }
}
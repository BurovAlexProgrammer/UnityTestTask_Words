using System;
using AppCoreModule.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Context
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private ScreenService _screenService;

        private void Start()
        {
            _screenService.Init(true);
            // _screenService.OpenWindow();
        }
    }
}
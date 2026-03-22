using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Test
{
    [RequireComponent(typeof(UIDocument))]
    public class MainPanelUI : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private VisualElement _root;
        private VisualElement _popUpWindow;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            _root = _uiDocument.rootVisualElement;
            VisualElement topContainer = _root.Q<VisualElement>("TopContainer");
            
            topContainer.RegisterCallback<ClickEvent>(HandleButtonClick);
            _popUpWindow = _root.Q<VisualElement>("PopUpWindow");
            
            DataButton closeBtn = _root.Q<DataButton>("CloseBtn");
            closeBtn.RegisterCallback<ClickEvent>(evt => _popUpWindow.RemoveFromClassList("open"));;
        }

        private void HandleButtonClick(ClickEvent evt)
        {
            if (evt.target is DataButton { ButtonIndex: 1 } dataButton)
            {
                OpenPopUpWindow();
            }
        }

        private void OpenPopUpWindow()
        {
            _popUpWindow.AddToClassList("open");
        }
    }
}
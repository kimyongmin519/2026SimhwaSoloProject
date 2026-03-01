using System;
using _06.GameLib.Runtime;
using UnityEngine.UIElements;

namespace _06.GameLib.Editor
{
    public class PoolItemView
    {
        private Label _nameLabel;
        private Label _warningLabel;
        private Button _deleteButton;
        private VisualElement _rootElement;

        public event Action<PoolItemView> OnDeleteEvent;
        public event Action<PoolItemView> OnSelectEvent;

        public string Name
        {
            get => _nameLabel.text;
            set => _nameLabel.text = value;
        }
        
        public PoolItemSO TargetItem { get; private set; }

        public bool IsActive
        {
            get => _rootElement.ClassListContains("active"); //엑티브 클래스 소유 시 활성화 취급
            set => _rootElement.EnableInClassList("active", value); //value값에 따라 엑티브 클래스 넣다빼기
        }

        public bool IsEmpty
        {
            get => _warningLabel.ClassListContains("on");
            set => _warningLabel.EnableInClassList("on", value);
        }

        public PoolItemView(VisualElement rootElement, PoolItemSO targetItem)
        {
            TargetItem = targetItem;
            _rootElement = rootElement.Q("PoolItem");
            _nameLabel = rootElement.Q<Label>("ItemName");
            _deleteButton = _rootElement.Q<Button>("DeleteBtn");
            _warningLabel =  _rootElement.Q<Label>("WarningLabel");
            
            _deleteButton.RegisterCallback<ClickEvent>(evt =>
            {
                OnDeleteEvent?.Invoke(this);
                evt.StopPropagation(); //부모로 전파되는 것을 막아준다
            });
            
            _rootElement.RegisterCallback<ClickEvent>(evt =>
            {
                OnSelectEvent?.Invoke(this);
                evt.StopPropagation(); //모든 컴퓨터의 UI시스템은 내가 클릭되면 클릭이 되었다는 신호를 부모까지 전파시킨다.
                //(버튼에 그림을 클릭해도 버튼의 로직도 작동되는 이유
            });
        }
    }
}
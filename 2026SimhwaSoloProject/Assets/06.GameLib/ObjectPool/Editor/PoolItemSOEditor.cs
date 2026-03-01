using _06.GameLib.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _06.GameLib.Editor
{
    [CustomEditor(typeof(PoolItemSO))]
    public class PoolItemSOEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset editorView = default;

        private TextField _nameField;
        private Button _changeButton;
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            editorView.CloneTree(root);
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            _nameField = root.Q<TextField>("PoolingName");
            _changeButton = root.Q<Button>("ChangeBtn");

            _changeButton.clicked += HandleChangeButtonClick;
            _nameField.RegisterCallback<KeyDownEvent>(HandleKeyDownEvent);

            return root;
        }
        
        private void HandleChangeButtonClick()
        {
            string newName = _nameField.text;
            if (string.IsNullOrEmpty(newName))
            {
                EditorUtility.DisplayDialog("Error", "Name is empty", "Ok");
                return;
            }

            string assetPath = AssetDatabase.GetAssetPath(target);
            
            //이 함수는 성공적으로 변환 시 null을 반환하고, 에러내용을 반환한다.
            string message = AssetDatabase.RenameAsset(assetPath, newName);
            if (string.IsNullOrEmpty(message))
            {
                target.name = newName;
            }
            else
            {
                EditorUtility.DisplayDialog("Error", message, "Ok");
            }
        }
        private void HandleKeyDownEvent(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.Return)
            {
                HandleChangeButtonClick();
            }
        }
    }
}
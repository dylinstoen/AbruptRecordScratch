using UnityEngine;
namespace _Project.Scripts.MainMenu {
    public class LevelSelectView : MonoBehaviour {
        [SerializeField]
        private MenuNavigationController navigation;
        public void OnReturnPressed() {
        navigation.GoBack();
    }
}
}

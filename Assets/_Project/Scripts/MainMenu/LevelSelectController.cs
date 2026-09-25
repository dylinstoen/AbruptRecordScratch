using UnityEngine;
using _Project.Scripts.UI.Navigation;
namespace _Project.Scripts.MainMenu {
    public class LevelSelectController : MonoBehaviour {
        [SerializeField]
        private MenuNavigationController navigation;
        public void OnReturnPressed() {
        navigation.RequestBack();
    }
}
}

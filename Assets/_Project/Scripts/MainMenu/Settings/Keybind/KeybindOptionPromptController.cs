using _Project.Scripts.MainMenu;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using _Project.Scripts.UI.Navigation;
public class KeybindOptionPromptController : MonoBehaviour
{
    [SerializeField] private TMP_Text _actionText;
    [SerializeField] private TMP_Text _bindingText;
    [SerializeField] private MenuPage keybindPromptMenuPage;


    KeybindMenuController _owner;

    private InputAction _action;
    
    private int _bindingIndex;
    private MenuNavigationController _navigation;

    public void Initialize(KeybindMenuController owner, MenuNavigationController navigation) {
        _owner = owner;
        _navigation = navigation;
        gameObject.SetActive(false);
        //Hide();
    }

    public void Show(string actionLabel, string bindingLabel, MenuOption optionThatOpenedIt) {
        _actionText.text = actionLabel;
        _bindingText.text = bindingLabel;
        _navigation.OpenSubmenu(keybindPromptMenuPage, optionThatOpenedIt);
    }

    public void Replace() {
        _owner.ReplaceBinding();
    }
    public void Remove() {
        _owner.RemoveBinding();
    }

    public void Close() {
        _owner.CloseBindingOptions();
    }

    public void Hide() {
        //gameObject.SetActive(false);
        _navigation.RequestBack();        
    }
}

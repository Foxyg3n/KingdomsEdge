using System.Collections;
using UnityEngine;

public class MenuUI : MonoBehaviour {

    [SerializeField] private Menu currentMenu;

    [SerializeField] private Menu mainMenu;
    [SerializeField] private Menu saveMenu;
    [SerializeField] private Menu optionsMenu;

    private bool isSwitching;

    public void SwitchMenu(Menu menu) {
        if(!isSwitching) StartCoroutine(SwitchMenuTask(menu));
    }

    public IEnumerator SwitchMenuTask(Menu newMenu) {
        isSwitching = true;
        if(currentMenu != null) yield return currentMenu.Hide();
        yield return newMenu.Show();

        currentMenu = newMenu;
        isSwitching = false;
    }

    public void ChangeMasterVolume(float volume) {
        App.AudioManager.ChangeMasterVolume(volume);
    }

    public void ChangeMusicVolume(float volume) {
        App.AudioManager.ChangeMusicVolume(volume);
    }

    public void ChangeSFXVolume(float volume) {
        App.AudioManager.ChangeSFXVolume(volume);
    }

    public void OnBack(UnityEngine.InputSystem.InputValue value) {
        if(currentMenu is MainMenu || isSwitching) return;
        SwitchMenu(mainMenu);
    }

}

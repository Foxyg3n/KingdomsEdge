using System.Collections;
using UnityEngine;

public abstract class Menu : MonoBehaviour {

    public bool menuEnabled;
    protected bool isAnimating;
    
    public abstract IEnumerator Show();
    public abstract IEnumerator Hide();

    public void Toggle() {
        if(isAnimating) return;
        if(menuEnabled) {
            StartCoroutine(Hide());
            menuEnabled = false;
        } else {
            gameObject.SetActive(true);
            StartCoroutine(Show());
            menuEnabled = true;
        }
    }

}

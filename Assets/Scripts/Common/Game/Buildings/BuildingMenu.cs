using System;
using System.Collections;
using UnityEngine;

public class BuildingMenu : MonoBehaviour {

    public EventHandler buildingOptionChooseEvent = delegate { };

    private float angle = 15; // 25  -  15
    private float radius = 0.8f; // 0.5f  -  0.8f
    public bool initialized { get; private set; }

    private int iconSelectorSpeed = 2;
    private bool finishedSelectorMove = true;
    private float originAngle = 0;
    private IconID currentIcon = IconID.EMPTY;

    private IconSelector iconSelector;

    public void InitializeMenu(MenuIcon[] menuIcons) {
        if(menuIcons == null || menuIcons.Length == 0) return;

        Icon[] icons = new Icon[menuIcons.Length];

        if(menuIcons.Length % 2 == 0) {
            for(int i = 0; i < menuIcons.Length; i++) {
                icons[i] = SpawnIcon(getIconPosition(90 - angle / 2 + angle * menuIcons.Length / 2 - i * angle, radius), menuIcons[i], angle * menuIcons.Length / 2 - i * angle - angle / 2);
            }
            iconSelector = SpawnIconSelector(getIconPosition(90 + angle / 2, radius), Quaternion.AngleAxis(angle / 2, new Vector3(0, 0, 1)));
            iconSelector.tilt = angle / 2;
        } else {
            for(int i = 0; i < menuIcons.Length; i++) {
                icons[i] = SpawnIcon(getIconPosition(90 + angle * (int) (menuIcons.Length / 2) - i * angle, radius), menuIcons[i], angle * (int) (menuIcons.Length / 2) - i * angle);
            }
            iconSelector = SpawnIconSelector(new Vector2(0, radius), Quaternion.identity);
        }
        initialized = true;
        foreach(Icon icon in icons) {
            icon.clickEvent += IconClick;
            icon.hoverEvent += IconHover;
        }
        originAngle = iconSelector.tilt;
    }

    private Vector2 getIconPosition(float angle, float radius) {
        float x = Mathf.Cos(angle / 180 * Mathf.PI) * radius;
        float y = Mathf.Sin(angle / 180 * Mathf.PI) * radius;
        return new Vector2(x, y);
    }

    private Icon SpawnIcon(Vector2 position, MenuIcon iconData, float tilt) {
        GameObject iconObject = PrefabUtils.InstantiatePrefabRelative("Buildings/Icons/Icon", position, this.transform);
        iconObject.GetComponent<SpriteRenderer>().sprite = iconData.sprite;
        Icon icon = iconObject.GetComponent<Icon>();
        icon.iconID = iconData.iconID;
        icon.tilt = tilt;
        return icon;
    }

    private IconSelector SpawnIconSelector(Vector2 position, Quaternion rotation) {
        GameObject iconSelectorObject = PrefabUtils.InstantiatePrefabRelative("Buildings/Icons/IconSelector", position, rotation, this.transform);
        return iconSelectorObject.GetComponent<IconSelector>();
    }

    public void EnableMenu() {
        if(initialized == false) return;
        gameObject.SetActive(true);
    }

    public void DisableMenu() {
        if(initialized == false) return;
        gameObject.SetActive(false);
    }

    public void IconClick(object obj, EventArgs args) {
        Icon icon = (Icon) obj;
        buildingOptionChooseEvent.Invoke(this, new IconEventArgs(icon.iconID));
    }

    public void IconHover(object obj, EventArgs args) {
        Icon icon = (Icon) obj;
        if(currentIcon != icon.iconID) {
            finishedSelectorMove = true;
            StartCoroutine(MoveSelector(icon.tilt));
            currentIcon = icon.iconID;
        }
    }

    public IEnumerator MoveSelector(float targetAngle) {
        yield return new WaitForFixedUpdate();
        finishedSelectorMove = false;
        while(!finishedSelectorMove) {
            originAngle += targetAngle > originAngle ? 
                Mathf.Max(0.3f, iconSelectorSpeed * Math.Abs(originAngle - targetAngle) / 15) :
                Mathf.Min(-0.3f, -iconSelectorSpeed * Math.Abs(originAngle - targetAngle) / 15);
            iconSelector.transform.SetPositionAndRotation(this.transform.position + (Vector3) getIconPosition(originAngle + 90, radius), Quaternion.AngleAxis(originAngle, new Vector3(0, 0, 1)));
            if(Math.Abs(originAngle - targetAngle) < 0.2) {
                iconSelector.tilt = targetAngle;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

}

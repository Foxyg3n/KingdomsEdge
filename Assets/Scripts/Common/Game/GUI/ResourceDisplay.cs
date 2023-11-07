using System;
using TMPro;
using UnityEngine;

public class ResourceDisplay : MonoBehaviour {

    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text sizeText;

    public String count {
        get { return countText.text; }
        set { countText.SetText(value); }
    }

    public String size {
        get { return sizeText.text; }
        set { sizeText.SetText(value); }
    }

}

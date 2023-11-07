using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    private Dictionary<int, SaveController> saves = new Dictionary<int, SaveController>();

    public void RegisterSave(SaveController save) {
        saves.Add(save.saveId, save);
    }

    public SaveController GetSaveController(int saveId) {
        return saves[saveId];
    }

}

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannelSO", menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject {

    public UnityAction OnEventRaised;

    public void RaiseEvent() {
        if(OnEventRaised != null) OnEventRaised();
    }

}

using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
    /// <summary>
    /// A standard button that sends an event when clicked.
    /// </summary>
    [AddComponentMenu("UI/PixelButton", 30)]
    public class PixelButton : Selectable, IPointerClickHandler, ISubmitHandler
    {
        [Serializable]
        /// <summary>
        /// Function definition for a button click event.
        /// </summary>
        public class ButtonClickedEvent : UnityEvent {}
        public class ButtonPressEvent : UnityEvent {}

        // Event delegates triggered on click.
        [FormerlySerializedAs("onClick")]
        [SerializeField]
        private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();
        [FormerlySerializedAs("onPress")]
        [SerializeField]
        private ButtonPressEvent m_OnPress = new ButtonPressEvent();

        [SerializeField] private Vector2 textOffsetOnPress;

        protected PixelButton()
        {}

        /// <summary>
        /// UnityEvent that is triggered when the button is pressed.
        /// Note: Triggered on MouseUp after MouseDown on the same object.
        /// </summary>
        ///<example>
        ///<code>
        /// <![CDATA[
        /// using UnityEngine;
        /// using UnityEngine.UI;
        /// using System.Collections;
        ///
        /// public class ClickExample : MonoBehaviour
        /// {
        ///     public Button yourButton;
        ///
        ///     void Start()
        ///     {
        ///         Button btn = yourButton.GetComponent<Button>();
        ///         btn.onClick.AddListener(TaskOnClick);
        ///     }
        ///
        ///     void TaskOnClick()
        ///     {
        ///         Debug.Log("You have clicked the button!");
        ///     }
        /// }
        /// ]]>
        ///</code>
        ///</example>
        public ButtonClickedEvent onClick
        {
            get { return m_OnClick; }
            set { m_OnClick = value; }
        }
        public ButtonPressEvent onPress
        {
            get { return m_OnPress; }
            set { m_OnPress = value; }
        }

        private void Click()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("Button.onClick", this);
            m_OnClick.Invoke();
        }

        private void Press()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("Button.onPress", this);
            m_OnPress.Invoke();
        }

        /// <summary>
        /// Call all registered IPointerClickHandlers.
        /// Register button presses using the IPointerClickHandler. You can also use it to tell what type of click happened (left, right etc.).
        /// Make sure your Scene has an EventSystem.
        /// </summary>
        /// <param name="eventData">Pointer Data associated with the event. Typically by the event system.</param>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// //Attatch this script to a Button GameObject
        /// using UnityEngine;
        /// using UnityEngine.EventSystems;
        ///
        /// public class Example : MonoBehaviour, IPointerClickHandler
        /// {
        ///     //Detect if a click occurs
        ///     public void OnPointerClick(PointerEventData pointerEventData)
        ///     {
        ///             //Use this to tell when the user right-clicks on the Button
        ///         if (pointerEventData.button == PointerEventData.InputButton.Right)
        ///         {
        ///             //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        ///             Debug.Log(name + " Game Object Right Clicked!");
        ///         }
        ///
        ///         //Use this to tell when the user left-clicks on the Button
        ///         if (pointerEventData.button == PointerEventData.InputButton.Left)
        ///         {
        ///             Debug.Log(name + " Game Object Left Clicked!");
        ///         }
        ///     }
        /// }
        /// ]]>
        ///</code>
        /// </example>

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Click();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();

            RectTransform textTransform = GetChildTransform();
            if (textTransform != null)
            {
                textTransform.offsetMin += textOffsetOnPress;
                textTransform.offsetMax += textOffsetOnPress;
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            RectTransform textTransform = GetChildTransform();
            if (textTransform != null)
            {
                textTransform.offsetMin -= textOffsetOnPress;
                textTransform.offsetMax -= textOffsetOnPress;
            }
        }

        /// <summary>
        /// Call all registered ISubmitHandler.
        /// </summary>
        /// <param name="eventData">Associated data with the event. Typically by the event system.</param>
        /// <remarks>
        /// This detects when a Button has been selected via a "submit" key you specify (default is the return key).
        ///
        /// To change the submit key, either:
        ///
        /// 1. Go to Edit->Project Settings->Input.
        ///
        /// 2. Next, expand the Axes section and go to the Submit section if it exists.
        ///
        /// 3. If Submit doesn’t exist, add 1 number to the Size field. This creates a new section at the bottom. Expand the new section and change the Name field to “Submit”.
        ///
        /// 4. Change the Positive Button field to the key you want (e.g. space).
        ///
        ///
        /// Or:
        ///
        /// 1. Go to your EventSystem in your Project
        ///
        /// 2. Go to the Inspector window and change the Submit Button field to one of the sections in the Input Manager (e.g. "Submit"), or create your own by naming it what you like, then following the next few steps.
        ///
        /// 3. Go to Edit->Project Settings->Input to get to the Input Manager.
        ///
        /// 4. Expand the Axes section in the Inspector window. Add 1 to the number in the Size field. This creates a new section at the bottom.
        ///
        /// 5. Expand the new section and name it the same as the name you inserted in the Submit Button field in the EventSystem. Set the Positive Button field to the key you want (e.g. space)
        /// </remarks>

        public virtual void OnSubmit(BaseEventData eventData)
        {
            Click();

            // if we get set disabled during the press
            // don't run the coroutine.
            if (!IsActive() || !IsInteractable())
                return;

            DoStateTransition(SelectionState.Pressed, false);
            StartCoroutine(OnFinishSubmit());
        }

        private IEnumerator OnFinishSubmit()
        {
            var fadeTime = colors.fadeDuration;
            var elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            DoStateTransition(currentSelectionState, false);
        }

        private RectTransform GetChildTransform()
        {
            RectTransform[] transforms = gameObject.GetComponentsInChildren<RectTransform>();
            foreach (RectTransform transform in transforms)
            {
                if (this.transform.name == transform.parent.transform.name)
                {
                    return transform;
                }
            }
            return null;
        }
    }
}

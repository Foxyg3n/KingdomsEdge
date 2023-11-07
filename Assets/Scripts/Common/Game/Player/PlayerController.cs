using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour {

        public Display display;

        [Header("Walking")] 
        [SerializeField] private float walkSpeed = 2;

        // [Header("Jumping")] 
        // [SerializeField] private float jumpSpeed = 3;
        // [SerializeField] private float fallSpeed = 12;
        // //[SerializeField] private int numberOfJumps = 3;
        // [SerializeField] private AnimationCurve jumpFallOff = AnimationCurve.Linear(0, 1, 1, 0);

        public Vector2 desiredDirection { get; private set; }
        public int facingDirection { get; private set; } = 1;
        public PlayerState state { get; private set; } = PlayerState.Movement;

        #region Events

        public void OnMove(InputValue value) {
            desiredDirection = new Vector2(Mathf.Sign(value.Get<Vector2>().x) == 1 ? Mathf.Ceil(value.Get<Vector2>().x) : Mathf.Floor(value.Get<Vector2>().x), 0);
            if(App.GameManager.isGamePaused) return;
            if(desiredDirection.x == 0) {
                GetComponent<Animator>().Play("Idle");
            } else {
                GetComponent<Animator>().Play("Walk");
            }
        }

        // public void OnJump(InputValue value)
        // {
        //     _wantsToJump = value.Get<float>() > 0.5f;

        //     if (_wantsToJump)
        //         RequestJump();
        //     else
        //         jumpStopwatch.Reset();
        // }

        // public void OnAttack(InputValue value)
        // {
        //     EnterAttackState();
        // }

        // private void OnCollisionEnter2D(Collision2D other)
        // {
        //     if (other.gameObject.layer != _enemyLayer) return;
        //     EnterHitState(other);
        // }

        // private void OnCollisionStay2D(Collision2D other)
        // {
        //     if (other.gameObject.layer != _enemyLayer || State == PlayerState.Hit) return;
        //     EnterHitState(other);
        // }

        #endregion

        private void FixedUpdate() {
            if(desiredDirection.x == 0 && GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != "Idle")
                GetComponent<Animator>().Play("Idle");
            if(App.GameManager.isGamePaused) return;
            switch(state)  {
                case PlayerState.Movement:
                    UpdateMovementState();
                    break;
                // case PlayerState.Attack:
                //     UpdateAttackState();
                //     break;
                // case PlayerState.Hit:
                //     UpdateHitState();
                //     break;
            }

            display.transform.position = new Vector3(transform.position.x, display.transform.position.y, display.transform.position.z);
        }

        #region States

        private void EnterMovementState() {
            state = PlayerState.Movement;
        }

        private void UpdateMovementState() {
            if(desiredDirection.x > 0)
                facingDirection = 1;
            else if(desiredDirection.x < 0)
                facingDirection = -1;

            Vector3 velocity = new Vector3((desiredDirection.x * walkSpeed) / 100, 0, 0);
            transform.Translate(velocity);
            transform.localScale = new Vector3(facingDirection, 1, 1);
        }

        #endregion
    }
}
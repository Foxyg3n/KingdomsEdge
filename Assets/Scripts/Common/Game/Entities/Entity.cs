using DG.Tweening;
using UnityEngine;

[@RequireComponent(typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour {

    private Tweener goToAction;

    public Island island { private set; get; }
    public Village village => island.village;
    public readonly GoalSelector goalSelector = new();
    public bool isWalking;
    public abstract float speed { get; }
    public Animator animator { get; private set; }

    public void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        goalSelector.Execute();
    }

    public void InitIsland(Island island) {
        if(this.island == null) this.island = island;
    }

    public void GoTo(MonoBehaviour obj, TweenCallback onFinish = null) {
        GoTo(obj.transform.position.x, onFinish);
    }

    public void GoTo(float x, TweenCallback onFinish = null) {
        if(goToAction != null || goToAction.IsActive()) goToAction.Kill();
        isWalking = true;
        animator.Play("Walk");
        transform.localScale = new Vector3(Mathf.Sign(x - transform.position.x), 1, 1);
        goToAction = transform.DOMoveX(x, speed).SetEase(Ease.Linear).SetSpeedBased(true).OnComplete(() => {
            isWalking = false;
            animator.Play("Idle");
            onFinish?.Invoke();
        });
    }

    public void InterruptGoTo() {
        isWalking = false;
        if(goToAction != null || goToAction.IsActive()) goToAction.Kill();
        goToAction = null;
    }

}

using System.Collections.Generic;
public class GoalSelector {

    private readonly SortedDictionary<int, IGoal> goals = new();
    private IGoal lastGoal;

    public void AddGoal(int priority, IGoal goal) {
        goals.Add(priority, goal);
    }

    public void RemoveGoal<T>() {
        using SortedDictionary<int, IGoal>.Enumerator enumerator = goals.GetEnumerator();
        while(enumerator.MoveNext()) {
            if(typeof(T) == enumerator.Current.Value.GetType()) {
                goals.Remove(enumerator.Current.Key);
                return;
            }
        }
    }

    public void Execute() {
        foreach(IGoal goal in goals.Values) {
            if(goal.MeetsRequirements()) {
                if(lastGoal != goal) {
                    lastGoal?.Reset();
                    lastGoal = goal;
                }
                goal.Execute();
                return;
            }
        }
    }
    
}

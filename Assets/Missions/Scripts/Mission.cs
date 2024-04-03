using UnityEngine;

public abstract class Mission : MonoBehaviour
{
    protected float totalTime;
    public float currentTime;
    protected int moneyToEarn;
    protected int reputationToEarn;
    
    protected abstract void checkFinishConditions();
    protected abstract void OnPassed();
    protected abstract void OnFailed();
    
}

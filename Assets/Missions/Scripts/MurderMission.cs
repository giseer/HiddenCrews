public class MurderMission : Mission
{
    private NPCMovement target;
    
    protected override void checkFinishConditions()
    {
        // Ganas si:
        // La vida del target es 0
        
        // Pierdes si:
        // Pasa el tiempo
        // Si mueres
    }

    protected override void OnPassed()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnFailed()
    {
        throw new System.NotImplementedException();
    }
}

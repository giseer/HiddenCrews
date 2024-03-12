using UnityEngine;

public class RecollectMission : Mission
{
    private Products product;
    private Transform location;
    
    protected override void checkFinishConditions()
    {
        // Ganas si:
        // Recollectas todos los productos
        
        // Pierdes si:
        // Pasa el tiempo
        // Te pilla la policia
        // Te mueres
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
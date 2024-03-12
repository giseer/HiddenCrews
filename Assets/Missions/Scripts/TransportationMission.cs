using UnityEngine;

public class TransportationMission : Mission
{
    protected override void checkFinishConditions()
    {
        // Ganas si:
        // Llegas a tu destino y entregas el paquete
        
        // Pierdes si:
        // Te pilla la policia
        // Te mueres
        // Se acaba el tiempo
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

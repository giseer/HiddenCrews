using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ObjectActivationInfo
{
    public GameObject objectToActivate;
    public float activationTime;
    public float activationDuration;
}

public class Prueba : MonoBehaviour
{
    public List<ObjectActivationInfo> activationInfos = new List<ObjectActivationInfo>();

    void Start()
    {
        StartCoroutine(ActivateObjects());
    }

    IEnumerator ActivateObjects()
    {
        foreach (var info in activationInfos)
        {
            yield return new WaitForSeconds(info.activationTime);

            info.objectToActivate.SetActive(true);

            yield return new WaitForSeconds(info.activationDuration);

            info.objectToActivate.SetActive(false);
        }
    }
}

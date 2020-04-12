using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaSmokeEffect : MonoBehaviour
{
    private GameObject parObj;

    private void Start()
    {
        parObj = gameObject.transform.parent.gameObject;
    }

    public void OnSmokeVanish()
    {
        Destroy(parObj);
    }

}

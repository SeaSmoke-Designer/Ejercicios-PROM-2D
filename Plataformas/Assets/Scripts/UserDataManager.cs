using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    private float vidas;


    public float GetVidas() => vidas;

    public void SetVidas(float value) => vidas = value;
}

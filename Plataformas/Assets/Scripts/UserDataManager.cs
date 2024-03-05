using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    //private float vidas;

    //public float vidas
    //{
    //    get => vidas;
    //    set => vidas = value;
    //}

    //public float vidas { get; set;}

    private float vidas;

    public float Vidas
    {
        get { return vidas; }
        set { vidas = value; }
    }

    private bool isDead;

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }


}

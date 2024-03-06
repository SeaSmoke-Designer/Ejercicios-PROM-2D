using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    public float vidas { get; set; }
    public bool isDead { get; set; }
    public void ResetValues()
    {
        vidas = 3f;
        isDead = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    public int level { get; set; }
    public int vidas { get; set; }
    public int columnas { get; set; }
    public int filas { get; set; }
    public bool levelProcedural { get; set; }

    private void Start()
    {
        level = 1;
        vidas = 3;
    }

    public void CambiarColumnasFilas(int c, int f)
    {
        columnas = c;
        filas = f;
    }

    public void ResetValues()
    {
        vidas = 3;
        level = 1;
        levelProcedural = false;
    }
}

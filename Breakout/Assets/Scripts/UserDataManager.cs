using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public int level { get; set; }
    public int vidas { get; set; }
    public int columnas { get; set; }
    public int filas { get; set; }

    private void Start()
    {
        level = 1;
    }

    public void CambiarColumnasFilas(int c, int f)
    {
        columnas = c;
        filas = f;
    }
}

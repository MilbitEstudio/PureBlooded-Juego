using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataService
{
    bool SaveData<T>(string relativePath, T data, bool encrypted);

    T LoadData<T>(string relativePath, T encrypted);
}

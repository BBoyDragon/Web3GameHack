using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController
{
    private BonusData _data;
    private List<BonusView> _views;
    private List<Vector3> _positions;

    public event Action OnBunusApyed;
    public BonusController(BonusData data)
    {
        _data = data;
        _views = new List<BonusView>();
        _positions = new List<Vector3>();
        for(int i = 0; i < _data.Amount; i++)
        {
            bool spawn = false;
            while (spawn != true)
            {
                Vector3 tmp = new Vector3(UnityEngine.Random.Range(0, 20) * 3 - 0.5f, 1, UnityEngine.Random.Range(0, 20) * 3 + 2.5f);
                bool isUnic = true;
                foreach (Vector3 vec in _positions)
                {
                    if (tmp == vec)
                    {
                        isUnic = false;
                        break;
                    }
                }
                

                if (isUnic)
                {
                    _positions.Add(tmp);
                    spawn = true;
                }

            }
        }

        foreach(Vector3 vec in _positions)
        {
            _views.Add(GameObject.Instantiate<BonusView>(_data.View, vec, Quaternion.identity));
            _views[_views.Count - 1].OnBonusUsed += AplyBous;
        }

    }

    void AplyBous(BonusView curent)
    {
        _views.Remove(curent);
        GameObject.Destroy(curent.gameObject);
        OnBunusApyed?.Invoke();
    }
}

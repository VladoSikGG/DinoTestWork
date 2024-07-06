using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T:MonoBehaviour
{
    public bool autoExpand;
    private T _prefab;
    private Transform _container;
    private List<T> _pool;

    public ObjectPool(T prefab, Transform container, int count)
    {
        _prefab = prefab;
        _container = container;
        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createObject = Object.Instantiate(_prefab, _container);
        createObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createObject);
        return createObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var item in _pool)
        {
            if (item.gameObject.activeInHierarchy == false)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;

        if (autoExpand)
            return CreateObject(true);

        throw new Exception("No free elements in pool");
    }
}

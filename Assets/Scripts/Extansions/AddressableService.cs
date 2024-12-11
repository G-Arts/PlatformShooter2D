using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressableService
{
    public static void InstantiateObject<T>(string key, Transform parent, Action<T> callbackComponent) where T : Component
    {
        Addressables.InstantiateAsync(key, parent,true).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject instantiatedObject = handle.Result;

                T component = instantiatedObject.GetComponent<T>();

                if (component != null)
                {
                    callbackComponent?.Invoke(component);
                }
                else
                {
                    Debug.LogError($"The instantiated object does not have a component of type {typeof(T)}.");
                    Addressables.ReleaseInstance(instantiatedObject);
                }
            }
            else
            {
                Debug.LogError($"Failed to load Addressable object with key: {key}");
            }
        };
    }
}

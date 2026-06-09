using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableMar
{
    private static AddressableMar instance;
    public static AddressableMar Instance
    {
        get {
            if(instance == null)
                instance = new AddressableMar();
            return instance; }
        
    }

    //存储容器  用字典
    public Dictionary<string, IEnumerator> resDic = new Dictionary<string, IEnumerator>();

    private AddressableMar() { }

    //异步加载资源 外部传入函数
    public void LoadAssetAsync<T>(string name , Action<AsyncOperationHandle<T>> callBack)
    {
        //反射
        //通过名字与类型拼接 key
        string keyName = name + "_" + typeof(T).Name;
        AsyncOperationHandle<T> handle;
        //如果已经加载过改资源
        if (resDic.ContainsKey(keyName))
        {
           handle =  (AsyncOperationHandle<T>)resDic[keyName];

            //判断异步加载是否结束
            if (handle.IsDone)
            {
                    callBack(handle);
            }
            else//还没有加载完成
            {
                //没有异步加载就告诉他完成时调用什么
                handle.Completed += callBack;
            }
            return;
        }

        //没有加载过该资源
        handle = Addressables.LoadAssetAsync<T>(keyName);
        handle.Completed += (obj) =>
        {
            if(obj.Status == AsyncOperationStatus.Succeeded)
            {
                callBack(obj);
            }
            else
            {
                Debug.LogWarning(keyName + "资源加载失败");
                if(resDic.ContainsKey(keyName))
                    resDic.Remove(keyName);
            }
        } ;
        resDic.Add(keyName, handle);

        
    }
    
    public void LoadAssetsAsync<T>(Addressables.MergeMode mode , Action<T> callback, params string[] keys)
    {
        List<string> list = new List<string>(keys);

        string keyName = "";

        foreach(var key in list)
        {
            keyName += key + "_";
            //类名
            
        }
        keyName += typeof(T).Name;
        AsyncOperationHandle<IList<T>> handle;
        if (resDic.ContainsKey(keyName))
        {
            handle = (AsyncOperationHandle<IList<T>>)resDic[keyName];
            if (handle.IsDone)
            {
                foreach (T item in handle.Result)
                    callback(item);
            }
            else
            {
                
                handle.Completed += (obj) =>
                {
                    //加载成功才调用外部传入的委托
                    if(obj.Status == AsyncOperationStatus.Succeeded)
                    {
                        foreach (T item in handle.Result)
                            callback(item);
                    }
                };
            }
            return;
        }
        //不存在做什么
        handle =  Addressables.LoadAssetsAsync<T>(list, callback , mode);
        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError("资源加载失败" + keyName);
                if (resDic.ContainsKey(keyName))
                {
                    resDic.Remove(keyName);
                }
            }
        };
        resDic.Remove(keyName);

    }



    //释放资源
    public void Release<T> (string name)
    {
        string keyName = name + "_" + typeof(T).Name;
        if (resDic.ContainsKey(keyName))
        {
            AsyncOperationHandle<T> handle = (AsyncOperationHandle<T>)resDic[keyName];
            Addressables.Release(handle);
            resDic.Remove(keyName);
        }
    }

    public void Release<T> (params string[] key)
    {

    }

    //清空资源

}

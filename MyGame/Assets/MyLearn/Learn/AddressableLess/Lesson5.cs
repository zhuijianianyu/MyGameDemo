using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
public class Lesson5 : MonoBehaviour
{
    //声明任意类型的资源
    public AssetReference assetReference;

    public AssetReference sceneReference;
    #region  加载资源
    AsyncOperationHandle<GameObject> handle;
    

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        handle = assetReference.LoadAssetAsync<GameObject>();
        handle.Completed += Handle_completed;

        sceneReference.LoadSceneAsync();
    }

    private void Handle_completed(AsyncOperationHandle<GameObject> obj)
    {
        //加载成功
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(handle.Result);
            //释放资源
            assetReference.ReleaseAsset();
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

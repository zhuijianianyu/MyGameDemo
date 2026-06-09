using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.PlayerLoop;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Lesson6 : MonoBehaviour 
{
    //private void Start()
    //{
    //    AddressableMar.Instance.LoadAssetAsync<GameObject>("Cube", (obj) =>
    //    {
    //        Instantiate(obj.Result);
    //    });
    //}

    private void Start()
    {
        Addressables.LoadAssetsAsync<GameObject>("Cube", (obj) =>
        {
            print(obj.name);
        });
    }




}

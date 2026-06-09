using NUnit;
using UnityEngine;
using UnityEngine.Windows;
using XLua;
public class Lesson2_Loader : MonoBehaviour
{
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    LuaEnv luaEnv = new LuaEnv();

    //    //xlua提供的 路径重定向的方法
    //    //允许我们自定义 加载lua 文件的规则
    //    //当执行lua语言 require 相当于执行lua 这个时候执行我们自定义传入的这个函数
    //    luaEnv.AddLoader(MyCustomLoader);

    //    luaEnv.DoString("require('Main')");
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    //private byte[] MyCustomLoader(ref string filePath)
    //{
    //    //传入的参数是 require执行的lua脚本文件名
    //    string path = Application.dataPath + "/Myrec/Learn/Lua/" + filePath + ".lua";
    //    Debug.Log(path);

    //    //有路径 去加载文件
    //    //file 知识点C#提供的文件读写类
    //    //是否存在
    //    if( File.Exists(path) )
    //    {
    //        return File.ReadAllBytes(path);
    //    }
    //    else
    //    {
    //        Debug.Log("lua重定向失败:" + filePath);
    //    }

    //    //通过函数中逻辑去加载lua
    //    return null;
    //}
}

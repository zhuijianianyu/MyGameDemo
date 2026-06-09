using UnityEngine;
using XLua;
using UnityEngine.Windows;

/// <summary>
///  Lua 管理器
///  提供 Lua解析器
///  保证解析器的唯一性
///  
/// lua脚本会放在AB包
/// 最终通过AB包加载其中的lua脚本资源 来执行
/// ab包中如果要加载文本 后缀还有一定限制 .lua
/// 打包时，还是要将lua打包为txt
/// </summary>



public class LuaMar : BaseManager<LuaMar>
{
    ////执行lua的函数
    ////释放垃圾
    ////销毁
    ////重定向
    //private LuaEnv luaEnv;

    ///// <summary>
    ///// 初始化
    ///// </summary>
    //public void Init()
    //{
    //    if(luaEnv != null)
    //    {
    //        Debug.Log("已经初始化");
    //        return;
    //    }
    //    //初始化
    //    luaEnv = new LuaEnv();
    //    //加载Lua脚本 重定向
    //    luaEnv.AddLoader(MyCustomLoader);


    //}

    ///// <summary>
    ///// 执行Lua
    ///// </summary>
    //public void DoString(string str)
    //{
    //    if(luaEnv == null)
    //    {
    //        Debug.Log("LuaEnv为空");
    //        return;
    //    }
    //    luaEnv.DoString(str);
    //}

    ///// <summary>
    ///// 手动释放的对象   垃圾回收
    ///// </summary>
    //public void Tick()
    //{
    //    luaEnv.Tick();
    //}

    ///// <summary>
    ///// 销毁Lua解析器
    ///// </summary>
    //public void Dispose()
    //{
    //    luaEnv.Dispose();
    //    luaEnv = null;
    //}

    ///// <summary>
    ///// 自动执行
    ///// </summary>
    //private byte[] MyCustomLoader(ref string filePath)
    //{
    //    //传入的参数是 require执行的lua脚本文件名
    //    string path = Application.dataPath + "/Myrec/Learn/Lua/" + filePath + ".lua";


    //    //有路径 去加载文件
    //    //file 知识点C#提供的文件读写类
    //    //是否存在
    //    if (File.Exists(path))
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

using UnityEngine;
using XLua;

public class Lesson1_LuaEnv : MonoBehaviour
{
    private void Start()
    {
        //lua解析器 能够在unity执行lua
        //一般情况下保持唯一性
        LuaEnv env = new LuaEnv();
        //第二个参数返回报错信息 第三个参数返回解析器
        env.DoString("print('你好世界')" , "Lesson1_LuaEnv");

        //直接执行lua脚本 :多脚本执行 require
        //在Resources下加载 ， 只能识别txt bytes等文件，所以lua文件后添加txt文件
        env.DoString("require('Main')");

        //帮助告诉lua李没有手动释放的对象   垃圾回收
        env.Tick();

        //销毁Lua解析器
        env.Dispose();
    }
}

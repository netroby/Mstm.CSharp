using MoonSharp.Interpreter;
using Mstm.DynamicScript.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.DynamicScript.Lua
{
    /// <summary>
    /// Lua动态脚本运行引擎
    /// </summary>
    public class LuaDynamicScriptRuntime : IDynamicScriptRuntimeProvider
    {
        public List<string> ScriptTypeList => new List<string>() { "lua", ".lua" };

        public string ExecuteScript(string script)
        {
            DynValue value = Script.RunString(script);
            return value.ToString();
        }

        public string ExecuteFile(string scriptFile)
        {
            DynValue value = Script.RunFile(scriptFile);
            return value.ToString();
        }
    }
}

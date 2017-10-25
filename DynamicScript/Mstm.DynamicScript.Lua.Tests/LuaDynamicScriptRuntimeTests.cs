using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Mstm.DynamicScript.Core;

namespace Mstm.DynamicScript.Lua.Tests
{
    public class LuaDynamicScriptRuntimeTests
    {

        [Fact]
        public void LuaExecuteFileTest()
        {
            var result = DynamicScriptRuntime.Execute("lua/demo.lua");
            result.ShouldBe("120");
        }

        [Fact]
        public void LuaExecuteScriptTest()
        {
            var result = DynamicScriptRuntime.Execute(@"		
                            function fact (n)
			                    if (n == 0) then
				                    return 1
			                    else
				                    return n*fact(n - 1)
			                    end
		                    end
		                    return fact(5)
            ", "lua");
            result.ShouldBe("120");
        }

        [Fact]
        public void LuaExecuteScriptWithDotTest()
        {
            var result = DynamicScriptRuntime.Execute(@"		
                            function fact (n)
			                    if (n == 0) then
				                    return 1
			                    else
				                    return n*fact(n - 1)
			                    end
		                    end
		                    return fact(5)
            ", ".lua");
            result.ShouldBe("120");
        }
    }
}

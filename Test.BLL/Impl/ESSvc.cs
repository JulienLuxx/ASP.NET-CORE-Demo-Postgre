using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Core;

namespace Test.Service
{
    public class ESSvc : IESSvc
    {
        public ESSvc(IOptions<ESConnectionStrings> connStr)
        {
        }

        public void Test()
        { }
    }
}

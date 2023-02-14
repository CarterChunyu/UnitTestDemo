using Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTests
{
    // 同一個測試類
    public class LongTimeTaskFixture : IDisposable
    {
        public  LongTimeTask Task { get; private set; }
        public LongTimeTaskFixture()
        {
            Task = new LongTimeTask();
        }
        public void Dispose()
        {
            
        }
    }
}

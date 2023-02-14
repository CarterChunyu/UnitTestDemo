using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Demo;
using Xunit.Abstractions;

namespace DemoTests
{
    [Collection("Long Time Task Collecttion")]
    public class PatientShold: IClassFixture<LongTimeTaskFixture>,IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly Patient _patient;
        private readonly LongTimeTask _task;
        public PatientShold(ITestOutputHelper output,LongTimeTaskFixture fixture)
        {
            _output = output;
            // Arrange 
            _patient = new Patient();
            // _task = new LongTimeTask();
            _task = fixture.Task;
        }
        [Fact]
        [Trait("Category","New")]
        public void BeNewWhenCreated()
        {
            _output.WriteLine("第一個測試");
            // Arrange 
            //var patient = new Patient();

            // Act
            var result = _patient.IsNew;

            // Assert
            Assert.True(result);
        }
        [Fact]
        [Trait("Category","Name")]
        public void HaveCorrectFullName()
        {
            var patient = new Patient()
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            var fullName = patient.FullName;

            Assert.Equal(expected: "Nick Carter", actual: fullName);
            Assert.StartsWith(expectedStartString: "Nick", actualString: fullName);
            Assert.EndsWith(expectedEndString: "Carter", actualString: fullName);
            Assert.Contains(expectedSubstring: "Car", actualString: fullName);
            Assert.NotEqual(expected: "NICK CARTER", actual: fullName);
            Assert.Matches(expectedRegexPattern: @"^[A-Z][a-z]*\s[A-Z][a-z]*",actualString:fullName);
        }
         
        [Fact]
        [Trait("Category","New")]
        public void HaveDefaultBloodSugarWhenCreated()
        {         
            var bloodSugar = _patient.BloodSurgar;

            Assert.Equal(expected: 4.9f, actual: bloodSugar, precision: 5); // precision 指的是精度
            Assert.InRange(actual: bloodSugar, low: 3.9f, high: 6.1f);                                                                 
        }
        [Fact]
        [Trait("Category","Name")]
        [Trait("Category","New")]
        public void HaveNoNameWhenCreated()
        {
            Assert.Null(_patient.FirstName);
            Assert.NotNull(_patient)  ;
        }
        [Fact(Skip ="不需要跑這個測試")]
        public void HaveHadAColdBefore()
        {
            _patient.History.Add("感冒");
            _patient.History.Add("發燒");
            _patient.History.Add("水痘");
            _patient.History.Add("腹瀉");
            var diseases = new List<string>
            {
                "感冒",
                "發燒",
                "水痘",
                "腹瀉"
            };
            Assert.Contains(expected: "感冒", _patient.History);
            Assert.DoesNotContain(expected: "心臟病", _patient.History);

            // Predicate
            Assert.Contains(_patient.History, filter: x => x.StartsWith("水"));
            // All的參數是 action需要另外處理
            Assert.All(_patient.History, x => Assert.True(x.Length>=2));

            Assert.Equal(expected: diseases, actual: _patient.History);
        }
        [Fact]
        public void BeAPerson()
        {
            var p1 = new Patient();
            var p2 = new Patient();

            Assert.IsType<Patient>(p1); // Vertifies that an object is exactly the given type(and not a derived type)
            Assert.IsNotType<Person>(p1); 

            Assert.IsAssignableFrom<Person>(p1); // 繼承自
            Assert.NotSame(expected: p1, actual: p2); // 是否不是同一個實例
        }
        [Fact]
        public void ThrowExceotionsWhenExrrorOccurred()
        {
            var ex = Assert.Throws<InvalidOperationException>(testCode: () => _patient.NotAllowed()) ;  // 拋出異常跟指定異常必須是同一個類
            Assert.Equal(expected: "Not able to create", actual: ex.Message);
        }
        [Fact]
        public void RaiseSleptEvent() 
        {
            // 確認是否有觸發特定參數的事件
            Assert.Raises<EventArgs>(
                attach: handler => _patient.PatientSlept += handler,   // 附加
                detach: handler => _patient.PatientSlept -= handler,   // 分離 
                testCode: () => _patient.Sleep());
        }
        [Fact]
        public void RaisePropertyChangedEvent()
        {
            //var p = new Patient();
            //Assert.PropertyChanged(p, nameof(p.HeartBeatRun), () => p.IncreaseHeartBeatRate());
            Assert.PropertyChanged(_patient, nameof(_patient.HeartBeatRun)
                , () => _patient.IncreaseHeartBeatRate());
        }

        public void Dispose()
        {
            _output.WriteLine("清理了資源");
        }
    }
}

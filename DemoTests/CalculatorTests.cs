using Demo;
using DemoTests.Data;
using System;
using Xunit;

namespace DemoTests
{
    [Collection("Long Time Task Collection")]
    [Trait("Category","Calc")]
    public class CalculatorTests
    {
        private readonly LongTimeTask _task;
        public CalculatorTests(LongTimeTaskFixture fixture)
        {
            //_task = new LongTimeTask();
            _task = fixture.Task;            
        }
        [Fact]
        public void SholdAddEqual3() 
        {
            // Arrange
            var sut = new Calculator(); // sut - System Under Test

            // Act
            var result = sut.Add(x: 1, y: 2);

            // Assert
            Assert.Equal(expected: 3, actual: result);
        }
        [Theory]
        //[InlineData(1,2,3)]
        //[InlineData(2,3,5)]
        //[InlineData(1,3,4)]
        //[InlineData(0,2,2)]
        //[MemberData(nameof(CalculatorTestData.TestData),MemberType = typeof(CalculatorTestData))]
        //[MemberData(nameof(CalculatorCsvData.TestData),MemberType =typeof(CalculatorCsvData))]
        [CalculatorData]
        public void SholdAdd(int x,int y,int expected)
        {
            var sut = new Calculator();
            var result= sut.Add(x: x, y: y);
            Assert.Equal(expected: expected, actual: result);
        }
         
    }
}

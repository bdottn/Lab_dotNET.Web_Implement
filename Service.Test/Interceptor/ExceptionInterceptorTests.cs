using Castle.DynamicProxy;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Service.Interceptor;
using Service.Model;
using System;

namespace Service.Test.Interceptor
{
    [TestClass]
    public sealed class ExceptionInterceptorTests
    {
        private IInvocation invocation;

        private ExceptionInterceptor interceptor;

        [TestInitialize]
        public void TestInitialize()
        {
            this.invocation = Substitute.For<IInvocation>();

            this.interceptor = new ExceptionInterceptor();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_ExceptionInterceptor_預期得到回傳值且呼叫過一次Proceed方法()
        {
            var expected = new ServiceResult()
            {
                ResultType = ServiceResultType.Success,
                Message = "interceptor"
            };

            this.invocation.TargetType.Returns(typeof(IEmpty));
            this.invocation.Method.Returns(typeof(IEmpty).GetMethod(nameof(IEmpty.Test)));
            this.invocation.ReturnValue.Returns(expected);

            this.interceptor.Intercept(this.invocation);

            var actual = this.invocation.ReturnValue;

            expected.ToExpectedObject().ShouldEqual(actual);

            this.invocation.Received(1).Proceed();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_ExceptionInterceptor_呼叫Proceed方法發生例外_預期得到回傳值且呼叫過一次Proceed方法()
        {
            var expected = new ServiceResult()
            {
                ResultType = ServiceResultType.Exception,
                Message = "exception"
            };

            this.invocation.TargetType.Returns(typeof(IEmpty));
            this.invocation.Method.Returns(typeof(IEmpty).GetMethod(nameof(IEmpty.Test)));
            this.invocation.When(i => i.Proceed()).Throw(new Exception(expected.Message));

            this.interceptor.Intercept(this.invocation);

            var actual = this.invocation.ReturnValue;

            expected.ToExpectedObject().ShouldEqual(actual);

            this.invocation.Received(1).Proceed();
        }

        private interface IEmpty
        {
            ServiceResult Test();
        }
    }
}
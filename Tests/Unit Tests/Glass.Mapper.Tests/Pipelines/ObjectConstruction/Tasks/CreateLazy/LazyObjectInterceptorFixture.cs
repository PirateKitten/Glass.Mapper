/*
   Copyright 2012 Michael Edwards
 
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 
*/ 
//-CRE-


using Castle.DynamicProxy;
using Glass.Mapper.Pipelines.ObjectConstruction.Tasks.CreateLazy;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Pipelines.ObjectConstruction.Tasks.CreateLazy
{
    [TestFixture]
    public class LazyObjectInterceptorFixture
    {
        #region Method - Intercept

        [Test]
        public void Intercept_CreatesObjectLazily_CallsInvokeMethod()
        {
            //Assign
            var typeContext = Substitute.For<AbstractTypeCreationContext>();

            var invocation = Substitute.For<IInvocation>();
            invocation.Method.Returns(typeof (StubClass).GetMethod("CalledMe"));

            var interceptor = new LazyObjectInterceptor(null, typeContext);
            interceptor.CreateConcrete = (objectFactory, creationContext) => new StubClass();

            //Act
            interceptor.Intercept(invocation);

            //Assert
            Assert.IsTrue((bool)invocation.ReturnValue);
            Assert.IsFalse(typeContext.IsLazy);
        }

        #endregion

        #region Stubs

        public class StubClass
        {

            public bool CalledMe()
            {
                return true;
            }

        }

        #endregion
    }
}




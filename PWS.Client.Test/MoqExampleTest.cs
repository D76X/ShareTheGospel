using FluentAssertions;
using Moq;

namespace PWS.Client.Test
{
    public interface IFoo
    {
        Bar Bar { get; set; }
        string Name { get; set; }
        int Value { get; set; }
        bool DoSomething(string value);
        bool DoSomething(int number, string value);
        Task<bool> DoSomethingAsync();
        Task<object> DoSomethingAsync2(string value);
        string DoSomethingStringy(string value);
        bool TryParse(string value, out string outputValue);
        bool Submit(ref Bar bar);
        int GetCount();
        bool Add(int value);
    }

    public class Bar
    {
        public virtual Baz Baz { get; set; }
        public virtual bool Submit() { return false; }
    }

    public class Baz
    {
        public virtual string Name { get; set; }
    }

    /// <summary>
    /// https://github.com/devlooped/moq/wiki/Quickstart
    /// </summary>
    public class MoqExampleTest
    {
        /// <summary>
        /// https://github.com/devlooped/moq/wiki/Quickstart#matching-arguments
        /// </summary>
        [Fact]
        public async void Moq_Test06_MatchingArguments()
        {
        }

        /// <summary>
            /// https://github.com/devlooped/moq/wiki/Quickstart#async-methods
            /// </summary>
            [Fact]
        public async void Moq_Test05_Async()
        {
            // arrange
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomethingAsync().Result).Returns(true);
            
            var anonymous1 = new { Name = "anonymous1", Value=101 };
            var anonymous2 = new { Name = "anonymous2", Value = 102 };
            mock.Setup(foo => foo.DoSomethingAsync2(It.IsAny<string>()).Result).Returns(anonymous1);
            mock.Setup(foo => foo.DoSomethingAsync2(nameof(anonymous2)).Result).Returns(anonymous2);

            // act 
            var result0 = await mock.Object.DoSomethingAsync();
            var result1 = await mock.Object.DoSomethingAsync2("some");
            var result2 = await mock.Object.DoSomethingAsync2(nameof(anonymous2));

            // assert
            result0.Should().BeTrue();
            result1.Should().Be(anonymous1);
            result2.Should().Be(anonymous2);
        }

        [Fact]
        public void Moq_Test05_ThrowOnSpecificArgs()
        {
            // arrange
            var mock = new Mock<IFoo>();
            string throwArg1 = "reset";
            string throwArg2 = string.Empty;
            string argExceptionMsg = "cannot use empty string as an argument";

            // for any args return true
            mock.Setup(x => x.DoSomething(It.IsAny<string>())).Returns(true);

            // but for specific args override by throwing a specific exception
            mock.Setup(foo => foo.DoSomething(throwArg1)).Throws<InvalidOperationException>();
            mock.Setup(foo => foo.DoSomething(throwArg2)).Throws(new ArgumentException(argExceptionMsg));
            //mock.Setup(x => x.DoSomething(It.IsAny<string>())).Returns(true);

            // https://fluentassertions.com/exceptions/
            Func<bool> func1 = () => mock.Object.DoSomething(throwArg1);
            Func<bool> func2 = () => mock.Object.DoSomething(throwArg2);

            // act
            var result3 = mock.Object.DoSomething("value1");

            // assert
            result3.Should().BeTrue();

            // act + assert
            // https://fluentassertions.com/exceptions/

            // style-1
            mock.Object.Invoking(m => m.DoSomething(throwArg1))
                .Should()
                .Throw<InvalidOperationException>();

            // style-2
            func1.Should().Throw<InvalidOperationException>();

            // style-1
            mock.Object.Invoking(m => m.DoSomething(throwArg2))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage(argExceptionMsg);

            // style-2
            func2.Should().Throw<ArgumentException>().WithMessage(argExceptionMsg);
        }

        [Fact]
        public void Moq_Test04_AccessInvocationArgs()
        {
            // arrange
            var mock = new Mock<IFoo>();

            // Multiple parameters overloads available
            // access invocation arguments when returning a value
            mock.Setup(x => x.DoSomethingStringy(It.IsAny<string>()))
                .Returns((string s) => s.ToLower());

            var upperCaseValue = "ANYSTRING";

            // act
            var result = mock.Object.DoSomethingStringy(upperCaseValue);

            // assert
            result.Should().NotBe(upperCaseValue);
            // Fluent Assertions 
            // https://fluentassertions.com/strings/
            result.Should().BeEquivalentTo(upperCaseValue);
            result.Should().BeLowerCased();
            result.Should().NotBeUpperCased();
            upperCaseValue.Should().BeUpperCased();
            upperCaseValue.Should().NotBeLowerCased();
            result.Should().Be(upperCaseValue.ToLower());
        }

        [Fact]
        public void Moq_Test03_Submit_With_RefArg()
        {
            // arrange
            var mock = new Mock<IFoo>();
            var instance = new Bar();
            // Only matches if the ref argument to the invocation is the same instance
            mock.Setup(foo => foo.Submit(ref instance)).Returns(true);

            var otherInstance = new Bar();

            // act
            var test1 = mock.Object.Submit(ref otherInstance);
            var test2 = mock.Object.Submit(ref instance);

            // assert
            test1.Should().BeFalse();
            test2.Should().BeTrue();
        }

        [Fact]
        public void Moq_Test02_TryParse_With_OutputArg()
        {
            // arrange
            // out arguments
            var outString = "ack";
            var mock = new Mock<IFoo>();
            // TryParse will return true, and the out argument will return "ack", lazy evaluated
            mock.Setup(foo => foo.TryParse("ping", out outString)).Returns(true);
            string outString1 = null;

            // act
            var test1 = mock.Object.TryParse("something", out outString1);

            // assert
            test1.Should().BeFalse();
            outString1.Should().BeNull();

            // arrange
            string outString2 = null;

            // act
            var test2 = mock.Object.TryParse("ping", out outString2);

            // assert
            test2.Should().BeTrue();
            outString2.Should().Be(outString);
        }

        [Fact]
        public void Moq_Test01_DoSomething()
        {
            // arrange
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);

            // act
            var test1 = mock.Object.DoSomething("something");
            var test2 = mock.Object.DoSomething("ping");

            // assert
            test1.Should().BeFalse();
            test2.Should().BeTrue();
        }
    }
}

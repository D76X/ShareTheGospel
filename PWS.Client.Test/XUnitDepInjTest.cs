
namespace PWS.Client.Test
{
    public interface IDependency
    {
        int Value { get; }
    }

    internal class DependencyClass : IDependency
    {
        public int Value => 1;
    }

    public class XUnitDepInjTest
    {
        private readonly IDependency _d;

        /// <summary>
        /// https://www.nuget.org/packages/Xunit.DependencyInjection/
        /// </summary>
        /// <param name="d"></param>
        public XUnitDepInjTest(IDependency d) => _d = d;

        [Fact]
        public void AssertThatWeDoStuff()
        {
            Assert.Equal(1, _d.Value);
        }
    }
}

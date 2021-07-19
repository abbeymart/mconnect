using Xunit;

namespace MConnect.Response.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            const NetStatus.Status resCode = NetStatus.Status.Ok;
            var resMessage = NetStatus.StatusText(resCode);
        }
    }
}
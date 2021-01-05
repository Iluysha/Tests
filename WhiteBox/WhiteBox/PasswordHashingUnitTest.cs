using System;
using Xunit;
using IIG.PasswordHashingUtils;

namespace WhiteBox
{
    public class PasswordHashingUnitTest
    {
        [Fact]
        public void notNullTest()
        {
            string str = "password";
            Assert.NotNull(str.GetHashCode());

        }

        [Fact]
        public void nullTest()
        {
            string str = "password";
            Assert.Throws<ArgumentNullException>(() => PasswordHasher.GetHash(null));
        }
    }
}

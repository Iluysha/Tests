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
            Assert.NotNull(PasswordHasher.GetHash(""));
            Assert.NotNull(PasswordHasher.GetHash(" "));
            Assert.NotNull(PasswordHasher.GetHash("password"));
            Assert.NotNull(PasswordHasher.GetHash("password", null));
            Assert.NotNull(PasswordHasher.GetHash("password", ""));
            Assert.NotNull(PasswordHasher.GetHash("password", " "));
            Assert.NotNull(PasswordHasher.GetHash("password", "salt"));
            Assert.NotNull(PasswordHasher.GetHash("password", "put your soul(or salt) here"));
            Assert.NotNull(PasswordHasher.GetHash("password", "salt", null));
            Assert.NotNull(PasswordHasher.GetHash("password", "salt", 0));
            Assert.NotNull(PasswordHasher.GetHash("password", "salt", 1));
            Assert.NotNull(PasswordHasher.GetHash("password", "salt", 12));
            Assert.NotNull(PasswordHasher.GetHash("password", "salt", 65521));
        }

        [Fact]
        public void nullTest()
        {
            Assert.Throws<ArgumentNullException>(() => PasswordHasher.GetHash(null));
        }

        //Test first if block in Init function 
        [Fact]
        public void initSaltTest()
        {
            Assert.Equal(PasswordHasher.GetHash("password", "put your soul(or salt) here"),
                PasswordHasher.GetHash("password", null));

            Assert.Equal(PasswordHasher.GetHash("password", "put your soul(or salt) here"),
                PasswordHasher.GetHash("password", ""));
        }

        //Test second if block in Init function 
        [Fact]
        public void initAdlerTest()
        {
            Assert.Equal(PasswordHasher.GetHash("password", null, 65521),
                PasswordHasher.GetHash("password", null, null));

            Assert.Equal(PasswordHasher.GetHash("password", null, 65521),
                PasswordHasher.GetHash("password", null, 0));
        }

        [Fact]
        public void equalTest()
        {
            Assert.Equal(PasswordHasher.GetHash("password"),
                PasswordHasher.GetHash("password"));
        }

        [Fact]
        public void notEqualTest()
        {
            Assert.NotEqual(PasswordHasher.GetHash("password1"),
                PasswordHasher.GetHash("password2"));
            Assert.NotEqual(PasswordHasher.GetHash("password"),
                PasswordHasher.GetHash("password "));
            Assert.NotEqual(PasswordHasher.GetHash("password"),
                PasswordHasher.GetHash(" password"));
            Assert.NotEqual(PasswordHasher.GetHash("password"),
                PasswordHasher.GetHash("p assword"));
            Assert.NotEqual(PasswordHasher.GetHash("password"),
                PasswordHasher.GetHash("pàssword")); //latin and cyrillic "a"
        }
    }
}

using Xunit;
using IIG.PasswordHashingUtils;

namespace Integration
{

    public class DataBaseIntegrationTest
    {
        private const string Server = @"DESKTOP-38VTOGO\MSSQLSERVER01";
        private const string Database = @"IIG.CoSWE.AuthDB";
        private const bool IsTrusted = true;
        private const string Login = @"coswe";
        private const string Password = @"L}EjpfCgru9X@GLj";
        private const int ConnectionTimeout = 30;

        AuthDatabaseUtils database = new AuthDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);
        
        [Fact]
        public void writeTest()
        {
            Assert.False(database.AddCredentials(null, PasswordHasher.GetHash("password")));
            Assert.False(database.AddCredentials("", PasswordHasher.GetHash("password")));
            Assert.False(database.AddCredentials(" ", PasswordHasher.GetHash("password")));
            Assert.False(database.AddCredentials("login", null));
            Assert.False(database.AddCredentials("login", ""));
            Assert.False(database.AddCredentials("login", " "));
            Assert.False(database.AddCredentials("login", "password"));
            Assert.True(database.AddCredentials("логин", PasswordHasher.GetHash("password")));
            Assert.True(database.AddCredentials("#@)₴?$0", PasswordHasher.GetHash("password")));
            Assert.True(database.AddCredentials("login", PasswordHasher.GetHash("password")));
        }

        [Fact]
        public void writEqualTest()
        {
            Assert.True(database.AddCredentials("login1", PasswordHasher.GetHash("password1")));
            Assert.False(database.AddCredentials("login1", PasswordHasher.GetHash("password2")));
            Assert.True(database.AddCredentials("login2", PasswordHasher.GetHash("password1")));
        }

        [Fact]
        public void updateWrongTest()
        {
            Assert.False(database.UpdateCredentials("login17", PasswordHasher.GetHash("password"),
                                                    "login", PasswordHasher.GetHash("password")));
        }

        [Fact]
        public void updateEqualTest()
        {
            Assert.True(database.AddCredentials("login3", PasswordHasher.GetHash("password3")));
            Assert.True(database.AddCredentials("login4", PasswordHasher.GetHash("password4")));

            Assert.False(database.UpdateCredentials("login4", PasswordHasher.GetHash("password4"),
                                                   "login3", PasswordHasher.GetHash("password4")));
            Assert.True(database.UpdateCredentials("login4", PasswordHasher.GetHash("password4"), 
                                                   "login4", PasswordHasher.GetHash("password4")));
            Assert.True(database.UpdateCredentials("login4", PasswordHasher.GetHash("password4"), 
                                                   "login4", PasswordHasher.GetHash("password5")));
            Assert.True(database.UpdateCredentials("login4", PasswordHasher.GetHash("password5"),
                                                   "login5", PasswordHasher.GetHash("password5")));
        }

        [Fact]
        public void deleteTest()
        {
            Assert.True(database.AddCredentials("login6", PasswordHasher.GetHash("password6")));
            Assert.True(database.DeleteCredentials("login6", PasswordHasher.GetHash("password6")));
        }

        [Fact]
        public void deleteWrongPasswordTest()
        {
            Assert.True(database.AddCredentials("login7", PasswordHasher.GetHash("password7")));
            Assert.False(database.DeleteCredentials("login7", PasswordHasher.GetHash("password6")));
        }

        [Fact]
        public void deleteNotExistTest()
        {
            Assert.True(database.AddCredentials("login8", PasswordHasher.GetHash("password8")));
            Assert.False(database.DeleteCredentials("login256", PasswordHasher.GetHash("password8")));
            Assert.False(database.DeleteCredentials("lоgin6", PasswordHasher.GetHash("password6")));//cyrillic "o"
        }
    }
}

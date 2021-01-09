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
        public void writeEmptyTest()
        {
            Assert.False(database.AddCredentials(null, PasswordHasher.GetHash("password")));
            Assert.False(database.AddCredentials("", PasswordHasher.GetHash("password")));
            Assert.False(database.AddCredentials(" ", PasswordHasher.GetHash("password")));
            Assert.False(database.AddCredentials("login", null));
            Assert.False(database.AddCredentials("login", ""));
            Assert.False(database.AddCredentials("login", " "));
            Assert.False(database.AddCredentials("login", "password"));
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

            Assert.True(database.UpdateCredentials("login3", PasswordHasher.GetHash("password3"), 
                                                   "login3", PasswordHasher.GetHash("password3")));
            Assert.True(database.UpdateCredentials("login3", PasswordHasher.GetHash("password3"), 
                                                   "login3", PasswordHasher.GetHash("password4")));
            Assert.True(database.UpdateCredentials("login3", PasswordHasher.GetHash("password4"),
                                                   "login4", PasswordHasher.GetHash("password4")));
        }
    }
}

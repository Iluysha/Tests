using System;
using Xunit;
//using Storage;

namespace Integration
{

    public class IntegrationTest
    {
        private const string Server = @"DESKTOP-38VTOGO\Ilya";
        private const string Database = @"IIG.CoSWE.StorageDB";
        private const bool IsTrusted = false;
        private const string Login = @"coswe";
        private const string Password = @"L}EjpfCgru9X@GLj";
        private const int ConnectionTimeout = 75;

        private StorageDataBaseUtils database = 
            new StorageDataBaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);

        [Fact]
        public void Test()
        {

        }
    }
}

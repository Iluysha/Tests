namespace Integration
{
    public class StorageDataBaseUtils
    {
        private string Server;
        private string Database;
        private bool IsTrusted;
        private string Login;
        private string Password;
        private int ConnectionTimeout;

        public StorageDataBaseUtils(string Server, string Database, bool IsTrusted,
            string Login, string Password, int ConnectionTimeout)
        {
            this.Server = Server;
            this.Database = Database;
            this.IsTrusted = IsTrusted;
            this.Login = Login;
            this.Password = Password;
            this.ConnectionTimeout = ConnectionTimeout;
        }
    }
}

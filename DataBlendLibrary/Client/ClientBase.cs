using System;
using System.Reflection;
using System.Text;

namespace DataBlendLibrary.Client
{
    public abstract class ClientBase
    {
        //protected new static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly bool _isSecure;

        protected ClientBase(string host, int port, string username, string password, bool isSecure)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _isSecure = isSecure;

            //Logger.InfoFormat("ThirdPartyClient '{0}' connection details:", GetType().Name);
            //Logger.InfoFormat("Host = {0}", _host);
            //Logger.InfoFormat("Username = {0}", _username);
            //Logger.InfoFormat("port = {0}", _port);
            //Logger.InfoFormat("isSecure = {0}", _isSecure);
        }

        protected ClientBase(string host, int port, string username, string password)
            : this(host, port, username, password, false)
        {
        }

        #region Properties

        public string Host
        {
            get { return _host; }
        }

        public int Port
        {
            get { return _port; }
        }

        public string Username
        {
            get { return _username; }
        }

        public string Password
        {
            get { return _password; }
        }

        public bool IsSecure
        {
            get { return _isSecure; }
        }

        #endregion

        #region Methods

        #endregion
    }
}

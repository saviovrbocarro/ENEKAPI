using System;
using System.Collections.Generic;
using System.Text;

namespace EnsekAPITests
{
    public class Settings
    {
		private string _token;
		private string _testServerEndpoint;

		public string Token
		{
			get { return _token; }
			set { _token = value; }
		}
		public string TestServerEndpoint
		{
			get { return _testServerEndpoint; }
			set { _testServerEndpoint = value; }
		}
	}
}

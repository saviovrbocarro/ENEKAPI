using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace EnsekAPITests.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private static string appSettingsFile = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + Path.DirectorySeparatorChar + "appsettings.json";

        [BeforeScenario]
        public void BeforeScenario()
        {
            ConfigurationBuilder config = new ConfigurationBuilder();
            config.AddJsonFile(@"C:\Users\savio\source\repos\EnsekAPITests\bin\Debug\netcoreapp3.1\appsettings.json");
            var configRoot = config.Build();
        }
    }
}

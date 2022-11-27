using CsharpExercise.Contracts;
using System;
using System.Collections.Generic;

namespace CsharpExercise.Tests
{
    public class MockAppSettingsConfig : IAppSettingsConfig
    {
        public Dictionary<string, string> GetConversionSource()
        {
            return new Dictionary<string, string>(){
                {"Path", ".{0}Source Files"},
                {"Filename", "Document1.xml"},
                {"ConnectionString", "DataSource:app.db;Cache=Shared"},
            };
        }

        public Dictionary<string, string> GetConversionTarget()
        {
            return new Dictionary<string, string>(){
                {"Path", ".{0}Target Files"},
                {"Filename", "Document1.json"},
                {"ConnectionString", "DataSource:app.db;Cache=Shared"},
            };
        }

        public Dictionary<string, Dictionary<string, string>> GetSerilogSettings()
        {
            throw new NotImplementedException();
        }
    }
}
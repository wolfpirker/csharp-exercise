using NUnit.Framework;

namespace CsharpExercise.Tests
{
    public class Target_To_File_Tests
    {
        [SetUp]
        public void Setup()
        {
            // to be mocked here
            // the configuration like for
            // Source_From_File_Tests, also the logging
            // like in other Units tests

            // issue of this Unit Tests here:
            // it would like this not be independent
            // of the filesystem; 
            // following library could help 
            // to write wrapper methods for File I/O methods:
            // System.IO.Abstractions

        }

        [Test]
        public void ReturnSuccess()
        {
            Assert.Pass();
        }
    }
}
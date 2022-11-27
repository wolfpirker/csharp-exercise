using NUnit.Framework;

namespace CsharpExercise.Tests
{
    public class Json_Writer_Tests
    {
        [SetUp]
        public void Setup()
        {
            // mocks required in here: 
            // the ILogger like previously,
            // and we need a serializeable Object
            // (JsonTitleText or XmlTitleText)
            // in the specific format as input of Write()
        }

        [Test]
        public void SerializeAsMemoryStreamWithSuccess()
        {

            Assert.Pass();
        }

        // other tests to be implemented in here:
        // * SerializeAsMemoryWithError - for example with a object
        //   which cannot be serialized in given format
        // * a success check with another more complex XML format
        // * a success check with a JSON format as input
    }
}
using System.Reflection.PortableExecutable;
using System.IO.Enumeration;
using System.IO;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Moravia.Homework
{
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.json");

            try
            {
                FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
                var reader = new StreamReader(sourceStream);
                string input = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var xdoc = XDocument.Parse(input);
            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            var serializedDoc = JsonConvert.SerilizeObject(doc);

            var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(targetStream);
            sw.Write(serializedDoc);


        }
    }
}

/* first issues I had in mind seeing that code:
 * 1) all in Main is not as readable, not testable with Unit Tests, it is against Single-Resposibility Principle,
      classes should be cohesive and should usually only have one specific purpose, if possible;
      also it would be getting more ugly when we want to extend it with more options or features;
      when a class has too many responsibilities we should try to break it into smaller classes - to make it 
      easier to read, maintain and reuse
 * 2) the Path.Combine with a relative path can easily be problematic, when actual program is moved to another path
 *    or the executable is run from another path than expected
 * 3) the variable input is only valid in the try block, so there is a compilation error
 * 4) the File.Open would require the Dispose method, to get rid of the file from memory; 
      a better approach would be to use a using block
   5.1) we should avoid using throw ex, since it resetts the stacktrace;
      throw would preserve the stacktrace and we could find the original offender
   5.2) also instead of creating a new general Exception with a message of ex, 
   we should rather throw the 'ex' Exception, to get Information about the type of exception thrown
 *
 *
 * // other issues:
 * 6) it strange Document1.xml and Document1.json should be always named like that, we could consider creating a 
 * settings file or specifying the input and output document as command line argument (args)
 * also if the string should be constant, we should still not have the constants string in the functions, 
 * but it would be better to define a constant string for it; which we can then use at various places, without
 * entering this string; Magic String Antipattern is the name of such strings written in logic code, it is 
 * bad for readability and typos are not as easily found
 * 7) it makes much sense to use interfaces to describe what a class should implement and able to do;
 * it would not only help understanding what a class does, but can help making a class more extensible 
 * or parts more resusable;
 * 8) the StreamReader should be closed after use; with the close() method to release the StreamReader resource; 
 * the same for the StreamWriter at the end - or alternatively use a using block
 * 
 */

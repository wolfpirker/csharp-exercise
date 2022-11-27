this program is used as assesment; I should refactor the app - the initial code - to allow

a) work with documents of various storages e.g. filesystem, cloud storage or HTTP (HTML ready-only);
implement just one, but be sure that the implementation if versatile for adding other storages

b) be capable of reading/writing different formats; implement XML and JSON format; implementation should
   be versatile for adding more formats (YAML, BSON, etc.)

c) build the app in the way to be able to test classes in isolation

d) be able to add new formats and storages in the future so it will have none or minimal impact on the existing code

e) be able to use any combination of input/output storages and formats 
(e.g. read JSON from filesystem, convert to XML and upload to cloud storage)


* app should match quality of production application
* for Tests: no need to cover everything


considerations for the task:

- possible Design pattern suitable for conversion of formats:
  Adapter Design pattern; this design patterns allows objects with incompatible 
  interfaces to collaborate;
    > I am not sure, will have to see whether this is the right pattern
    > it might be rather overengineering
- Decorator Design pattern:
    > could be an idea to get different behavior like considering:
    > should it be stored on the web or as a file?
    > while keeping testability high, and be in line with Single Resposibility 
    and Open-Closed Principle of SOLID principles
- I will first define contracts (interfaces), to see and document how it could work out
    > and think about what all formats XML, BSON, JSON could have in common as a datatype
    or class to be compatible and replaceable objects
- 

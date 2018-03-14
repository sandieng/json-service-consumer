# json-service-consumer
Demonstrate the use of layered architecture to separate business, service and entities. 
The solution has 5 separate projects as follows:
- CommonLib project which contains an interface for the Business and Service layer
- BusinessLayer project which communicates with the service layer to get a list of JSON objects and returns a list of pet-owner relationship
- ServiceLayer project which is an HTTPClient wrapper to the actual API service
- ConsoleClient application to demonstrate the working application
- UnitTests project with tests on the business layer and the service layer.

using System.Collections.Generic;
using MongoDB.Driver;
using MongoWithAspNetCore.Models;

namespace MongoWithAspNetCore.Repositories
{
    public class EmployeeRepository
    {
        private readonly IMongoDatabase _mongoDatabase;
        public EmployeeRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = mongoClient.GetDatabase("Employeedb");
        }

        //Create
        public Employee AddEmployee(Employee entity)
        {
            _mongoDatabase.GetCollection<Employee>("Employee").InsertOne(entity);
            return entity;
        }

        //Read
        public IEnumerable<Employee> GetEmployees()
        {
            return _mongoDatabase.GetCollection<Employee>("Employee").Find(FilterDefinition<Employee>.Empty).ToList();
        }

        public Employee GetEmployeeById(int empId)
        {
            var filter = Builders<Employee>.Filter.Eq(employee => employee.EmployeeId, empId);
            return _mongoDatabase.GetCollection<Employee>("Employee").Find(filter).FirstOrDefault();
        }

        //Update
        public void UpdateEmployee(int empId, Employee entity)
        {
            var filter = Builders<Employee>.Filter.Eq(employee => employee.EmployeeId, empId);
            var update = Builders<Employee>.Update
                .Set(x => x.Name, entity.Name)
                .Set(x => x.Position, entity.Position)
                .Set(x => x.JoinedDate, entity.JoinedDate);

            var updateResult = _mongoDatabase.GetCollection<Employee>("Employee").UpdateOne(filter, update);

            //Work with updateResult
        }

        //Delete
        public void DeleteEmployee(int empId)
        {
            var filter = Builders<Employee>.Filter.Eq(employee => employee.EmployeeId, empId);
            _mongoDatabase.GetCollection<Employee>("Employee").DeleteOne(filter);
        }
    }
}

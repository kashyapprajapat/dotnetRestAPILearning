using dotrestapiwithmongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace dotrestapiwithmongo.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _studentsCollection;

        public StudentService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _studentsCollection = database.GetCollection<Student>(settings.Value.StudentsCollection);
        }

        public async Task<List<Student>> GetAsync() =>
            await _studentsCollection.Find(_ => true).ToListAsync();

        public async Task<Student> GetAsync(string id) =>
            await _studentsCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Student student) =>
            await _studentsCollection.InsertOneAsync(student);

        public async Task UpdateAsync(string id, Student studentIn) =>
            await _studentsCollection.ReplaceOneAsync(s => s.Id == id, studentIn);

        public async Task DeleteAsync(string id) =>
            await _studentsCollection.DeleteOneAsync(s => s.Id == id);
    }
}

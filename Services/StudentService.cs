using dotrestapiwithmongo.Models;
using MongoDB.Driver;

namespace dotrestapiwithmongo.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _studentsCollection;

        // 👇 Inject MongoDBSettings directly (not via IOptions<>)
        public StudentService(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _studentsCollection = database.GetCollection<Student>(settings.StudentsCollection);
        }

        // 📥 Get all students
        public async Task<List<Student>> GetAsync() =>
            await _studentsCollection.Find(_ => true).ToListAsync();

        // 📥 Get one student by ID
        public async Task<Student> GetAsync(string id) =>
            await _studentsCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        // ➕ Create new student
        public async Task CreateAsync(Student student) =>
            await _studentsCollection.InsertOneAsync(student);

        // 🔄 Update student by ID
        public async Task UpdateAsync(string id, Student studentIn) =>
            await _studentsCollection.ReplaceOneAsync(s => s.Id == id, studentIn);

        // ❌ Delete student by ID
        public async Task DeleteAsync(string id) =>
            await _studentsCollection.DeleteOneAsync(s => s.Id == id);
    }
}

using Newtonsoft.Json;
using TimeTrackingApp.Domain;

namespace TimeTrackingApp.DataAccess
{
    public class FileSystemDb<T> : IFileSystemDb<T> where T : BaseEntity
    {
        private readonly string _appPath;
        private readonly string _directoryPath;
        private readonly string _filePath;
        private int _id;
        public FileSystemDb()
        {
            _appPath = @"..\..\..\";
            _directoryPath = _appPath + @"\Database\";
            _filePath = _directoryPath + @$"{typeof(T).Name}Db.json";

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
                Writer(new List<T>());
            }

            List<T> entities = Reader();
            if (entities.Count > 0)
            {
                _id = entities.Last().Id;
            }
            else
            {
                _id = 0;
            }
        }


        protected List<T> Reader()
        {
            using (StreamReader sr = new StreamReader(_filePath))
            {
                var json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }

        protected void Writer(List<T> entities)
        {
            using (StreamWriter sw = new StreamWriter(_filePath))
            {
                var json = JsonConvert.SerializeObject(entities);
                sw.Write(json);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Successfully written in db");
                Console.ResetColor();
            }
        }

        public void Insert(T entity)
        {
            List<T> entities = Reader();
            entity.Id = ++_id;
            entities.Add(entity);

            Writer(entities);
        }

        public List<T> GetAll()
        {
            return Reader().ToList();
        }

        public T GetById(int id)
        {
            T entity = Reader().FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception($"Entity with id {id} does not exist!");
            }

            return entity;
        }

        public void Delete(int id)
        {
            List<T> entities = Reader();
            T entity = entities.FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception($"Entity with id {id} does not exist!");
            }

            entities.Remove(entity);

            Writer(entities);
        }

        public void Update(T entity)
        {
            List<T> entities = Reader();
            T foundEntity = entities.FirstOrDefault(x => x.Id == entity.Id);

            if (foundEntity == null)
            {
                throw new Exception($"Entity does not exist!");
            }

            entities[entities.IndexOf(foundEntity)] = entity;

            Writer(entities);
        }
    }
}

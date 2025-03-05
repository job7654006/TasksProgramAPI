using System.Text.Json;
using System.Xml.Linq;
using TasksProgramAPI.Models;

namespace TasksProgramAPI.Services
{
    public class TaskService
    {
        private const string FilePath = @"C:\Users\USER\Desktop\חיפוש עבודה\מבחני בית שהוגשו\שעמ\tasks.json";

        private List<TaskModel> tasks;

        public TaskService()
        {
            tasks = LoadTasks(); // טוען את המשימות מהקובץ בעת אתחול השירות
        }

        // 📌 קריאת המשימות מהקובץ
        private List<TaskModel> LoadTasks()
        {
            if (!File.Exists(FilePath)) return new List<TaskModel>(); // אם הקובץ לא קיים, החזר רשימה ריקה

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<TaskModel>>(json) ?? new List<TaskModel>();
        }

        // 📌 שמירת המשימות לקובץ JSON
        private void SaveTasks()
        {
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        // 📌 קבלת כל המשימות
        public List<TaskModel> GetAllTasks() => tasks;

        // 📌 קבלת משימה לפי מזהה
        public TaskModel? GetTaskById(int id) => tasks.FirstOrDefault(t => t.Id == id);

        public void AddTask(TaskModel task)
        {
            task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1; // יצירת ID חדש
            tasks.Add(task);
            SaveTasks();

            Console.WriteLine($"✅ משימה חדשה נוספה: {JsonSerializer.Serialize(task)}");
        }

        // 📌 עדכון משימה קיימת
        public bool UpdateTask(int id, TaskModel updatedTask)
        {
            var task = GetTaskById(id);
            if (task == null) return false;

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Priority = updatedTask.Priority;
            task.DueDate = updatedTask.DueDate;
            task.Status = updatedTask.Status;

            SaveTasks();
            return true;
        }

        // 📌 מחיקת משימה לפי מזהה
        public bool DeleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task == null) return false;

            tasks.Remove(task);
            SaveTasks();
            return true;
        }
    }
}
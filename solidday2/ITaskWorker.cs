using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static solidday2.Task;

namespace solidday2
{
    //TASK1
    public interface ITask
    {
        string Title { get; set; }
        string Description { get; set; }
        void AssignTo(IDeveloper developer);
    }

    public interface IDeveloper
    {
        string Name { get; set; }
    }

    public interface IAssignable
    {
        void AssignTask();
    }

    public interface IWorkable
    {
        void WorkOnTask();
        void CreateSubTask();
    }
    public class Developer : IDeveloper
    {
        public string Name { get; set; }
    }

    public class Task : ITask
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public void AssignTo(IDeveloper developer)
        {
            Console.WriteLine($"Assigning '{Title}' to {developer.Name}");
        }
    }
    public class TeamLead : IAssignable, IWorkable
    {
        private readonly ITask _task;
        private readonly IDeveloper _developer;

        public TeamLead(ITask task, IDeveloper developer)
        {
            _task = task;
            _developer = developer;
        }

        public void AssignTask()
        {
            _task.Title = "Merge and Deploy";
            _task.Description = "Deploy sharing feature to develop branch";
            _task.AssignTo(_developer);
        }

        public void CreateSubTask()
        {
            Console.WriteLine("Creating subtask...");
        }

        public void WorkOnTask()
        {
            Console.WriteLine("Working on the assigned task...");
        }
    }
    public class Manager : IAssignable
    {
        private readonly IAssignable _lead;

        public Manager(IAssignable lead)
        {
            _lead = lead;
        }

        public void AssignTask()
        {
            Console.WriteLine("Manager assigning task to team lead...");
            _lead.AssignTask();
        }

    }
    //
    // 1.
    // a. Based on specifications, we need to create an interface and a TeamLead class to implement it.
    // b. Later another role like Manager, who assigns tasks to TeamLead and will not work on the tasks, is introduced into the system,
    // Apply needed refactoring to for better design and mention the current design smells
    //VERSION A,B
    public interface ISqlFile
    {
        string LoadText();
        void SaveText(); // might be optional later
    }
    public class SqlFile : ISqlFile
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }

        public string LoadText()
        {
            return System.IO.File.ReadAllText(FilePath);
        }

        public void SaveText()
        {
            System.IO.File.WriteAllText(FilePath, FileText);
        }
    }
    public class SqlFileManager
    {
        public List<ISqlFile> lstSqlFiles { get; set; }

        public string GetTextFromFiles()
        {
            StringBuilder objStrBuilder = new StringBuilder();
            foreach (var objFile in lstSqlFiles)
            {
                objStrBuilder.Append(objFile.LoadText());
            }
            return objStrBuilder.ToString();
        }

        public void SaveTextIntoFiles()
        {
            foreach (var objFile in lstSqlFiles)
            {
                objFile.SaveText();
            }
        }
    }


    // c. New Requirement:
    // After some time our leaders might tell us that we may have a few read-only files in the application folder, 
    // so we need to restrict the flow whenever it tries to do a save on them.
    //VERSION C
    public interface IReadableSqlFile
    {
        string LoadText();
    }

    public interface IWritableSqlFile : IReadableSqlFile
    {
        void SaveText();
    }
    public class ReadOnlySqlFile : IReadableSqlFile
    {
        public string FilePath { get; set; }

        public string LoadText()
        {
            return System.IO.File.ReadAllText(FilePath);
        }
    }
    public class SqlFile : IWritableSqlFile
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }

        public string LoadText()
        {
            return System.IO.File.ReadAllText(FilePath);
        }

        public void SaveText()
        {
            System.IO.File.WriteAllText(FilePath, FileText);
        }
    }
    // d. To avoid an exception we need to modify "SqlFileManager" by adding one condition to the loop.
    //VeSION D
    public class SqlFileManager
    {
        public List<IReadableSqlFile> lstSqlFiles { get; set; }

        public string GetTextFromFiles()
        {
            StringBuilder objStrBuilder = new StringBuilder();
            foreach (var objFile in lstSqlFiles)
            {
                objStrBuilder.Append(objFile.LoadText());
            }
            return objStrBuilder.ToString();
        }

        public void SaveTextIntoFiles()
        {
            foreach (var objFile in lstSqlFiles)
            {
                if (objFile is IWritableSqlFile writableFile)
                {
                    writableFile.SaveText();
                }
            }
        }
    }

}


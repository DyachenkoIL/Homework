using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework.Models
{
    public interface IWorkRepository
    {
        void Add(WorkItem item);
        void Add(string plan, string status);
        IEnumerable<WorkItem> GetAll();
        WorkItem Find(string key);
        WorkItem Remove(string key);
        void Update(WorkItem item);
    }
}

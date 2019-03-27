﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Homework.Models
{
    public class WorkRepository : IWorkRepository
    {
        private static ConcurrentDictionary<string, WorkItem> _works =
              new ConcurrentDictionary<string, WorkItem>();

        public WorkRepository()
        {
            Add(new WorkItem { Plan = "First item" });
        }

        public IEnumerable<WorkItem> GetAll()
        {
            return _works.Values;
        }

        public void Add(WorkItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            _works[item.Key] = item;
        }

        public WorkItem Find(string key)
        {
            WorkItem item;
            _works.TryGetValue(key, out item);
            return item;
        }

        public WorkItem Remove(string key)
        {
            WorkItem item;
            _works.TryRemove(key, out item);
            return item;
        }

        public void Update(WorkItem item)
        {
            _works[item.Key] = item;
        }
    }
}
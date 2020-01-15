using System;

namespace CCLLC.Core.ExecutionContext
{
    public class RecordPointer<T> : IRecordPointer<T>
    {
        public T Id { get; }

        public string RecordType { get; }

        public string Name { get; }

        public RecordPointer(string recordType, T Id, string name)
        {
            this.RecordType = recordType;
            this.Id = Id;
            this.Name = name;
        }
    }
}

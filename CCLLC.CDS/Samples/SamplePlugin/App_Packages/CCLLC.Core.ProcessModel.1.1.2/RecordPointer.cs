using System;

namespace CCLLC.Core
{
    public class RecordPointer<T> : IRecordPointer<T>, ISettableRecordPointer<T>
    {
        public T Id { get;  }

        public string RecordType { get;  }
        
        string ISettableRecordPointer<T>.RecordType { get; set; }
        T ISettableRecordPointer<T>.Id { get; set; }       
              
        public RecordPointer() { }
        
        public RecordPointer(IRecordPointer<T> source)
        {
            this.RecordType = source.RecordType;
            this.Id = source.Id;            
        }

        public RecordPointer(string recordType, T Id)
        {
            this.RecordType = recordType;
            this.Id = Id;           
        }
    }
}

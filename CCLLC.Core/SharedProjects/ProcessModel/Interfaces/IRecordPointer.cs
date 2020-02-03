using System;

namespace CCLLC.Core
{    
    public interface ISettableRecordPointer<T> : IRecordPointer<T>
    {
        new string RecordType { get; set; }
        new T Id { get; set; }      
    }

    public interface IRecordPointer<T>
    {
        string RecordType { get; }
        T Id { get; }        
    }
}

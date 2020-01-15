using System;

namespace CCLLC.Core
{
    public interface IRecordPointer<T>
    {
        string RecordType { get; }
        T Id { get; }
        string Name { get; }
    }
}

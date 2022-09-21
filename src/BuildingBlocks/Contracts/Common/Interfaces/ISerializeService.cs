using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Contracts.Common.Interfaces
{
    public interface ISerializeService
    {
        string Serialize<T>(T obj);
        string Serialize<T>(T obj, Type type);

        T Derialize<T>(string text);
    }

}
using YourTV_DAL.Entities;
using System;

namespace YourTV_DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}

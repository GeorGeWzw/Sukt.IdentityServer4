using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using MOSI.IdentityServer4.Shared.DcContextInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOSI.IdentityServer4.Shared.DbContexts
{
    public class MosiPersistedGrantDbContext : PersistedGrantDbContext<MosiPersistedGrantDbContext>, IMosiPersistedGrantDbContext
    {
        public MosiPersistedGrantDbContext(DbContextOptions<MosiPersistedGrantDbContext> options, OperationalStoreOptions storeOptions) : base(options, storeOptions)
        {
        }
    }
}

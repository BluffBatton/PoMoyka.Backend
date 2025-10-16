using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Interfaces
{
    public interface IApplicationDbContext
    {
         DbSet<User> Users { get;  }
         DbSet<Car> Cars { get;  }
         DbSet<Center> Centers { get;  }
         DbSet<Service> Services { get;  }
         DbSet<Statement> Statements { get;  }
         DbSet<CenterService> CenterServices { get;  }
         DbSet<Booking> Bookings { get;  }
         DbSet<Transaction> Transactions { get;  }
         DbSet<TypeService> TypeServices { get;  }
         DbSet<Rating> Ratings { get;  }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

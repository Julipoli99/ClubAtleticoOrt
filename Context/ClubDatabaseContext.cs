using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubAtleticoOrt.Models;

namespace ClubAtleticoOrt.Context
{ 

public class ClubDatabaseContext : DbContext
{
	
		public ClubDatabaseContext(DbContextOptions<ClubDatabaseContext> options) : base(options)
		{
		}
		public DbSet<Usuario> Usuarios { get; set;}
		public DbSet<Cancha> Canchas { get; set; }
		public DbSet<Reserva> Reservas { get; set; }

	}

}

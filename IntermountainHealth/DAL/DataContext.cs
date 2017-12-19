using IntermountainHealth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IntermountainHealth.DAL
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DataContext")
        {

        }

        public DbSet<PatientModel> patients { get; set; }

        public System.Data.Entity.DbSet<IntermountainHealth.Models.PatientFormModel> PatientFormModels { get; set; }
        //public DbSet<Questions> questions { get; set; }
        //public DbSet<PokemonLevelMoves> pokemonlevelmoves { get; set; }
        //public DbSet<PokemonList> pokemonlist { get; set; }
        //public DbSet<MoveDex> movedex { get; set; }
        //public DbSet<TMHM> TMHMs { get; set; }
        //public DbSet<PokedexMain> pokedexmain { get; set; }

        //public System.Data.Entity.DbSet<PokeGoals.Models.TeamBuilderPokemon> TeamBuilders { get; set; }
    }
}
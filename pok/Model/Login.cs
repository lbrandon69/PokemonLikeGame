using System;
using System.Collections.Generic;
using PokemonLikeProject.Model;

namespace PokemonLikeProject.Model;

public partial class Login
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}

using System;
using System.Collections.Generic;

namespace DotNet_rpg.Models;
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordHased { get; set; }
    public byte[] PasswordSalt { get; set; } 
    public List<Character>? Characters { get; set; }

}

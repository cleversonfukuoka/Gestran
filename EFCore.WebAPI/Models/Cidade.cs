﻿namespace EFCore.WebAPI.Models
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Estado Estado { get; set; }
    }
}

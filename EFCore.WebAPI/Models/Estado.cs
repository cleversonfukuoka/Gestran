﻿namespace EFCore.WebAPI.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public Pais Pais { get; set; }
    }
}

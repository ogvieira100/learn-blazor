﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Models
{

    public class CustomerSearch: PagedDataRequest
    {

         public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Telphone { get; set; }

        public string? SurName { get; set; }

        public string? CPF { get; set; }


    }

    public class CustomerGenerator
    {
        private static readonly Random _random = new Random();
        private static readonly string[] _firstNames = { "Carlos", "Fernanda", "João", "Mariana", "Roberto", "Ana" };
        private static readonly string[] _lastNames = { "Silva", "Souza", "Oliveira", "Pereira", "Ferreira", "Almeida" };
        private static readonly string[] _streets = { "Avenida Paulista", "Rua das Flores", "Praça Central", "Alameda dos Anjos", "Travessa do Sol" };

        public static List<Customers> GenerateCustomers(int count)
        {
            var customers = new List<Customers>();

            for (int i = 0; i < count; i++)
            {
                var customer = new Customers
                {
                    Name = GetRandomItem(_firstNames),
                    SurName = GetRandomItem(_lastNames),
                    Email = GenerateEmail(),
                    Telphone = GeneratePhoneNumber(),
                    CPF = GenerateCPF(),
                    Addresses = GenerateAddresses(3) // 3 endereços por cliente
                };

                customers.Add(customer);
            }

            return customers;
        }

        private static List<Address> GenerateAddresses(int count)
        {
            var addresses = new List<Address>();

            for (int i = 0; i < count; i++)
            {
                addresses.Add(new Address
                {
                    Street = GetRandomItem(_streets),
                    Number = _random.Next(1, 9999).ToString()
                });
            }

            return addresses;
        }

        private static string GenerateEmail()
        {
            string name = GetRandomItem(_firstNames).ToLower();
            string domain = "example.com";
            return $"{name}{_random.Next(1, 100)}@{domain}";
        }

        private static string GeneratePhoneNumber()
        {
            return _random.Next(600000000, 999999999).ToString() + _random.Next(0, 9);
        }

        private static string GenerateCPF()
        {
            return string.Concat(Enumerable.Range(0, 11).Select(_ => _random.Next(0, 9).ToString()));
        }

        private static string GetRandomItem(string[] array)
        {
            return array[_random.Next(array.Length)];
        }
    }
    public class Address: EntityDataBase
    {
        [Required(ErrorMessage = "Endereço requerido.")]
        
        public string? Street { get; set; }

        [Required(ErrorMessage = "Número é requerido.")]
        
        public string? Number { get; set; }

        public long? CustomerId { get; set; }
        public virtual  Customers? Customer { get; set; }
    }

    public class Customers: EntityDataBase
    {
        [Required(ErrorMessage = "Atenção nome requerido")]
        
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Atenção email inválido")]
        public string? Email { get; set; }

        
        public string? Telphone { get; set; }

        [Required(ErrorMessage = "Atenção nome requerido")]
        
        public string? SurName { get; set; }
        public virtual List<Address> Addresses { get; set; }

        [Required(ErrorMessage = "Atenção CPF requerido.")]
        public string? CPF { get; set; }

        
        public Customers()
        {
            Addresses = new List<Address>();
            
        }
    }
}

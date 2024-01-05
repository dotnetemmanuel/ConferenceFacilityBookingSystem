﻿namespace BookingApp.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public bool IsBusinessCustomer { get; set; }
    }
}

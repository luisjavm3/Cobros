﻿namespace Cobros.API.Entities
{
    public class Loan:Entity
    {
        public int Value { get; set; }
        public int LoanInterest { get; set; }
        public int RoutePosition { get; set; }

        public int CobroId { get; set; }
        public Cobro Cobro { get; init; }
        public Customer Customer { get; init; }
        public IEnumerable<PartialPayment> PartialPayments { get; set; }

        public double Total => Value * (1 + LoanInterest / (double)100);
        public int TotalPaid => PartialPayments.Aggregate(0, (acc, x) => acc + x.Value);
        public int Balance => (int)Total - TotalPaid;
    }
}
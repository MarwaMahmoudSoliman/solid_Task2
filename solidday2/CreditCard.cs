using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solidday2
{


    interface ICreditCard
    {
        void Charge();
    }

    internal class VisaCard : ICreditCard
    {
        public void Charge()
        {
            Console.WriteLine("Charging using Visa Card........");
        }
    }

    internal class MasterCard : ICreditCard
    {
        public void Charge()
        {
            Console.WriteLine("Charging using MasterCard........");
        }
    }

    internal class DebitCard : ICreditCard
    {
        public void Charge()
        {
            Console.WriteLine("Charging using Debit Card........");
        }
    }

    internal class Shopper
    {
        private readonly ICreditCard _card;

        public Shopper(ICreditCard card)
        {
            _card = card;
        }

        internal void Checkout()
        {
            _card.Charge();
        }
    }

}

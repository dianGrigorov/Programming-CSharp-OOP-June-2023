using BookingApp.Models.Rooms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        public int BedCapacity => throw new NotImplementedException();

        public double PricePerNight => throw new NotImplementedException();

        public void SetPrice(double price)
        {
            throw new NotImplementedException();
        }
    }
}

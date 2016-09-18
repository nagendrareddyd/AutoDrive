using AutoDriveEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveServices.Booking
{
    public interface IBookingService
    {
        IEnumerable<BookingEntity> GetAllBookings();
        BookingEntity GetBooking(string id);        
        bool Update(BookingEntity booking);
        bool Save(BookingEntity booking);
        bool Delete(string id);
    }
}

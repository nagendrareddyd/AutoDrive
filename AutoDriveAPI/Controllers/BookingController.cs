using AutoDriveAPI.CustomExceptions;
using AutoDriveAPI.Util;
using AutoDriveEntities;
using AutoDriveServices.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutoDriveAPI.Controllers
{
    public class BookingController : ApiController
    {
        private IBookingService BookingServices { get; set; }

        public BookingController(IBookingService bookingservice)
        {
            BookingServices = bookingservice;
        }
        // GET: api/Booking
        public HttpResponseMessage Get()
        {
            var result = BookingServices.GetAllBookings();
            if (result != null && result.ToList().Any())
                return Request.CreateResponse(result);
            throw new ApiDataException(9031, Constants.ErrorCode9031, HttpStatusCode.NotFound);
        }

        // GET: api/Booking/5
        public HttpResponseMessage Get(string id)
        {
            BookingEntity booking = null;
            if (!string.IsNullOrEmpty(id))
            {
                booking = BookingServices.GetBooking(id);
            }
            if (booking != null)
                return Request.CreateResponse(booking);
            throw new ApiDataException(9032, Constants.ErrorCode9032, HttpStatusCode.NotFound);
        }

        // POST: api/Booking
        public HttpResponseMessage Post([FromBody]BookingEntity value)
        {            
            var booking = BookingServices.GetBooking(value.Id);
            if (booking != null)
                throw new ApiDataException(9033, Constants.ErrorCode9033, HttpStatusCode.Conflict);

            if (BookingServices.Save(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.SavedSuccessfully);
            }
            throw new ApiDataException(8001, Constants.ErrorCode8001, HttpStatusCode.InternalServerError);
        }

        // PUT: api/Booking/5
        public HttpResponseMessage Put([FromBody]BookingEntity value)
        {
            var booking = BookingServices.GetBooking(value.Id);
            if (booking == null)
                throw new ApiDataException(9032, Constants.ErrorCode9032, HttpStatusCode.NotFound);

            if (BookingServices.Update(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.SavedSuccessfully);
            }
            throw new ApiDataException(8001, Constants.ErrorCode8001, HttpStatusCode.InternalServerError);
        }

        // DELETE: api/Booking/5
        public HttpResponseMessage Delete(string id)
        {
            var booking = BookingServices.GetBooking(id);
            if (booking == null)
                throw new ApiDataException(9032, Constants.ErrorCode9032, HttpStatusCode.NotFound);

            if (BookingServices.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.DeletedSuccessfully);
            }
            throw new ApiDataException(8002, Constants.ErrorCode8002, HttpStatusCode.InternalServerError);
        }
    }
}

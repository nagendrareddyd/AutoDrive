using AutoDriveDataModel.UnitOfWork;
using AutoDriveEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using Model = AutoDriveDataModel.Models;

namespace AutoDriveServices.Booking
{
    public class BookingService : IBookingService
    {
        private IUnitOfWork _unitOfWork { get; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Public constructor.
        /// </summary>
        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BookingEntity> GetAllBookings()
        {
            var bookings = _unitOfWork.GetBookingRepository.FindAll();
            if (bookings.Any())
            {
                return AutoMapperSetup.AutoMap.Map<List<Model.Booking>, List<BookingEntity>>(bookings.ToList());
            }
            return null;
        }

        public BookingEntity GetBooking(string id)
        {
            var booking = _unitOfWork.GetBookingRepository.GetById(id);
            if (booking != null)
            {
                return AutoMapperSetup.AutoMap.Map<Model.Booking, BookingEntity>(booking);
            }
            return null;
        }

        public bool Update(BookingEntity booking)
        {
            if (!string.IsNullOrEmpty(booking.Id))
            {
                var _booking = _unitOfWork.GetBookingRepository.GetById(booking.Id);
                if (_booking != null)
                {
                    _booking.ContactNumber = booking.ContactNumber;
                    _booking.CreatedBy = booking.CreatedBy;
                    _booking.DateCreatedOn = booking.DateCreatedOn;
                    _booking.DateModified = booking.DateModified;
                    _booking.EndDateTime = booking.EndDateTime;
                    _booking.Instructor = new Model.BookingInstructor()
                    {
                        Id = ObjectId.Parse(booking.Instructor.Id),
                        InstructorName = booking.Instructor.InstructorName
                    };
                    _booking.ModifiedBy = booking.ModifiedBy;
                    _booking.PickUplocation = booking.PickUplocation;
                    _booking.StartDateTime = booking.StartDateTime;
                    _booking.Status = booking.Status;
                    _booking.Student = new Model.BookingStudent()
                    {
                        Id = ObjectId.Parse(booking.Student.Id),
                        StudentName = booking.Student.StudentName
                    };
                    _booking.Type = booking.Type;
                    return Update(_booking);
                }
            }
            logger.Log(LogLevel.Info, "No Booking found");
            return false;
        }

        public bool Save(BookingEntity booking)
        {
            try
            {
                var _booking = new Model.Booking()
                {
                    ContactNumber = booking.ContactNumber,
                    CreatedBy = booking.CreatedBy,
                    DateCreatedOn = booking.DateCreatedOn,
                    DateModified = booking.DateModified,
                    EndDateTime = booking.EndDateTime,
                    Instructor = new Model.BookingInstructor()
                    {
                        Id = ObjectId.Parse(booking.Instructor.Id),
                        InstructorName = booking.Instructor.InstructorName
                    },
                    ModifiedBy = booking.ModifiedBy,
                    PickUplocation = booking.PickUplocation,
                    StartDateTime = booking.StartDateTime,
                    Status = booking.Status,
                    Student = new Model.BookingStudent()
                    {
                        Id = ObjectId.Parse(booking.Student.Id),
                        StudentName = booking.Student.StudentName
                    },
                    Type = booking.Type
                };
                _unitOfWork.GetBookingRepository.Insert(_booking);
                return true;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex);
                return false;
            }
        }
        public bool Delete(string id)
        {
            try
            {
                return _unitOfWork.GetBookingRepository.Delete(id);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex);
                return false;
            }
        }

        private bool Update(Model.Booking booking)
        {
            var builder = Builders<Model.Booking>.Filter;
            var filter = builder.Eq(x => x.Id, booking.Id);
            var updatebuilder = Builders<Model.Booking>.Update;
            var updates = updatebuilder
                .Set(t => t.ContactNumber, booking.ContactNumber)
                .Set(t => t.CreatedBy, booking.CreatedBy)
                .Set(t => t.DateCreatedOn, booking.DateCreatedOn)
                .Set(t => t.DateModified, booking.DateModified)
                .Set(t => t.EndDateTime, booking.EndDateTime)
                .Set(t => t.Instructor, booking.Instructor)
                .Set(t => t.ModifiedBy, booking.ModifiedBy)
                .Set(t => t.PickUplocation, booking.PickUplocation)
                .Set(t => t.StartDateTime, booking.StartDateTime)
                .Set(t => t.Status, booking.Status)
                .Set(t => t.Student, booking.Student)
                .Set(t => t.Type, booking.Type);
            return _unitOfWork.GetBookingRepository.Update(filter, updates);
        }

    }
}

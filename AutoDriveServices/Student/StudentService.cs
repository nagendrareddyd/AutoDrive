using AutoDriveDataModel.UnitOfWork;
using AutoDriveEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using Model = AutoDriveDataModel.Models;

namespace AutoDriveServices.Student
{
    public class StudentService : IStudentService
    {
        private IUnitOfWork _unitOfWork { get; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Public constructor.
        /// </summary>
        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }       
         public IEnumerable<StudentEntity> GetAllStudents()
        {
            var students = _unitOfWork.GetStudentRepository.FindAll();
            if (students.Any())
            {
                return AutoMapperSetup.AutoMap.Map<List<Model.Student>, List<StudentEntity>>(students.ToList());
            }
            return null;
        }

        public StudentEntity GetStudent(string id)
        {
            var student = _unitOfWork.GetStudentRepository.GetById(id);
            if (student != null)
            {
                return AutoMapperSetup.AutoMap.Map<Model.Student, StudentEntity>(student);
            }
            return null;
        }

        public StudentEntity GetStudentByCode(string code)
        {
            var builder = Builders<Model.Student>.Filter;
            var filter = builder.Eq(x => x.StudentCode, code);
            var student = _unitOfWork.GetStudentRepository.GetByFilter(filter).FirstOrDefault();
            if (student != null)
            {
                return AutoMapperSetup.AutoMap.Map<Model.Student, StudentEntity>(student);
            }
            return null;
        }

        public bool Update(StudentEntity student)
        {
            if (!string.IsNullOrEmpty(student.Id))
            {
                var _student = _unitOfWork.GetStudentRepository.GetById(student.Id);
                if (_student != null)
                {
                    _student.StudentCode = student.StudentCode;
                    _student.FullName = student.FullName;
                    _student.DOB = student.DOB;
                    _student.Email = student.Email;
                    _student.Gender = student.Gender;
                    _student.Instructor = new Model.StudentInstructor()
                    {
                        Id = ObjectId.Parse(student.Instructor.Id),
                        InstructorName = student.Instructor.InstructorName
                    };
                    _student.LicenceCountry = student.LicenceCountry;
                    _student.LicenceExpireOn = student.LicenceExpireOn;
                    _student.LicenceNumber = student.LicenceNumber;
                    _student.LicenceState = student.LicenceState;
                    _student.Mobile = student.Mobile;
                    _student.PickUpLocation = student.PickUpLocation;
                    _student.Suburbs = new Model.Suburb()
                    {
                        PostCode = student.Suburbs.PostCode,
                        SuburbName = student.Suburbs.SuburbName
                    };
                    _student.Status = student.Status;
                    return Update(_student);
                }
            }
            logger.Log(LogLevel.Info, "No Student found");
            return false;
        }

        public bool Save(StudentEntity student)
        {
            try
            {
                var _student = new Model.Student()
                {
                    StudentCode = student.StudentCode,
                    FullName = student.FullName,
                    DOB = student.DOB,
                    Email = student.Email,
                    Gender = student.Gender,
                    Instructor = new Model.StudentInstructor()
                    {
                        Id = ObjectId.Parse(student.Instructor.Id),
                        InstructorName = student.Instructor.InstructorName
                    },
                    LicenceCountry = student.LicenceCountry,
                    LicenceExpireOn = student.LicenceExpireOn,
                    LicenceNumber = student.LicenceNumber,
                    LicenceState = student.LicenceState,
                    Mobile = student.Mobile,
                    PickUpLocation = student.PickUpLocation,
                    Suburbs = new Model.Suburb()
                    {
                        PostCode = student.Suburbs.PostCode,
                        SuburbName = student.Suburbs.SuburbName
                    },
                    Status = student.Status
                };
                _unitOfWork.GetStudentRepository.Insert(_student);
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
                return _unitOfWork.GetStudentRepository.Delete(id);                
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex);
                return false;
            }
        }
        private bool Update(Model.Student student)
        {
            var builder = Builders<Model.Student>.Filter;
            var filter = builder.Eq(x => x.Id, student.Id);
            var updatebuilder = Builders<Model.Student>.Update;
            var updates = updatebuilder
                .Set(t => t.StudentCode, student.StudentCode)
                .Set(t => t.FullName, student.FullName)
                .Set(t => t.DOB, student.DOB)
                .Set(t => t.Email, student.Email)
                .Set(t => t.Gender, student.FullName)
                .Set(t => t.Instructor, student.Instructor)
                .Set(t => t.LicenceCountry, student.LicenceCountry)
                .Set(t => t.LicenceExpireOn, student.LicenceExpireOn)
                .Set(t => t.LicenceNumber, student.LicenceNumber)
                .Set(t => t.LicenceState, student.LicenceState)
                .Set(t => t.Mobile, student.Mobile)
                .Set(t => t.PickUpLocation, student.PickUpLocation)
                .Set(t => t.Status, student.Status)
                .Set(t => t.Suburbs, student.Suburbs);
            return _unitOfWork.GetStudentRepository.Update(filter, updates);
        }


    }
}

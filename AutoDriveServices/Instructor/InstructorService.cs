using AutoDriveDataModel.UnitOfWork;
using AutoDriveEntities;
using MongoDB.Driver;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using Model = AutoDriveDataModel.Models;

namespace AutoDriveServices.Instructor
{
    public class InstructorService : IInstructorService
    {
        private IUnitOfWork _unitOfWork { get; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Public constructor.
        /// </summary>
        public InstructorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Delete(string id)
        {
            try
            {
                return _unitOfWork.GetInstructorRepository.Delete(id);                
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Error, ex);
                return false;
            }            
        }

        public IEnumerable<InstructorEntity> GetAllInstructors()
        {
            var instructors = _unitOfWork.GetInstructorRepository.FindAll();
            if (instructors.Any())
            {
                return AutoMapperSetup.AutoMap.Map<List<Model.Instructor>, List<InstructorEntity>>(instructors.ToList());
            }
            return null;
        }
        public string GetInstructorCode()
        {
            var instructors = _unitOfWork.GetInstructorRepository.FindAll();
            int code = 0;
            if (instructors.Any())
            {
                 code = instructors.Count() ;
            }
            code += 1;
            string result = code.ToString().PadLeft(5, '0');
            return result;
        }

        public InstructorEntity GetInstructor(string id)
        {
            var instructor = _unitOfWork.GetInstructorRepository.GetById(id);
            if (instructor != null)
            {
                return AutoMapperSetup.AutoMap.Map<Model.Instructor, InstructorEntity>(instructor);
            }
            return null;
        }

        public InstructorEntity GetInstructorByCode(string code)
        {
            var builder = Builders<Model.Instructor>.Filter;
            var filter = builder.Eq(x => x.InstructorCode, code);
            var instructor = _unitOfWork.GetInstructorRepository.GetByFilter(filter).FirstOrDefault();
            if (instructor != null)
            {
                return AutoMapperSetup.AutoMap.Map<Model.Instructor, InstructorEntity>(instructor);
            }
            return null;
        }

        public bool Update(InstructorEntity instructor)
        {
            if (!string.IsNullOrEmpty(instructor.Id))
            {
                var _instructor = _unitOfWork.GetInstructorRepository.GetById(instructor.Id);
                if (_instructor != null)
                {
                    _instructor.InstructorCode = instructor.InstructorCode;
                    _instructor.FirstName = instructor.FirstName;
                    _instructor.LastName = instructor.LastName;
                    _instructor.Gender = instructor.Gender;
                    _instructor.Email = instructor.Email;
                    _instructor.Address = instructor.Address;                       
                    _instructor.Home = instructor.Home;
                    _instructor.Status = instructor.Status;
                    _instructor.Suburb = new Model.Suburb()
                    {
                        SuburbName = instructor.Suburb.SuburbName,
                        PostalCode = instructor.Suburb.PostalCode
                    };
                    var areaslist = new List<Model.Area>();
                    foreach (var item in instructor.Areas)
                    {
                        areaslist.Add(new Model.Area()
                        {
                            AreaCode = item.AreaCode,
                            Name = item.Name
                        });
                    }
                    _instructor.Areas = areaslist;
                    return Update(_instructor);
                }
            }
            logger.Log(LogLevel.Info, "No Area found");
            return false;
        }

        public bool Save(InstructorEntity instructor)
        {
            try
            {
                var _instructor = new Model.Instructor()
                {
                     Email = instructor.Email,
                    Gender = instructor.Gender,
                    Home = instructor.Home,
                   InstructorCode = instructor.InstructorCode,
                    Mobile = instructor.Mobile,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    Status = instructor.Status,
                    Address =instructor.Address          
                };
             //   instructor.InstructorCode = GetInstructorCode();
                _instructor.Suburb = new Model.Suburb()
                {
                    SuburbName = instructor.Suburb.SuburbName,
                    PostalCode = instructor.Suburb.PostalCode
                };
                var areaslist = new List<Model.Area>();
                foreach (var item in instructor.Areas)
                {
                    areaslist.Add(new Model.Area()
                    {
                        AreaCode = item.AreaCode,
                        Name = item.Name
                    });
                }
                _instructor.Areas = areaslist;
                _unitOfWork.GetInstructorRepository.Insert(_instructor);
                return true;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex);
                return false;
            }
        }
        private bool Update(Model.Instructor instructor)
        {
            var builder = Builders<Model.Instructor>.Filter;
            var filter = builder.Eq(x => x.Id, instructor.Id);
            var updatebuilder = Builders<Model.Instructor>.Update;
            var updates = updatebuilder
                .Set(t => t.InstructorCode, instructor.InstructorCode)
                .Set(t => t.FirstName, instructor.FirstName)
                .Set(t => t.LastName, instructor.LastName)
                .Set(t=> t.DOB,instructor.DOB)
                .Set(t => t.Address, instructor.Address)
                .Set(t => t.Areas, instructor.Areas)
                .Set(t => t.Email, instructor.Email)
                .Set(t => t.Gender, instructor.Gender)
                .Set(t => t.Home, instructor.Home)
                .Set(t => t.Mobile, instructor.Mobile)
                .Set(t => t.Status, instructor.Status)
                .Set(t => t.Suburb, instructor.Suburb);

            return _unitOfWork.GetInstructorRepository.Update(filter, updates);
        }
    }
}

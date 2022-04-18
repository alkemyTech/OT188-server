﻿using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsBusiness
    {

        Task<IEnumerable<Testimony>> GetTestimonials(bool listEntity);
        Task<Testimony> GetTestimonial(int id);
        Task<Testimony> InsertTestimonial(Testimony entity);
        Task UpdateTestimonial(int id, Testimony entity);
        Task DeleteTestimonial(int id);

    }
}
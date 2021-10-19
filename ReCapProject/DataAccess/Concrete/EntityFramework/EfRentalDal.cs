using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>,IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from user in context.Users
                             join customer in context.Customers
                             on user.UserId equals customer.UserId
                             join rental in context.Rentals
                             on customer.CustomerId equals rental.CustomerId
                             join car in context.Cars
                             on rental.CarId equals car.CarId
                             join brand in context.Brands
                             on car.BrandId equals brand.BrandId
                             join color in context.Colors
                             on car.ColorId equals color.ColorId
                             select new RentalDetailDto { RentalId = rental.RentalId, FirstName = user.FirstName, LastName = user.LastName, BrandName = brand.BrandName, ModelYear = car.ModelYear, ColorName = color.ColorName, DailyPrice = car.DailyPrice, RentDate = rental.RentDate, ReturnDate = rental.ReturnDate };
                return result.ToList();
            }
        }
    }
}

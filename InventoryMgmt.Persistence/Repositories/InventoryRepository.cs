using InventoryMgmt.Application.DTOs;
using InventoryMgmt.Application.Interfaces.IRepositories;
using InventoryMgmt.Application.Models;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace InventoryMgmt.Persistence.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public InventoryRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _dbContext = dbContext;
        }

        public  async Task<int> AddCarDetail(CarDetail carDetail)
        {
            int result = 0;
            CarDetailDto dto = new CarDetailDto();

            try
            {
                string email = SessionExtensions.GetString(_session, "Email");
                CarDetails car = new CarDetails();
                car.ModelName = carDetail.ModelName;
                car.CarName = carDetail.CarName;
                car.Price = carDetail.Price;
                car.BrandName = carDetail.BrandName;
                car.IsNew = carDetail.IsNew;
                car.CreatedBy = email;

                _dbContext.CarDetails.Add(car);
                result= await _dbContext.SaveChangesAsync();
                }
            catch (Exception ex)
            {

            }
            return result;

        }

        public async Task<int> DeleteDetail(int id)
        {
            int result = 0;
            try
            {
                var cardetail = await _dbContext.CarDetails.FindAsync(id);
                if (cardetail != null)
                { 
                    string email = SessionExtensions.GetString(_session, "Email");
                    cardetail.IsActive = false;
                    cardetail.ModifiedBy = email;
                }
                _dbContext.Entry(cardetail).State = EntityState.Modified;
                result = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
            }
            return result;
        }

        public async Task<CarDetail> GetCarDetailById(int id)
        {
            CarDetail carDetail = new CarDetail();

            try
            {
               var response= await _dbContext.CarDetails.FindAsync(id);
                if (response != null)
                {
                    carDetail = new CarDetail()
                    {
                        BrandName = response.BrandName,
                        ModelName = response.ModelName,
                        Id = response.Id,
                        CarName = response.CarName,
                        Price = response.Price,
                        IsNew=response.IsNew

                    };
                }
                

                
            }
            catch(Exception ex)
            {

            }
            return carDetail;
        }

        public async Task<CarDetailDto> GetCarDetails(CarDetailRequest request)
        {
           
            CarDetailDto dto = new CarDetailDto();
            try
            {


                var res = await (from a in _dbContext.CarDetails
                                 where a.IsActive
                                 select new CarDetailInfo
                                 {
                                     Id = a.Id,
                                     BrandName = a.BrandName,
                                     CarName = a.CarName,
                                     ModelName = a.ModelName,
                                     Price = a.Price,
                                     CreatedOn = a.CreatedDate.ToString("yyyy-MM-dd hh:mm:ss"),
                                     IsNew = a.IsNew?"Yes":"No",
                                     UpdatedOn = a.ModifiedDate == null ? "" : Convert.ToDateTime(a.ModifiedDate).ToString("yyyy-MM-dd hh:mm:ss")

                                 }).ToListAsync();

                //Getting data
                dto.data = res.Skip((request.skip) * request.pageSize).Take(request.pageSize).ToList();

                //total record
                var filterRecord = (from a in _dbContext.CarDetails
                                    where a.IsActive
                                    orderby a.Id descending
                                    select a).Count();

                // search data when search value found
                if (!string.IsNullOrEmpty(request.searchValue))
                {
                    res = res.Where(x => x.CarName.ToLower().Contains(request.searchValue.ToLower()) || x.BrandName.ToLower().Contains(request.searchValue.ToLower()) || x.ModelName.ToLower().Contains(request.searchValue.ToLower()) || x.Price.ToString().Contains(request.searchValue.ToLower()) || x.IsNew.ToString().Contains(request.searchValue.ToLower())).ToList();
                }
                dto.data = res;
                // get total count of records after search
                filterRecord = res.Count();
                //sort data by car name
                if (!string.IsNullOrEmpty(request.sortColumn) && !string.IsNullOrEmpty(request.sortColumnDirection))
                    res = res.OrderBy(a => a.CarName).ToList();

                dto.data = res;

                dto = new
                 CarDetailDto
                {
                    draw = request.draw,
                    recordsTotal = filterRecord,
                    recordsFiltered = filterRecord,
                    data = dto.data
                };
            }
            catch (Exception ex)
            {

            }
            return dto;

        }

        public async Task<int> UpdateCarDetail(CarDetail carDetail)
        {
            int result = 0;
            
            try
            {
                var cardetail = await _dbContext.CarDetails.FindAsync(carDetail.Id);
                if (cardetail != null)
                {


                    string email = SessionExtensions.GetString(_session, "Email");
                    cardetail.ModelName = carDetail.ModelName;
                    cardetail.CarName = carDetail.CarName;
                    cardetail.Price = carDetail.Price;
                    cardetail.BrandName = carDetail.BrandName;
                    cardetail.IsNew = carDetail.IsNew;
                    cardetail.ModifiedBy = email;
                }
                _dbContext.Entry(cardetail).State = EntityState.Modified;
                result = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}

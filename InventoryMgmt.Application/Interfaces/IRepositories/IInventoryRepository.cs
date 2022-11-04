using InventoryMgmt.Application.DTOs;
using InventoryMgmt.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Interfaces.IRepositories
{
    public interface IInventoryRepository
    {
        public Task<CarDetailDto> GetCarDetails(CarDetailRequest request);
        public Task<int> UpdateCarDetail(CarDetail carDetail);
        public Task<int> AddCarDetail(CarDetail carDetail);
        public Task<CarDetail> GetCarDetailById(int id);
        public Task<int> DeleteDetail(int id);


    }
}

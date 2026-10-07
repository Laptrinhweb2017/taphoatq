using Application.Assets.Dtos;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class EmployeeService(EmployeeRepo _repo)
    {
        public async Task<DtoResult<DtoEmployee>> GetAllAsync()
        {
            DtoResult<DtoEmployee> result = new();
            try
            {
                result = await _repo.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
            result.Succeed = result.ResultList != null;
            return result;
        }

        public async Task<DtoResult<DtoEmployee>> AddAsync(DtoEmployee dto)
        {
            DtoResult<DtoEmployee> result = new();
            try
            {
                result = await _repo.AddAsync(dto);
            }
            catch (Exception)
            {
                throw;
            }
            result.Succeed = result.Result != null;
            return result;
        }

        public async Task<DtoResult<DtoEmployee>> UpdateAsync(DtoEmployee dto)
        {
            DtoResult<DtoEmployee> result = new();
            try
            {
                result = await _repo.UpdateAsync(dto);
            }
            catch (Exception)
            {
                throw;
            }
            result.Succeed = result.Result != null;
            return result;
        }

        public async Task<DtoResult<DtoEmployee>> DeleteAsync(object? ID)
        {
            DtoResult<DtoEmployee> result = new();
            try
            {
                result = await _repo.DeleteAsync(ID);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<DtoResult<DtoEmployee>> DeleteAsync(DtoEmployee dto) => await DeleteAsync(dto.Id);
    }
}
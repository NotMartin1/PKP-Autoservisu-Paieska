﻿using Model.Entities.CarService;
using Model.Entities.CarWorkshop;
using Model.Entities.Filter;

namespace Model.Repositories
{
    public interface ICarWorkshopRepository
    {
        bool CheckIfCompanyNameExsits(string companyName);
        CarWorkshopBasicData GetBasicByUsername(string username);
        string GetPasswordByUsername(string username);
        bool CheckIfExsitsByUsername(string username);
        void Insert(CarWorkshopExtendedData args);
        List<CarWorkshopDisplayBasicData> List(ListArgs args);
        bool ValidateCredentials(string username, string password);
        bool CheckIfExsistsById(int id);
    }
}
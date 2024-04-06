﻿using Model.Entities.CarService;

namespace Model.Repositories
{
    public interface ICarWorkshopRepository
    {
        bool CheckIfCompanyNameExsits(string companyName);
        CarWorkshopBasicData GetBasicByUsername(string username);
        void Insert(CarWorkshopExtendedData args);
        bool ValidateCredentials(string username, string password);
    }
}
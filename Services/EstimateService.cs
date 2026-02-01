using ecommerce.Data;
using ecommerce.DTO;
using ecommerce.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;

public class EstimateService
{
    private readonly DataContext _db;

    public EstimateService(DataContext db)
    {
        _db = db;
    }

    public async Task CreateAsync(EstimateDto dto)
    {
        var estimate = new Estimate
        {
            Nom = dto.Nom,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Adresse = dto.Adresse,
            TarifTotal = dto.TarifTotal,
            Date = dto.Date,
            Intervention = dto.Intervention
        };

        try
        {
            _db.Estimate.Add(estimate);
            await _db.SaveChangesAsync();
        }catch (Exception ex)
        {

            return;
        }

    }
}

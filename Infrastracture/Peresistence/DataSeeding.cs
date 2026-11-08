using DomainLayer.Contracts.ProductContracts;
using DomainLayer.Models.IdentityModuls;
using DomainLayer.Models.ProductModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Peresistence.IdentityStore;
using Peresistence.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Peresistence
{
    public class DataSeeding
        (
            StoreDbContext _context ,
            UserManager<AppUser> _userManager, 
            RoleManager<IdentityRole> _roleManager ,
            IdentityStoreDbContext _identityStoreDbContext
        ) : IDataSeeding
    {
        
        public async Task DataSeedAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
               await _context.Database.MigrateAsync();
            }

            if (!_context.ProductBrands.Any())
            {
                var ProductBrandsItems = File.OpenRead("../Infrastracture/Peresistence/Store/Data/DataSeed/brands.json");
                var ProductBrandsSerializer =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsItems);
                if (ProductBrandsSerializer is not null && ProductBrandsSerializer.Any())
                {
                   await _context.ProductBrands.AddRangeAsync(ProductBrandsSerializer);
                }
            }


            if (!_context.ProductTypes.Any())
            {
                var ProductTypesItems = File.OpenRead("../Infrastracture/Peresistence/Store/Data/DataSeed/types.json");
                var ProductTypesSerializer =await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesItems);
                if (ProductTypesSerializer is not null && ProductTypesSerializer.Any())
                {
                   await _context.ProductTypes.AddRangeAsync(ProductTypesSerializer);
                }
            }

            if (!_context.Products.Any())
            {
                var ProductItems = File.OpenRead("../Infrastracture/Peresistence/Store/Data/DataSeed/products.json");
                var ProductSerializer =await JsonSerializer.DeserializeAsync<List<Product>>(ProductItems);
                if (ProductSerializer is not null && ProductSerializer.Any())
                {
                    await _context.Products.AddRangeAsync(ProductSerializer);
                }
            }


            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if ((await _identityStoreDbContext.Database.GetPendingMigrationsAsync()).Any()) 
                {
                    await _identityStoreDbContext.Database.MigrateAsync();
                }

                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }


                if (!_userManager.Users.Any())
                {
                    var User1 = new AppUser
                    {
                        DisplayName = "Anas Adel",
                        UserName = "anasadel",
                        PhoneNumber = "1234567890",
                        Email = "anas@gmail.com",
                    };
                    var User2 = new AppUser
                    {
                        DisplayName = "Adel Eltohamy",
                        UserName = "adeleltoahmy",
                        PhoneNumber = "1234567890",
                        Email = "adel@gmail.com",
                    };

                    await _userManager.CreateAsync(User1, "P@ssw0rd01");
                    await _userManager.CreateAsync(User2, "P@ssw0rd02");

                    await _userManager.AddToRoleAsync(User1, "Admin");
                    await _userManager.AddToRoleAsync(User2, "SuperAdmin");
                }

                await _identityStoreDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Identity DataSeeding Exception :: {ex.Message}");
            }
          
        }
    }
}

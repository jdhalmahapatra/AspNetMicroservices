using System;
using System.Data;
using Discount.API.Data;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDbService _dbService;
        

        public DiscountRepository(IDbService dbService)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        }

        //Interface Methods
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            try { 
                var newDiscount = await _dbService.EditData("insert into public.coupon (productname, description, amount)" +
                                            " values (@ProductName, @Description, @Amount)",
                                            new {
                                                ProductName = coupon.ProductName,
                                                Description = coupon.Description,
                                                Amount = coupon.Amount
                                            });
                return (newDiscount!=0)? true : false;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error is: ", e.Message.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            try { 
                var deleteCoupon = await _dbService.EditData("delete from public.coupon where productname=@ProductName", new { productName });
                return (deleteCoupon != 0) ? true : false;

            }
            catch(Exception e)
            {
                Console.WriteLine("Error is : ", e.Message);
                return false;
            }
            
        }

        public async Task<Coupon?> GetDiscount(string productName)
        {
            var coupon = await _dbService.
                        GetAsync<Coupon>("select *from public.coupon where productname=@productName",
                        new { productName });
            if(coupon is not null)
                return coupon;

            return new Coupon { ProductName="No Discount", Amount=0, Description="No Discount Desc"};

        }

        public async Task<List<Coupon>> GetAllDiscount()
        {
            var discountCoupons = await _dbService.GetAll<Coupon>("select * from public.coupon", new { });
            return discountCoupons;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            try
            {
                var updateDiscount = await _dbService.EditData("update public.coupon set productname=@ProductName, description=@Description, amount=@Amount" +
                    " where id=@Id",
                    new {
                        Id = coupon.Id,
                        ProductName = coupon.ProductName,
                        Description = coupon.Description,
                        Amount = coupon.Amount
                    });
                return (updateDiscount != 0) ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error is: ", e.Message);
                return false;
            }
        }
    }
}


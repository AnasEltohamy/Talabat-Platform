using AutoMapper;
using DomainLayer.Contracts.BasketContracts;
using DomainLayer.Contracts.ProductContracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModels;
using DomainLayer.Models.OrderModuls;
using DomainLayer.Models.ProductModels;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Payment
{
    public class PaymentService
            (
                IConfiguration _configuration,
                IBasketReposatory _basketReposatory,
                IUnitOfWork _unitOfWork,
                IMapper _mapper
            ) : IPaymentService
    {
        public async Task<CustomerBasketDTO> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            Stripe.StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            // 1) هات السلة
            var basket = await _basketReposatory.GetCustomerBasketAsync(basketId)
                ?? throw new BadrequestExcption(["Basket not found"]);

            if (basket.DeliveryMethodId is null)
                throw new BadrequestExcption(["Choose a delivery method first"]);

            // 2) احسب المبلغ من الداتابيز (مش من أسعار السلة)
            decimal total = 0;
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new BadrequestExcption([$"Product {item.Id} not found"]);
                total += product.Price * item.Quantity;
            }

            var delivery = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new BadrequestExcption(["Delivery method not found"]);

            basket.ShippingPrice = delivery.Cost;
            total += delivery.Cost;

            // 3) Stripe بياخد المبلغ بالسنت: 305 × 100 = 30500
            var amount = (long)(total * 100);

            // 4) أنشئ أو حدّث الـ Intent
            var service = new Stripe.PaymentIntentService();
            if (basket.PaymentIntentId is null)
            {
                var intent = await service.CreateAsync(new Stripe.PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "usd",
                    AutomaticPaymentMethods = new Stripe.PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true,
                        AllowRedirects = "never"
                    }
                });
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                await service.UpdateAsync(basket.PaymentIntentId,
                    new Stripe.PaymentIntentUpdateOptions { Amount = amount });
            }

            // 5) احفظ السلة في Redis ورجّعها
            await _basketReposatory.CreateOrUpdateBasketAsync(basket);
            return _mapper.Map<CustomerBasket, CustomerBasketDTO>(basket);
        }
    }
}


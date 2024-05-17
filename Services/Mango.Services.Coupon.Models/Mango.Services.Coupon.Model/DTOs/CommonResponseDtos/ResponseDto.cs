using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.Coupon.Model.DTOs.CommonResponseDtos
{
    /// <summary>
    /// This DTO class is for having common response for all the endpoints
    /// Endpoints: Get Method, Get Method With Id, Post Method, Put Method, Delete Method
    /// </summary>
    public class ResponseDto
    {
        public object? Result { get; set; }

        public bool IsSuccess { get; set; } = default;

        public string DisplayMessage { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; } = false;

        public DateTime ExpiryDate { get; set; }
    }
}

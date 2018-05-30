using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Teca.Core.Domain
{
    public enum BannerType
    {
        [Description("Banner trên(Slide)")]
        Top = 0,
        [Description("Banner dưới")]
        Bootom = 1,
        [Description("Banner trái")]
        Left = 2,
        [Description("Banner phải")]
        Right = 3,
        [Description("Banner(Công ty)")]
        Company = 4,
        [Description("Liên kết")]
        Relation = 5,
        [Description("Banner giữa")]
        Middle = 6,
    }

    public enum MenuListType
    {
        [Description("Menu trên")]
        Top = 0,
        [Description("Menu dưới")]
        Bottom = 1,
        [Description("Menu trái")]
        Left = 2,
        [Description("Menu phải")]
        Right = 3,
        [Description("Footer trái(Thông tin)")]
        FooterLeft = 4,
        [Description("Footer phải(Liên hệ)")]
        FooterRight = 5
    }

    public enum IntroductionType
    {
        [Description("Lịch sử hình thành")]
        LichSu = 0,
        [Description("Cơ cấu tổ chức")]
        CoCau = 1,
        [Description("Thành tích đạt được")]
        ThanhTich = 2,
    }

    public enum LogType
    {
        [Description("Thành công")]
        Success = 0,
        [Description("Lỗi")]
        Error = 1,
       
    }
    public enum LinkType
    {
        [Description("Link Local")]
        Local = 0,
        [Description("Link Youtube")]
        Youtube = 1,

    }
}
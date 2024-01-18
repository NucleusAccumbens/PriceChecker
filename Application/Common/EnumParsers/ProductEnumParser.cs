using Domain.Enums;

namespace Application.Common.EnumParsers;

public static class ProductEnumParser
{
    public static string GetProductStringValue(Product product)
    {
        if (product == Product.LemonAcid) return "Лимонная кислота";
        if (product == Product.MalicAcid) return "Яблочная кислота";
        if (product == Product.SuccinicAcid) return "Янтарная кислота";
        if (product == Product.TartaricAcid) return "Винная кислота";
        if (product == Product.Salt) return "Соль «Экстра»";
        if (product == Product.PotassiumChloride) return "Хлорид калия";
        if (product == Product.PropyleneGlycol) return "Пропиленгликоль";
        if (product == Product.MonosodiumGlutamate) return "Глутамат натрия";
        if (product == Product.SodiumBenzoate) return "Бензоат натрия";
        if (product == Product.PotassiumSorbate) return "Сорбат калия";
        if (product == Product.Polysorbate80) return "Полисорбат-80";
        if (product == Product.BrowmBottle50mlChina) return "Флакон коричневый (Китай)";
        if (product == Product.BrowmBottle50mlCzech) return "Флакон коричневый (Чехия)";
        if (product == Product.BlackBottle50ml) return "Флакон чёрный";
        if (product == Product.RoundPipette) return "Пипетка круглая";
        if (product == Product.SquarePipette) return "Пипетка квадратная";
        return "значение не указано";
    }
}

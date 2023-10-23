namespace PaymentTerminalManager.Lib;
public class PriceConvert {
    public static string ConvertDecimalToLongString(decimal price){
        return decimal.ToInt64(price).ToString();
    }
}

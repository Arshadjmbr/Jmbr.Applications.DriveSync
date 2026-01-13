using System.Text.RegularExpressions;

namespace DriveSync.Utility
{
    public class RegexUtility
    {


                // Emirates ID: 784-XXXX-XXXXXXX-X (15 digits total)
public static readonly Regex EmiratesIdRegex = new Regex(@"^784-\d{4}-\d{7}-\d{1}$");

    // UAE Mobile: Starts with 050, 052, 054, 055, 056, 058 followed by 7 digits
    public static readonly Regex UaeMobileRegex = new Regex(@"^(05|5|9715)\d{8}$");
    }
}

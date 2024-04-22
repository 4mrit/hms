using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace hms.Identity.API.Services.Helpers;

public static class EncodingHelper
{
  public static string Encode(string token)
  {
    var tokenEncoded =
        WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
    return HtmlEncoder.Default.Encode(tokenEncoded);
  }

  public static string Decode(string htmlSuitableCode)
  {
    return Encoding.UTF8.GetString(
        WebEncoders.Base64UrlDecode(htmlSuitableCode));
  }
}

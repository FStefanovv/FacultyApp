namespace FacultyApp.Utils;

public static class AuthCookieUtils {

    public static  (CookieOptions, CookieOptions) GetOptions (){
        var cookieOptionsHttpOnly = new CookieOptions {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddHours(1),
        };

        var cookieOptionsExpiration = new CookieOptions {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddHours(1),
        };

        return (cookieOptionsHttpOnly, cookieOptionsExpiration);
    }

    public static (CookieOptions, CookieOptions) GetInvalidationOptions()
       {
           var cookieOptionsHttpOnly = new CookieOptions()
               {
                   HttpOnly = true,
                   Secure = true,
                   SameSite = SameSiteMode.None,
                   Expires = DateTime.UtcNow.AddMinutes(-1),
               };

            var cookieOptionsExpiration = new CookieOptions()
               {
                   HttpOnly = false,
                   Secure = true,
                   SameSite = SameSiteMode.None,
                   Expires = DateTime.UtcNow.AddMinutes(-1),
               };
            
            return (cookieOptionsHttpOnly, cookieOptionsExpiration);
       }

}
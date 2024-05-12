namespace FacultyApp.Utils;

public static class AuthCookieUtils {

    public static CookieOptions GetOptions (){
        var cookieOptions = new CookieOptions {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddHours(1),
        };

        return cookieOptions;
    }

    public static CookieOptions GetInvalidationOptions()
       {
           var cookieOptions = new CookieOptions()
               {
                   HttpOnly = true,
                   Secure = true,
                   SameSite = SameSiteMode.None,
                   Expires = DateTime.UtcNow.AddMinutes(-1),
               };
            
            return cookieOptions;
       }

}
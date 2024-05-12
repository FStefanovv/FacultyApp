import { HttpHeaders, HttpInterceptorFn } from '@angular/common/http';

export const AddAccessTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authReq = req.clone({
    withCredentials: true
  });

  return next(authReq);
};

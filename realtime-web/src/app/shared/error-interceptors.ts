import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => next(req).pipe(catchError(handleErrorResponse));

function handleErrorResponse(error: HttpErrorResponse): ReturnType<typeof throwError> {
  const errorResponse = `{Error: {
    code: ${error.status},
    type: ${error.type},
    message: ${error.message},
    thing: ${error.headers.keys() + error.statusText}
  }}`;

  return throwError(() => errorResponse);
}

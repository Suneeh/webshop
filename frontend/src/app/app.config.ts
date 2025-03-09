import { ApplicationConfig, LOCALE_ID, provideExperimentalZonelessChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { environment as env } from '../environments/environment';
import { routes } from './app.routes';
import { authHttpInterceptorFn, provideAuth0 } from '@auth0/auth0-angular';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideExperimentalZonelessChangeDetection(),
    { provide: LOCALE_ID, useValue: 'de-DE' },
    provideAnimations(),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authHttpInterceptorFn])),
    provideAuth0({
      domain: env.auth.domain,
      clientId: env.auth.clientId,
      authorizationParams: {
        redirect_uri: window.location.origin,
        audience: 'webshop',
        scope: 'openid profile email manage',
      },
      httpInterceptor: env.httpInterceptor,
      cacheLocation: 'localstorage',
      useRefreshTokens: true,
    }),
  ],
};

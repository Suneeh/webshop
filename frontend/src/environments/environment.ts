import { HttpMethod } from '@auth0/auth0-angular';
import config from '../../auth_config.json';

export const {
  domain,
  clientId,
  authorizationParams: { audience },
  apiUri,
  errorPath,
} = config as {
  domain: string;
  clientId: string;
  authorizationParams: {
    audience?: string;
  };
  apiUri: string;
  errorPath: string;
};

export const environment = {
  production: false,
  auth: {
    domain,
    clientId,
    authorizationParams: {
      ...(audience && audience !== 'webshop' ? { audience } : null),
      redirect_uri: window.location.origin,
    },
    errorPath,
  },
  httpInterceptor: {
    allowedList: [
      {
        uri: `${apiUri}/*`,
        httpMethod: HttpMethod.Post,
        tokenOptions: {
          authorizationParams: {
            audience: 'webshop',
            scope: 'manage',
          },
        },
      },
      {
        uri: `${apiUri}/*`,
        httpMethod: HttpMethod.Put,
        tokenOptions: {
          authorizationParams: {
            audience: 'webshop',
            scope: 'manage',
          },
        },
      },
      {
        uri: `${apiUri}/*`,
        httpMethod: HttpMethod.Delete,
        tokenOptions: {
          authorizationParams: {
            audience: 'webshop',
            scope: 'manage',
          },
        },
      },
    ],
  },
  api: {
    url: config.apiUri,
  },
};
